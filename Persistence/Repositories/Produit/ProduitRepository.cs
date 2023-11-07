using ApiCube.Domain.Mappers.FamilleProduit;
using ApiCube.Domain.Mappers.Fournisseur;
using ApiCube.Domain.Mappers.Produit;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Produit;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    private readonly IProduitMapper _produitMapper;
    private readonly IFamilleProduitMapper _familleProduitMapper;
    private readonly IFournisseurMapper _fournisseurMapper;
    private readonly IMapper _mapper;

    public ProduitRepository(ApiDbContext context, IProduitMapper produitMapper,
        IFamilleProduitMapper familleProduitMapper, IFournisseurMapper fournisseurMapper, IMapper mapper)
    {
        _context = context;
        _produitMapper = produitMapper;
        _familleProduitMapper = familleProduitMapper;
        _fournisseurMapper = fournisseurMapper;
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
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .ToList();

        var produits = new List<Domain.Entities.Produit>();

        foreach (var produitModel in produitsModels)
        {
            var familleProduitModel = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
            var fournisseurModel = _fournisseurMapper.Mapper(produitModel.Fournisseur);
            var produit = _produitMapper.Mapper(produitModel, familleProduitModel, fournisseurModel);

            produits.Add(produit);
        }

        return produits;
    }

    public Domain.Entities.Produit Trouver(int id)
    {
        var produitModel = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Id == id);

        if (produitModel == null) throw new ProduitIntrouvable();

        var familleProduitModel = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
        var fournisseurModel = _fournisseurMapper.Mapper(produitModel.Fournisseur);
        var produit = _produitMapper.Mapper(produitModel, familleProduitModel, fournisseurModel);

        return produit;
    }

    public Domain.Entities.Produit Trouver(string nom)
    {
        var produitModel = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .Include(produit => produit.Promotion)
            .FirstOrDefault(produit => produit.Nom == nom);

        if (produitModel == null) throw new ProduitIntrouvable();

        var familleProduitModel = _familleProduitMapper.Mapper(produitModel.FamilleProduit);
        var fournisseurModel = _fournisseurMapper.Mapper(produitModel.Fournisseur);
        var produit = _produitMapper.Mapper(produitModel, familleProduitModel, fournisseurModel);

        return produit;
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