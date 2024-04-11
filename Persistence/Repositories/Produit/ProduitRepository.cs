using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Domain.Mappers.Promotion;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Produit;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProduitMapper _produitMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IPromotionMapper _promotionMapper;
    
    public ProduitRepository(ApiDbContext context, IMapper mapper, IProduitMapper produitMapper, IFamilleProduitMapper familleProduitMapper, IFournisseurMapper fournisseurMapper, IPromotionMapper promotionMapper)
    {
        _context = context;
        _mapper = mapper;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
        _promotionMapper = promotionMapper;
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

        var produits = new List<Domain.Entities.Produit>();
        
        foreach (var produitModel in produitsModels)
        {
            var familleProduit = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
            var fournisseur = _fournisseurMapper.Mapper(produitModel.Fournisseur);
            
            if (produitModel.Promotion == null)
            {
                var produit = _produitMapper.Mapper(produitModel, familleProduit, fournisseur);
                produits.Add(produit);
            }
            else
            {
                var promotion = _promotionMapper.Mapper(produitModel.Promotion);
                var produit = _produitMapper.Mapper(produitModel, familleProduit, fournisseur, promotion);
                produits.Add(produit);
            }
        }
        
        return produits;
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

        var familleProduit = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(produitModel.Fournisseur);
        
        if (produitModel.Promotion == null) return _produitMapper.Mapper(produitModel, familleProduit, fournisseur);
        
        var promotion = _promotionMapper.Mapper(produitModel.Promotion);
        return _produitMapper.Mapper(produitModel, familleProduit, fournisseur, promotion);
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

        var familleProduit = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
        var fournisseur = _fournisseurMapper.Mapper(produitModel.Fournisseur);
        
        if (produitModel.Promotion == null) return _produitMapper.Mapper(produitModel, familleProduit, fournisseur);
        
        var promotion = _promotionMapper.Mapper(produitModel.Promotion);
        return _produitMapper.Mapper(produitModel, familleProduit, fournisseur, promotion);
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