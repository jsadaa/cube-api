using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Commande;
using ApiCube.Domain.Enums.Facture;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Domain.Mappers.CommandeClient;
using ApiCube.Domain.Mappers.FactureClient;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.LigneCommandeClient;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.FactureClient;

public class FactureClientRepository : IFactureClientRepository
{
    private readonly IClientMapper _clientMapper;
    private readonly ICommandeClientMapper _commandeClientMapper;
    private readonly ApiDbContext _context;
    private readonly IFactureClientMapper _factureClientMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly ILigneCommandeClientMapper _ligneCommandeClientMapper;
    private readonly IMapper _mapper;
    private readonly IProduitMapper _produitMapper;
    private readonly StatutCommandeMapper _statutCommandeMapper;
    private readonly StatutFactureMapper _statutFactureMapper;

    public FactureClientRepository(ApiDbContext context, IMapper mapper, ICommandeClientMapper commandeClientMapper,
        IProduitMapper produitMapper, IFamilleProduitMapper familleProduitMapper, IFournisseurMapper fournisseurMapper,
        IClientMapper clientMapper, IFactureClientMapper factureClientMapper,
        ILigneCommandeClientMapper ligneCommandeClientMapper, StatutCommandeMapper statutCommandeMapper,
        StatutFactureMapper statutFactureMapper)
    {
        _context = context;
        _mapper = mapper;
        _commandeClientMapper = commandeClientMapper;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _clientMapper = clientMapper;
        _factureClientMapper = factureClientMapper;
        _ligneCommandeClientMapper = ligneCommandeClientMapper;
        _statutCommandeMapper = statutCommandeMapper;
        _statutFactureMapper = statutFactureMapper;
    }

    public void Ajouter(Domain.Entities.FactureClient factureClient)
    {
        var factureClientModel = _mapper.Map<FactureClientModel>(factureClient);
        _context.FacturesClients.Add(factureClientModel);
        _context.SaveChanges();
    }

    public Domain.Entities.FactureClient Trouver(int id)
    {
        var factureClientModel = _context.FacturesClients
            .AsNoTracking()
            .Include(factureClient => factureClient.CommandeClient)
            .Include(factureClient => factureClient.CommandeClient.Client)
            .Include(factureClient => factureClient.CommandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(factureClient => factureClient.CommandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .FirstOrDefault(factureClient => factureClient.Id == id);

        if (factureClientModel == null) throw new FactureClientIntrouvable();

        return _mapper.Map<Domain.Entities.FactureClient>(factureClientModel);
    }

    public List<Domain.Entities.FactureClient> Lister()
    {
        var factureClientModels = _context.FacturesClients
            .AsNoTracking()
            .Include(factureClient => factureClient.CommandeClient)
            .Include(factureClient => factureClient.CommandeClient.Client)
            .Include(factureClient => factureClient.CommandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(factureClient => factureClient.CommandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .ToList();

        var factureClients = new List<Domain.Entities.FactureClient>();
        foreach (var factureClientModel in factureClientModels)
        {
            var client = _clientMapper.Mapper(factureClientModel.CommandeClient.Client);
            var statutCommande = _statutCommandeMapper.Mapper(factureClientModel.CommandeClient.Statut);
            var ligneCommandeClients = new List<LigneCommandeClient>();
            foreach (var ligneCommandeClientModel in factureClientModel.CommandeClient.LigneCommandeClients)
            {
                var familleProduit = _familleProduitMapper.Mapper(ligneCommandeClientModel.Produit.FamilleProduit);
                var fournisseur = _fournisseurMapper.Mapper(ligneCommandeClientModel.Produit.Fournisseur);
                var produit = _produitMapper.Mapper(ligneCommandeClientModel.Produit, familleProduit, fournisseur);
                var ligneCommandeClient = _ligneCommandeClientMapper.Mapper(ligneCommandeClientModel, produit);
                ligneCommandeClients.Add(ligneCommandeClient);
            }

            var commandeClient = _commandeClientMapper.Mapper(factureClientModel.CommandeClient, client, statutCommande,
                ligneCommandeClients);
            var statutFacture = _statutFactureMapper.Mapper(factureClientModel.Statut);
            var factureClient = _factureClientMapper.Mapper(factureClientModel, commandeClient, statutFacture);
            factureClients.Add(factureClient);
        }

        return factureClients;
    }

    public void Modifier(Domain.Entities.FactureClient factureClient)
    {
        var factureClientModel = _mapper.Map<FactureClientModel>(factureClient);
        _context.FacturesClients.Update(factureClientModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.FactureClient factureClient)
    {
        var factureClientModel = _mapper.Map<FactureClientModel>(factureClient);
        _context.FacturesClients.Remove(factureClientModel);
        _context.SaveChanges();
    }
}