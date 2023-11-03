using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.FamilleProduit;

public class FamilleProduitRepository : IFamilleProduitRepository
{
    private readonly ApiDbContext _context;
    
    public FamilleProduitRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(AjouterFamilleProduitRequest familleProduit)
    {
        FamilleProduitModel nouvelleFamilleProduit = new FamilleProduitModel
        {
            Nom = familleProduit.Nom,
            Description = familleProduit.Description
        };
        
        _context.FamillesProduits.Add(nouvelleFamilleProduit);
        _context.SaveChanges();
        
        return nouvelleFamilleProduit.Id;
    }
    
    public List<FamilleProduitDTO> Lister()
    {
        List<FamilleProduitDTO> famillesProduits = new List<FamilleProduitDTO>();
        
        famillesProduits.AddRange(
            _context.FamillesProduits
                .Select(familleProduit => new FamilleProduitDTO
                {
                    Id = familleProduit.Id,
                    Nom = familleProduit.Nom,
                    Description = familleProduit.Description
                })
        );

        return famillesProduits;
    }
    
    public FamilleProduitDTO? Trouver(int id)
    {
        FamilleProduitModel? familleProduit = null;
        familleProduit = _context.FamillesProduits.Find(id);
        
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
    
    public FamilleProduitDTO? Trouver(string nom)
    {
        FamilleProduitModel? familleProduit = null;
        familleProduit = _context.FamillesProduits.FirstOrDefault(familleProduit => familleProduit.Nom == nom);
        
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
    
    public int? Modifier(int id, AjouterFamilleProduitRequest familleProduit)
    {
        FamilleProduitModel? familleProduitAModifier = null;
        familleProduitAModifier = _context.FamillesProduits.Find(id);

        if (familleProduitAModifier == null)
        {
            return null;
        }
        
        familleProduitAModifier.Nom = familleProduit.Nom;
        familleProduitAModifier.Description = familleProduit.Description;
        
        _context.FamillesProduits.Update(familleProduitAModifier);
        _context.SaveChanges();
        
        return familleProduitAModifier.Id;
    }
    
    public void Supprimer(int id)
    {
        FamilleProduitModel? familleProduit = null;
        familleProduit = _context.FamillesProduits.Find(id);
        
        if (familleProduit == null)
        {
            return;
        }
        
        _context.FamillesProduits.Remove(familleProduit);
        _context.SaveChanges();
    }
}