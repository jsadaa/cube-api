using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Repositories.Produit;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    
    public ProduitRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(AjouterProduitRequest produit)
    {
        ProduitModel nouveauProduit = new ProduitModel
        {
            Nom = produit.Nom,
            Description = produit.Description,
            Appellation = produit.Appellation,
            Cepage = produit.Cepage,
            Region = produit.Region,
            DegreAlcool = produit.DegreAlcool,
            FamilleProduitId = produit.FamilleProduitId,
            FournisseurId = produit.FournisseurId,
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            EnPromotion = produit.EnPromotion
        };
        
        _context.Produits.Add(nouveauProduit);
        _context.SaveChanges();
        
        return nouveauProduit.Id;
    }
    
    public List<ProduitDTO> Lister()
    {
        List<ProduitDTO> produits = new List<ProduitDTO>();
        
        produits.AddRange(
            _context.Produits
                .Include(produit => produit.FamilleProduit)
                .Include(produit => produit.Fournisseur)
                .Select(produit => new ProduitDTO
                {
                    Id = produit.Id,
                    Nom = produit.Nom,
                    Description = produit.Description,
                    Appellation = produit.Appellation,
                    Cepage = produit.Cepage,
                    Region = produit.Region,
                    DegreAlcool = produit.DegreAlcool,
                    FamilleProduitNom = produit.FamilleProduit.Nom,
                    FournisseurNom = produit.Fournisseur.Nom,
                    PrixAchat = produit.PrixAchat,
                    PrixVente = produit.PrixVente,
                    EnPromotion = produit.EnPromotion
                })
        );

        return produits;
    }
        
    
    public ProduitDTO? Trouver(int id)
    {
        ProduitModel? produit = null;
        produit = _context.Produits
            .Include(produit => produit.FamilleProduit)
            .Include(produit => produit.Fournisseur)
            .FirstOrDefault(produit => produit.Id == id);

        if (produit == null) return null;
            
        ProduitDTO produitDTO = new ProduitDTO
        {
            Id = produit.Id,
            Nom = produit.Nom,
            Description = produit.Description,
            Appellation = produit.Appellation,
            Cepage = produit.Cepage,
            Region = produit.Region,
            DegreAlcool = produit.DegreAlcool,
            FamilleProduitNom = produit.FamilleProduit.Nom,
            FournisseurNom = produit.Fournisseur.Nom,
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            EnPromotion = produit.EnPromotion
        };
        
        return produitDTO;
    }

    public int? Modifier(int id, AjouterProduitRequest produit)
    {
        ProduitModel? produitAModifier = null;
        produitAModifier = _context.Produits.Find(id);
        
        if (produitAModifier == null)
        {
            return null;
        }
        
        produitAModifier.Nom = produit.Nom;
        produitAModifier.Description = produit.Description;
        produitAModifier.Appellation = produit.Appellation;
        produitAModifier.Cepage = produit.Cepage;
        produitAModifier.Region = produit.Region;
        produitAModifier.DegreAlcool = produit.DegreAlcool;
        produitAModifier.FamilleProduitId = produit.FamilleProduitId;
        produitAModifier.FournisseurId = produit.FournisseurId;
        produitAModifier.PrixAchat = produit.PrixAchat;
        produitAModifier.PrixVente = produit.PrixVente;
        produitAModifier.EnPromotion = produit.EnPromotion;
        
        _context.Produits.Update(produitAModifier);
        _context.SaveChanges();
        
        return produitAModifier.Id;
    }

    public void Supprimer(int id)
    {
        ProduitModel? produit = null;
        produit = _context.Produits.Find(id);
        
        if (produit == null)
        {
            return;
        }
        
        _context.Produits.Remove(produit);
        _context.SaveChanges();
    }
}