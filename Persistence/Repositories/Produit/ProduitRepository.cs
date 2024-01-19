using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Produit;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public ProduitRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Ajouter(Domain.Entities.Produit nouveauProduit)
    {
        var nouveauProduitModel = _mapper.Map<ProduitModel>(nouveauProduit);

        _context.Produits.Add(nouveauProduitModel);
        _context.SaveChanges();
    }

    public List<Domain.Entities.Produit> Lister()
    {
        var produitsModels = _context.Produits
            .AsNoTracking()
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .OrderBy(produit => produit.Id)
            .ToList();

        return _mapper.Map<List<Domain.Entities.Produit>>(produitsModels);
    }

    public Domain.Entities.Produit Trouver(int id)
    {
        var produitModel = _context.Produits
            .AsNoTracking()
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Id == id);

        if (produitModel == null) throw new ProduitIntrouvable();

        return _mapper.Map<Domain.Entities.Produit>(produitModel);
    }

    public Domain.Entities.Produit Trouver(string nom)
    {
        var produitModel = _context.Produits
            .AsNoTracking()
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Nom == nom);

        if (produitModel == null) throw new ProduitIntrouvable();

        return _mapper.Map<Domain.Entities.Produit>(produitModel);
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