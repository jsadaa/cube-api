using ApiCube.Domain.Entities;
using ApiCube.DTOs;
using ApiCube.DTOs.Responses;
using ApiCube.Models;
using ApiCube.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Repositories;

public class ProduitRepository : IProduitRepository
{
    private readonly ApiDbContext _context;
    
    public ProduitRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public void AjouterProduit(Produit produit)
    {
        ProduitModel nouveauProduit = new ProduitModel
        {
            Nom = produit.Nom,
            Description = produit.Description,
            SeuilDisponibilite = produit.SeuilDisponibilite,
            StatutStock = produit.StatutStock,
            Quantite = produit.Quantite,
            FamilleProduitId = produit.FamilleProduit.Id,
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            DateAchat = produit.DateAchat,
            DatePeremption = produit.DatePeremption
        };

        using (_context)
        {
            _context.Produits.Add(nouveauProduit);
            _context.SaveChanges();
        }
    }
    
    public List<ProduitDTO> ListerProduits()
    {
        List<ProduitDTO> produits = new List<ProduitDTO>();

        using (_context)
        {
            produits.AddRange(
                _context.Produits
                    .Include(produit => produit.FamilleProduit)
                    .Select(produit => new ProduitDTO
                    {
                        Id = produit.Id,
                        Nom = produit.Nom,
                        Description = produit.Description,
                        SeuilDisponibilite = produit.SeuilDisponibilite,
                        StatutStock = produit.StatutStock,
                        Quantite = produit.Quantite,
                        FamilleProduitNom = produit.FamilleProduit.Nom,
                        PrixAchat = produit.PrixAchat,
                        PrixVente = produit.PrixVente,
                        DateAchat = produit.DateAchat,
                        DatePeremption = produit.DatePeremption
                    })
            );
        }

        return produits;
    }
        
    
    public ProduitDTO? TrouverProduit(int id)
    {
        ProduitModel? produit = null;

        using (_context)
        {
            // find the product and find the family of the product to populate the DTO
            produit = _context.Produits
                .Include(produit => produit.FamilleProduit)
                .FirstOrDefault(produit => produit.Id == id);
        }

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
            PrixAchat = produit.PrixAchat,
            PrixVente = produit.PrixVente,
            DateAchat = produit.DateAchat,
            DatePeremption = produit.DatePeremption
        };
        
        return produitDTO;
    }

    public void ModifierProduit(int id, Produit produit)
    {
        ProduitModel? produitModifié = null;

        using (_context)
        {
            produitModifié = _context.Produits.Find(id);
        }

        if (produitModifié == null)
        {
            return;
        }
        
        produitModifié.Nom = produit.Nom;
        produitModifié.Description = produit.Description;
        produitModifié.SeuilDisponibilite = produit.SeuilDisponibilite;
        produitModifié.StatutStock = produit.StatutStock;
        produitModifié.Quantite = produit.Quantite;
        produitModifié.FamilleProduitId = produit.FamilleProduit.Id;
        produitModifié.PrixAchat = produit.PrixAchat;
        produitModifié.PrixVente = produit.PrixVente;
        produitModifié.DateAchat = produit.DateAchat;
        produitModifié.DatePeremption = produit.DatePeremption;
        
        using (_context)
        {
            _context.Produits.Update(produitModifié);
            _context.SaveChanges();
        }
    }

    public void SupprimerProduit(int id)
    {
        ProduitModel? produit = null;

        using (_context)
        {
            produit = _context.Produits.Find(id);
        }
        
        if (produit == null)
        {
            return;
        }
        
        using (_context)
        {
            _context.Produits.Remove(produit);
            _context.SaveChanges();
        }
    }
}