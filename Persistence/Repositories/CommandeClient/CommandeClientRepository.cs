using ApiCube.Domain.Entities;
using ApiCube.Domain.Enums.Commande;
using ApiCube.Domain.Mappers.Client;
using ApiCube.Domain.Mappers.CommandeClient;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.LigneCommandeClient;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.CommandeClient;

public class CommandeClientRepository : ICommandeClientRepository
{
    private readonly IClientMapper _clientMapper;
    private readonly ICommandeClientMapper _commandeClientMapper;
    private readonly ApiDbContext _context;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly ILigneCommandeClientMapper _ligneCommandeClientMapper;
    private readonly IMapper _mapper;
    private readonly IProduitMapper _produitMapper;
    private readonly StatutCommandeMapper _statutCommandeMapper;

    public CommandeClientRepository(ApiDbContext context, IMapper mapper, IClientMapper clientMapper,
        ICommandeClientMapper commandeClientMapper, IFamilleProduitMapper familleProduitMapper,
        IFournisseurMapper fournisseurMapper, ILigneCommandeClientMapper ligneCommandeClientMapper,
        IProduitMapper produitMapper, StatutCommandeMapper statutCommandeMapper)
    {
        _context = context;
        _mapper = mapper;
        _clientMapper = clientMapper;
        _commandeClientMapper = commandeClientMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _ligneCommandeClientMapper = ligneCommandeClientMapper;
        _produitMapper = produitMapper;
        _statutCommandeMapper = statutCommandeMapper;
    }

    public void Ajouter(Domain.Entities.CommandeClient nouvelleCommandeClient)
    {
        var nouvelleCommandeClientModel = _mapper.Map<CommandeClientModel>(nouvelleCommandeClient);
        _context.CommandesClients.Add(nouvelleCommandeClientModel);
        _context.SaveChanges();
    }

    public Domain.Entities.CommandeClient Trouver(int id)
    {
        var commandeClientModel = _context.CommandesClients
            .AsNoTracking()
            .Include(commandeClient => commandeClient.Client)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .FirstOrDefault(commandeClient => commandeClient.Id == id);

        if (commandeClientModel == null) throw new CommandeClientIntrouvable();

        var client = _clientMapper.Mapper(commandeClientModel.Client);
        var statut = _statutCommandeMapper.Mapper(commandeClientModel.Statut);
        var ligneCommandeClients = new List<LigneCommandeClient>();
        foreach (var ligneCommandeClientModel in commandeClientModel.LigneCommandeClients)
        {
            var familleProduit = _familleProduitMapper.Mapper(ligneCommandeClientModel.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(ligneCommandeClientModel.Produit.Fournisseur);
            var produit = _produitMapper.Mapper(ligneCommandeClientModel.Produit, familleProduit, fournisseur);
            var ligneCommandeClient = _ligneCommandeClientMapper.Mapper(ligneCommandeClientModel, produit);
            ligneCommandeClients.Add(ligneCommandeClient);
        }

        var commandeClient = _commandeClientMapper.Mapper(commandeClientModel, client, statut, ligneCommandeClients);

        return commandeClient;
    }

    public List<Domain.Entities.CommandeClient> Lister()
    {
        var commandeClientModels = _context.CommandesClients
            .AsNoTracking()
            .Include(commandeClient => commandeClient.Client)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .ToList();

        var commandeClients = new List<Domain.Entities.CommandeClient>();
        foreach (var commandeClientModel in commandeClientModels)
        {
            var client = _clientMapper.Mapper(commandeClientModel.Client);
            var statut = _statutCommandeMapper.Mapper(commandeClientModel.Statut);
            var ligneCommandeClients = new List<LigneCommandeClient>();

            foreach (var ligneCommandeClientModel in commandeClientModel.LigneCommandeClients)
            {
                var familleProduit = _familleProduitMapper.Mapper(ligneCommandeClientModel.Produit.FamilleProduit);
                var fournisseur = _fournisseurMapper.Mapper(ligneCommandeClientModel.Produit.Fournisseur);
                var produit = _produitMapper.Mapper(ligneCommandeClientModel.Produit, familleProduit, fournisseur);
                var ligneCommandeClient = _ligneCommandeClientMapper.Mapper(ligneCommandeClientModel, produit);
                ligneCommandeClients.Add(ligneCommandeClient);
            }

            var commandeClient =
                _commandeClientMapper.Mapper(commandeClientModel, client, statut, ligneCommandeClients);
            commandeClients.Add(commandeClient);
        }

        return commandeClients;
    }

    public List<Domain.Entities.CommandeClient> ListerParClient(int idClient)
    {
        var commandeClientModels = _context.CommandesClients
            .AsNoTracking()
            .Include(commandeClient => commandeClient.Client)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(commandeClient => commandeClient.LigneCommandeClients)
            .ThenInclude(ligneCommandeClient => ligneCommandeClient.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Where(commandeClient => commandeClient.Client.Id == idClient)
            .ToList();

        var commandeClients = new List<Domain.Entities.CommandeClient>();
        foreach (var commandeClientModel in commandeClientModels)
        {
            var client = _clientMapper.Mapper(commandeClientModel.Client);
            var statut = _statutCommandeMapper.Mapper(commandeClientModel.Statut);
            var ligneCommandeClients = new List<LigneCommandeClient>();

            foreach (var ligneCommandeClientModel in commandeClientModel.LigneCommandeClients)
            {
                var familleProduit = _familleProduitMapper.Mapper(ligneCommandeClientModel.Produit.FamilleProduit);
                var fournisseur = _fournisseurMapper.Mapper(ligneCommandeClientModel.Produit.Fournisseur);
                var produit = _produitMapper.Mapper(ligneCommandeClientModel.Produit, familleProduit, fournisseur);
                var ligneCommandeClient = _ligneCommandeClientMapper.Mapper(ligneCommandeClientModel, produit);
                ligneCommandeClients.Add(ligneCommandeClient);
            }

            var commandeClient =
                _commandeClientMapper.Mapper(commandeClientModel, client, statut, ligneCommandeClients);
            commandeClients.Add(commandeClient);
        }

        return commandeClients;
    }

    public void Modifier(Domain.Entities.CommandeClient commandeClient)
    {
        var commandeClientModel = _mapper.Map<CommandeClientModel>(commandeClient);
        _context.CommandesClients.Update(commandeClientModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.CommandeClient commandeClient)
    {
        var commandeClientModel = _mapper.Map<CommandeClientModel>(commandeClient);
        _context.CommandesClients.Remove(commandeClientModel);
        _context.SaveChanges();
    }
}