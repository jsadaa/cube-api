using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Models;

namespace ApiCube.Persistence.Repositories.Fournisseur;

public class FournisseurRepository : IFournisseurRepository
{
    private readonly ApiDbContext _context;
    
    public FournisseurRepository(ApiDbContext context)
    {
        _context = context;
    }
    
    public int Ajouter(AjouterFournisseurRequest fournisseur)
    {
        FournisseurModel nouveauFournisseur = new FournisseurModel
        {
            Nom = fournisseur.Nom,
            Adresse = fournisseur.Adresse,
            Telephone = fournisseur.Telephone,
            Email = fournisseur.Email
        };
        
        _context.Fournisseurs.Add(nouveauFournisseur);
        _context.SaveChanges();
        
        return nouveauFournisseur.Id;
    }
    
    public List<FournisseurDTO> Lister()
    {
        List<FournisseurDTO> fournisseurs = new List<FournisseurDTO>();
        
        fournisseurs.AddRange(
            _context.Fournisseurs
                .Select(fournisseur => new FournisseurDTO
                {
                    Id = fournisseur.Id,
                    Nom = fournisseur.Nom,
                    Adresse = fournisseur.Adresse,
                    Telephone = fournisseur.Telephone,
                    Email = fournisseur.Email
                })
        );
        
        return fournisseurs;
    }
    
    public FournisseurDTO? Trouver(int id)
    {
        FournisseurModel? fournisseur = _context.Fournisseurs.Find(id);
        
        if (fournisseur == null)
        {
            return null;
        }
        
        return new FournisseurDTO
        {
            Id = fournisseur.Id,
            Nom = fournisseur.Nom,
            Adresse = fournisseur.Adresse,
            Telephone = fournisseur.Telephone,
            Email = fournisseur.Email
        };
    }
    
    public FournisseurDTO? Trouver(string nom)
    {
        FournisseurModel? fournisseur = _context.Fournisseurs.FirstOrDefault(fournisseur => fournisseur.Nom == nom);
        
        if (fournisseur == null)
        {
            return null;
        }
        
        return new FournisseurDTO
        {
            Id = fournisseur.Id,
            Nom = fournisseur.Nom,
            Adresse = fournisseur.Adresse,
            Telephone = fournisseur.Telephone,
            Email = fournisseur.Email
        };
    }
    
    public int? Modifier(int id, AjouterFournisseurRequest fournisseur)
    {
        FournisseurModel? fournisseurAModifier = _context.Fournisseurs.Find(id);
        
        if (fournisseurAModifier == null)
        {
            return null;
        }
        
        fournisseurAModifier.Nom = fournisseur.Nom;
        fournisseurAModifier.Adresse = fournisseur.Adresse;
        fournisseurAModifier.Telephone = fournisseur.Telephone;
        fournisseurAModifier.Email = fournisseur.Email;
        
        _context.SaveChanges();
        
        return fournisseurAModifier.Id;
    }
    
    public void Supprimer(int id)
    {
        FournisseurModel? fournisseurASupprimer = _context.Fournisseurs.Find(id);
        
        if (fournisseurASupprimer == null)
        {
            return;
        }
        
        _context.Fournisseurs.Remove(fournisseurASupprimer);
        _context.SaveChanges();
    }
}