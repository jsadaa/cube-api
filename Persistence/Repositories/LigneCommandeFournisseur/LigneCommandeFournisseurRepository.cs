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

namespace ApiCube.Persistence.Repositories.LigneCommandeFournisseur;

public class LigneCommandeFournisseurRepository : ILigneCommandeFournisseurRepository
{
    private readonly ApiDbContext _context;
    private readonly ILigneCommandeFournisseurMapper _ligneCommandeFournisseurMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IProduitMapper _produitMapper;
    private readonly ICommandeFournisseurMapper _commandeFournisseurMapper;
    private readonly IEmployeMapper _employeMapper;
    private readonly StatutCommandeMapper _statutCommandeMapper;
    private readonly IMapper _mapper;

    public LigneCommandeFournisseurRepository(ApiDbContext context,
        ILigneCommandeFournisseurMapper ligneCommandeFournisseurMapper,
        IFamilleProduitMapper familleProduitMapper,
        IFournisseurMapper fournisseurMapper,
        IProduitMapper produitMapper,
        ICommandeFournisseurMapper commandeFournisseurMapper,
        IEmployeMapper employeMapper,
        StatutCommandeMapper statutCommandeMapper,
        IMapper mapper)
    {
        _context = context;
        _ligneCommandeFournisseurMapper = ligneCommandeFournisseurMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _produitMapper = produitMapper;
        _commandeFournisseurMapper = commandeFournisseurMapper;
        _employeMapper = employeMapper;
        _statutCommandeMapper = statutCommandeMapper;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.LigneCommandeFournisseur nouvelleLigneCommandeFournisseur)
    {
        var nouvelleLigneCommandeFournisseurModel = _mapper.Map<LigneCommandeFournisseurModel>(nouvelleLigneCommandeFournisseur);
        _context.LignesCommandesFournisseurs.Add(nouvelleLigneCommandeFournisseurModel);
        _context.SaveChanges();
    }

    public Domain.Entities.LigneCommandeFournisseur Trouver(int id)
    {
        var ligneCommandeFournisseurModel = _context.LignesCommandesFournisseurs
            .AsNoTracking()
            .Include(ligneCommandeFournisseur => ligneCommandeFournisseur.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(ligneCommandeFournisseurModel => ligneCommandeFournisseurModel.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(ligneCommandeFournisseurModel => ligneCommandeFournisseurModel.CommandeFournisseur)
            .ThenInclude(commandeFournisseurModel => commandeFournisseurModel.Employe)
            .FirstOrDefault(ligneCommandeFournisseur => ligneCommandeFournisseur.Id == id);
        
        if (ligneCommandeFournisseurModel == null) throw new LigneCommandeFournisseurIntrouvable();
        
        var familleProduit = _familleProduitMapper.Mapper(ligneCommandeFournisseurModel.Produit.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(ligneCommandeFournisseurModel.Produit.Fournisseur);
        var produit = _produitMapper.Mapper(ligneCommandeFournisseurModel.Produit, familleProduit, fournisseur);
        var ligneCommandeFournisseur = _ligneCommandeFournisseurMapper.Mapper(ligneCommandeFournisseurModel, produit, ligneCommandeFournisseurModel.CommandeFournisseur.Id);
        
        return ligneCommandeFournisseur;
    }
    
    public List<Domain.Entities.LigneCommandeFournisseur> Lister()
    {
        var lignesCommandesFournisseursModels = _context.LignesCommandesFournisseurs
            .AsNoTracking()
            .Include(ligneCommandeFournisseur => ligneCommandeFournisseur.Produit)
            .ThenInclude(produitModel => produitModel.FamilleProduit)
            .Include(ligneCommandeFournisseurModel => ligneCommandeFournisseurModel.Produit)
            .ThenInclude(produitModel => produitModel.Fournisseur)
            .Include(ligneCommandeFournisseurModel => ligneCommandeFournisseurModel.CommandeFournisseur)
            .ThenInclude(commandeFournisseurModel => commandeFournisseurModel.Employe)
            .ToList();
        
        var lignesCommandesFournisseurs = new List<Domain.Entities.LigneCommandeFournisseur>();
        
        foreach (var ligneCommandeFournisseurModel in lignesCommandesFournisseursModels)
        {
            var familleProduit = _familleProduitMapper.Mapper(ligneCommandeFournisseurModel.Produit.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(ligneCommandeFournisseurModel.Produit.Fournisseur);
            var produit = _produitMapper.Mapper(ligneCommandeFournisseurModel.Produit, familleProduit, fournisseur);
            var ligneCommandeFournisseur = _ligneCommandeFournisseurMapper.Mapper(ligneCommandeFournisseurModel, produit, ligneCommandeFournisseurModel.CommandeFournisseur.Id);
            lignesCommandesFournisseurs.Add(ligneCommandeFournisseur);
        }
        
        return lignesCommandesFournisseurs;
    }
    
    public void Modifier(Domain.Entities.LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        var ligneCommandeFournisseurModel = _mapper.Map<LigneCommandeFournisseurModel>(ligneCommandeFournisseur);
        _context.LignesCommandesFournisseurs.Update(ligneCommandeFournisseurModel);
        _context.SaveChanges();
    }
    
    public void Supprimer(Domain.Entities.LigneCommandeFournisseur ligneCommandeFournisseur)
    {
        var ligneCommandeFournisseurModel = _mapper.Map<LigneCommandeFournisseurModel>(ligneCommandeFournisseur);
        _context.LignesCommandesFournisseurs.Remove(ligneCommandeFournisseurModel);
        _context.SaveChanges();
    }
}