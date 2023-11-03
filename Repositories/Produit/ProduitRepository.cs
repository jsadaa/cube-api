using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;
using ApiCube.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Repositories.Produit;

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
            SeuilDisponibilite = produit.SeuilDisponibilite,
            StatutStock = produit.StatutStock,
            Quantite = produit.Quantite,
            FamilleProduitId = produit.FamilleProduitId,
            FournisseurId = produit.FournisseurId,
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            DateAchat = produit.DateAchat,
            DatePeremption = produit.DatePeremption
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
                    SeuilDisponibilite = produit.SeuilDisponibilite,
                    StatutStock = produit.StatutStock,
                    Quantite = produit.Quantite,
                    FamilleProduitNom = produit.FamilleProduit.Nom,
                    FournisseurNom = produit.Fournisseur.Nom,
                    PrixAchat = produit.PrixAchat,
                    PrixVente = produit.PrixVente,
                    DateAchat = produit.DateAchat,
                    DatePeremption = produit.DatePeremption
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
            SeuilDisponibilite = produit.SeuilDisponibilite,
            StatutStock = produit.StatutStock,
            Quantite = produit.Quantite,
            FamilleProduitNom = produit.FamilleProduit.Nom,
            FournisseurNom = produit.Fournisseur.Nom,
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            DateAchat = produit.DateAchat,
            DatePeremption = produit.DatePeremption
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
        produitAModifier.SeuilDisponibilite = produit.SeuilDisponibilite;
        produitAModifier.StatutStock = produit.StatutStock;
        produitAModifier.Quantite = produit.Quantite;
        produitAModifier.FamilleProduitId = produit.FamilleProduitId;
        produitAModifier.FournisseurId = produit.FournisseurId;
        produitAModifier.PrixAchat = produit.PrixAchat;
        produitAModifier.PrixVente = produit.PrixVente;
        produitAModifier.DateAchat = produit.DateAchat;
        produitAModifier.DatePeremption = produit.DatePeremption;
        
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