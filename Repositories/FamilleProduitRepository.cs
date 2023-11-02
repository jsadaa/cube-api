using ApiCube.Domain.Entities;
using ApiCube.DTOs;
using ApiCube.Models;
using ApiCube.Repositories.Interfaces;

namespace ApiCube.Repositories;

public class FamilleProduitRepository : IFamilleProduitRepository
{
    private readonly ApiDbContext _context;
    
    public FamilleProduitRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public void AjouterFamilleProduit(FamilleProduit familleProduit)
    {
        FamilleProduitModel nouvelleFamilleProduit = new FamilleProduitModel
        {
            Nom = familleProduit.Nom,
            Description = familleProduit.Description
        };

        using (_context)
        {
            _context.FamillesProduits.Add(nouvelleFamilleProduit);
            _context.SaveChanges();
        }
    }
    
    public List<FamilleProduitDTO> ListerFamillesProduits()
    {
        List<FamilleProduitDTO> famillesProduits = new List<FamilleProduitDTO>();

        using (_context)
        {
            famillesProduits.AddRange(
                _context.FamillesProduits
                    .Select(familleProduit => new FamilleProduitDTO
                    {
                        Id = familleProduit.Id,
                        Nom = familleProduit.Nom,
                        Description = familleProduit.Description
                    })
            );
        }

        return famillesProduits;
    }
    
    public FamilleProduitDTO? TrouverFamilleProduit(int id)
    {
        FamilleProduitModel? familleProduit = null;
        
        using (_context)
        {
            familleProduit = _context.FamillesProduits.Find(id);
        }

        if (familleProduit == null)
        {
            return null;
        }
        
        return new FamilleProduitDTO
        {
            Id = familleProduit.Id,
            Nom = familleProduit.Nom,
            Description = familleProduit.Description
        };
    }
    
    public void ModifierFamilleProduit(int id, FamilleProduit familleProduit)
    {
        FamilleProduitModel? familleProduitModifiée = null;
        
        using (_context)
        {
            familleProduitModifiée = _context.FamillesProduits.Find(id);
        }

        if (familleProduitModifiée == null)
        {
            return;
        }
        
        familleProduitModifiée.Nom = familleProduit.Nom;
        familleProduitModifiée.Description = familleProduit.Description;

        using (_context)
        {
            _context.FamillesProduits.Update(familleProduitModifiée);
            _context.SaveChanges();
        }
    }
    
    public void SupprimerFamilleProduit(int id)
    {
        FamilleProduitModel? familleProduit = null;
        
        using (_context)
        {
            familleProduit = _context.FamillesProduits.Find(id);
        }

        if (familleProduit == null)
        {
            return;
        }

        using (_context)
        {
            _context.FamillesProduits.Remove(familleProduit);
            _context.SaveChanges();
        }
    }
}