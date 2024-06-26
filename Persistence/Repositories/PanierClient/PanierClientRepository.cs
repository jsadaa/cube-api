using ApiCube.Domain.Entities;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.LignePanierClient;
using ApiCube.Domain.Mappers.PanierClient;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Domain.Mappers.Promotion;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.PanierClient;

public class PanierClientRepository : IPanierClientRepository
{
    private readonly IClientMapper _clientMapper;
    private readonly ApiDbContext _context;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly ILignePanierClientMapper _lignePanierClientMapper;
    private readonly IPromotionMapper _promotionMapper;
    private readonly IMapper _mapper;
    private readonly IPanierClientMapper _panierClientMapper;
    private readonly IProduitMapper _produitMapper;

    public PanierClientRepository(ApiDbContext context, IMapper mapper, IPanierClientMapper panierClientMapper,
        IClientMapper clientMapper, ILignePanierClientMapper lignePanierClientMapper,
        IProduitMapper produitMapper, IFamilleProduitMapper familleProduitMapper,
        IFournisseurMapper fournisseurMapper, IPromotionMapper promotionMapper)
    {
        _context = context;
        _mapper = mapper;
        _panierClientMapper = panierClientMapper;
        _clientMapper = clientMapper;
        _lignePanierClientMapper = lignePanierClientMapper;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _promotionMapper = promotionMapper;
    }

    public void Ajouter(Domain.Entities.PanierClient nouveauPanierClient)
    {
        var nouveauPanierClientModel = _mapper.Map<PanierClientModel>(nouveauPanierClient);
        _context.PaniersClients.Add(nouveauPanierClientModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.PanierClient> Lister()
    {
        var panierClientModels = _context.PaniersClients
            .AsNoTracking()
            .Include(panierClient => panierClient.Client)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Promotion)
            .ToList();

        var panierClients = new List<Domain.Entities.PanierClient>();

        foreach (var panierClientModel in panierClientModels)
        {
            var client = _clientMapper.Mapper(panierClientModel.Client);
            var lignePanierClients = new List<LignePanierClient>();

            foreach (var lignePanierClientModel in panierClientModel.LignePanierClients)
            {
                var familleProduit = _familleProduitMapper.Mapper(lignePanierClientModel.Produit.FamilleProduit);
                var fournisseur = _fournisseurMapper.Mapper(lignePanierClientModel.Produit.Fournisseur);
                
                if (lignePanierClientModel.Produit.Promotion != null)
                {
                    var promotion = _promotionMapper.Mapper(lignePanierClientModel.Produit.Promotion);
                    var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur, promotion);
                    var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                    lignePanierClients.Add(lignePanierClient);
                } 
                else
                {
                    var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur);
                    var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                    lignePanierClients.Add(lignePanierClient);
                }
            }

            var panierClient = _panierClientMapper.Mapper(panierClientModel, client, lignePanierClients);
            panierClients.Add(panierClient);
        }

        return panierClients;
    }

    public Domain.Entities.PanierClient Trouver(int id)
    {
        var panierClientModel = _context.PaniersClients
            .AsNoTracking()
            .Include(panierClient => panierClient.Client)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Promotion)
            .FirstOrDefault(panierClient => panierClient.Id == id);

        if (panierClientModel == null) throw new PanierClientIntrouvable();

        var client = _clientMapper.Mapper(panierClientModel.Client);
        var lignePanierClients = new List<LignePanierClient>();

        foreach (var lignePanierClientModel in panierClientModel.LignePanierClients)
        {
            var familleProduit = _familleProduitMapper.Mapper(lignePanierClientModel.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(lignePanierClientModel.Produit.Fournisseur);
            
            if (lignePanierClientModel.Produit.Promotion != null)
            {
                var promotion = _promotionMapper.Mapper(lignePanierClientModel.Produit.Promotion);
                var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur, promotion);
                var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                lignePanierClients.Add(lignePanierClient);
            } 
            else
            {
                var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur);
                var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                lignePanierClients.Add(lignePanierClient);
            }
        }

        return _panierClientMapper.Mapper(panierClientModel, client, lignePanierClients);
    }

    public Domain.Entities.PanierClient TrouverParClient(int clientId)
    {
        var panierClientModel = _context.PaniersClients
            .AsNoTracking()
            .Include(panierClient => panierClient.Client)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(panierClient => panierClient.LignePanierClients)
            .ThenInclude(lignePanierClient => lignePanierClient.Produit)
            .ThenInclude(produitModel => produitModel.Promotion)
            .FirstOrDefault(panierClient => panierClient.ClientId == clientId);

        if (panierClientModel == null) throw new PanierClientIntrouvable();

        var client = _clientMapper.Mapper(panierClientModel.Client);
        var lignePanierClients = new List<LignePanierClient>();

        foreach (var lignePanierClientModel in panierClientModel.LignePanierClients)
        {
            var familleProduit = _familleProduitMapper.Mapper(lignePanierClientModel.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(lignePanierClientModel.Produit.Fournisseur);
            
            if (lignePanierClientModel.Produit.Promotion != null)
            {
                var promotion = _promotionMapper.Mapper(lignePanierClientModel.Produit.Promotion);
                var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur, promotion);
                var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                lignePanierClients.Add(lignePanierClient);
            } 
            else
            {
                var produit = _produitMapper.Mapper(lignePanierClientModel.Produit, familleProduit, fournisseur);
                var lignePanierClient = _lignePanierClientMapper.Mapper(lignePanierClientModel, produit);
                lignePanierClients.Add(lignePanierClient);
            }
        }

        return _panierClientMapper.Mapper(panierClientModel, client, lignePanierClients);
    }

    public void Modifier(Domain.Entities.PanierClient panierClient)
    {
        var panierClientModel = _mapper.Map<PanierClientModel>(panierClient);
        _context.PaniersClients.Update(panierClientModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.PanierClient panierClient)
    {
        var panierClientModel = _mapper.Map<PanierClientModel>(panierClient);
        _context.PaniersClients.Remove(panierClientModel);
        _context.SaveChanges();
    }
}