using ApiCube.Domain.Factories;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Produit;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly ProduitFactory _produitFactory;
    private readonly IMapper _mapper;
    
    public ProduitRepository(ApiDbContext context, ProduitFactory produitFactory, IMapper mapper)
    {
        _context = context;
        _produitFactory = produitFactory;
        _mapper = mapper;
    }
    
    public int Ajouter(Domain.Entities.Produit nouveauProduit)
    {
        var nouveauProduitModel = _mapper.Map<ProduitModel>(nouveauProduit);
        
        _context.Produits.Add(nouveauProduitModel);
        _context.SaveChanges();
        
        return nouveauProduitModel.Id;
    }
    
    public List<Domain.Entities.Produit> Lister()
    {
        var produitsModels = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .ToList();
        
        return produitsModels.Select(produitModel => _produitFactory.Mapper(produitModel)).ToList();
    }
    
    public Domain.Entities.Produit Trouver(int id)
    {
        var produitModel = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Id == id);
        
        if (produitModel == null) throw new ProduitIntrouvable();
        
        return _produitFactory.Mapper(produitModel);
    }
    
    public Domain.Entities.Produit Trouver(string nom)
    {
        var produitModel = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Nom == nom);
        
        if (produitModel == null) throw new ProduitIntrouvable();
        
        return _produitFactory.Mapper(produitModel);
    }

    public void Modifier(Domain.Entities.Produit produitModifie)
    {
        var produitModel = _mapper.Map<ProduitModel>(produitModifie);
        
        _context.Produits.Update(produitModel);
        _context.SaveChanges();
    }

    public void Supprimer(Domain.Entities.Produit produitASupprimer)
    {
        var produitModel = _mapper.Map<ProduitModel>(produitASupprimer);
        
        _context.Produits.Remove(produitModel);
        _context.SaveChanges();
    }
}