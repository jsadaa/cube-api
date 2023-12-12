using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Mappers.CommandeFournisseur;
using ApiCube.Domain.Mappers.Employe;
using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.LigneCommandeFournisseur;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.CommandeFournisseur;

public class CommandeFournisseurRepository : ICommandeFournisseurRepository
{
    private readonly ApiDbContext _context;
    private readonly StatutCommandeMapper _statutCommandeMapper;
    private readonly ICommandeFournisseurMapper _commandeFournisseurMapper;
    private readonly ILigneCommandeFournisseurMapper _ligneCommandeFournisseurMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IEmployeMapper _employeMapper;
    private readonly IProduitMapper _produitMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IMapper _mapper;
    
    public CommandeFournisseurRepository(ApiDbContext context, StatutCommandeMapper statutCommandeMapper, ICommandeFournisseurMapper commandeFournisseurMapper, ILigneCommandeFournisseurMapper ligneCommandeFournisseurMapper, IFournisseurMapper fournisseurMapper, IEmployeMapper employeMapper, IProduitMapper produitMapper, IFamilleProduitMapper familleProduitMapper, IMapper mapper)
    {
        _context = context;
        _statutCommandeMapper = statutCommandeMapper;
        _commandeFournisseurMapper = commandeFournisseurMapper;
        _ligneCommandeFournisseurMapper = ligneCommandeFournisseurMapper;
        _fournisseurMapper = fournisseurMapper;
        _employeMapper = employeMapper;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _mapper = mapper;
    }
    
    public void Ajouter(Domain.Entities.CommandeFournisseur nouvelleCommandeFournisseur)
    {
        var nouvelleCommandeFournisseurModel = _mapper.Map<CommandeFournisseurModel>(nouvelleCommandeFournisseur);
        _context.CommandesFournisseurs.Add(nouvelleCommandeFournisseurModel);
        _context.SaveChanges();
    }

    public Domain.Entities.CommandeFournisseur Trouver(int id)
    {
        var commandeFournisseurModel = _context.CommandesFournisseurs
            .AsNoTracking()
            .Include(commandeFournisseur => commandeFournisseur.Fournisseur)
            .Include(commandeFournisseur => commandeFournisseur.Employe)
            .Include(commandeFournisseur => commandeFournisseur.LigneCommandeFournisseurs)
            .ThenInclude(ligneCommandeFournisseur => ligneCommandeFournisseur.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .FirstOrDefault(commandeFournisseur => commandeFournisseur.Id == id);

        if (commandeFournisseurModel == null) throw new CommandeFournisseurIntrouvable();

        var fournisseur = _fournisseurMapper.Mapper(commandeFournisseurModel.Fournisseur);
        var employe = _employeMapper.Mapper(commandeFournisseurModel.Employe);
        var statutCommande = _statutCommandeMapper.Mapper(commandeFournisseurModel.Statut);
        var listeLigneCommandeFournisseur = new List<Domain.Entities.LigneCommandeFournisseur>();
        
        foreach (var ligneCommandeFournisseurModel in commandeFournisseurModel.LigneCommandeFournisseurs)
        {
            var familleProduit = _familleProduitMapper.Mapper(ligneCommandeFournisseurModel.Produit.FamilleProduit);
            var produit = _produitMapper.Mapper(ligneCommandeFournisseurModel.Produit, familleProduit, fournisseur);
            var ligneCommandeFournisseur = _ligneCommandeFournisseurMapper.Mapper(ligneCommandeFournisseurModel, produit, commandeFournisseurModel.Id);
            listeLigneCommandeFournisseur.Add(ligneCommandeFournisseur);
        }
        
        var commandeFournisseur = _commandeFournisseurMapper.Mapper(commandeFournisseurModel, fournisseur, employe, listeLigneCommandeFournisseur, statutCommande);
        
        return commandeFournisseur;
    }
    
    public List<Domain.Entities.CommandeFournisseur> Lister()
    {
        var commandesFournisseursModels = _context.CommandesFournisseurs
            .AsNoTracking()
            .Include(commandeFournisseur => commandeFournisseur.Fournisseur)
            .Include(commandeFournisseur => commandeFournisseur.Employe)
            .Include(commandeFournisseur => commandeFournisseur.LigneCommandeFournisseurs)
            .ThenInclude(ligneCommandeFournisseur => ligneCommandeFournisseur.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .ToList();

        var commandesFournisseurs = new List<Domain.Entities.CommandeFournisseur>();
        
        foreach (var commandeFournisseurModel in commandesFournisseursModels)
        {
            var fournisseur = _fournisseurMapper.Mapper(commandeFournisseurModel.Fournisseur);
            var employe = _employeMapper.Mapper(commandeFournisseurModel.Employe);
            var statutCommande = _statutCommandeMapper.Mapper(commandeFournisseurModel.Statut);
            var listeLigneCommandeFournisseur = new List<Domain.Entities.LigneCommandeFournisseur>();
        
            foreach (var ligneCommandeFournisseurModel in commandeFournisseurModel.LigneCommandeFournisseurs)
            {
                var familleProduit = _familleProduitMapper.Mapper(ligneCommandeFournisseurModel.Produit.FamilleProduit);
                var produit = _produitMapper.Mapper(ligneCommandeFournisseurModel.Produit, familleProduit, fournisseur);
                var ligneCommandeFournisseur = _ligneCommandeFournisseurMapper.Mapper(ligneCommandeFournisseurModel, produit, commandeFournisseurModel.Id);
                listeLigneCommandeFournisseur.Add(ligneCommandeFournisseur);
            }
        
            var commandeFournisseur = _commandeFournisseurMapper.Mapper(commandeFournisseurModel, fournisseur, employe, listeLigneCommandeFournisseur, statutCommande);
            commandesFournisseurs.Add(commandeFournisseur);
        }

        return commandesFournisseurs;
    }
    
    public void Modifier(Domain.Entities.CommandeFournisseur commandeFournisseur)
    {
        var commandeFournisseurModel = _mapper.Map<CommandeFournisseurModel>(commandeFournisseur);
        _context.Update(commandeFournisseurModel);
        _context.SaveChanges();
    }
    
    public void Supprimer(Domain.Entities.CommandeFournisseur commandeFournisseur)
    {
        var commandeFournisseurModel = _mapper.Map<CommandeFournisseurModel>(commandeFournisseur);
        _context.CommandesFournisseurs.Remove(commandeFournisseurModel);
        _context.SaveChanges();
    }
}