using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;

namespace ApiCube.Domain.Entities;

public class Produit
{
    public int Id { get; set; } = 0;
    public string Nom { get; set; }
    public string Description { get; set; }
    public string Appellation { get; set; }
    public string Cepage { get; set; }
    public string Region { get; set; }
    public double DegreAlcool { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public bool EnPromotion { get; set; }
    public Promotion? Promotion { get; set; }
    public FamilleProduit FamilleProduit { get; set; }
    public Fournisseur Fournisseur { get; set; }
    
    public Produit(string nom, string description,string appellation, string cepage, string region, double degreAlcool, bool enPromotion, double prixAchat, double prixVente, FamilleProduit familleProduit, Fournisseur fournisseur)
    {
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;
    }
    
    public Produit(int id, string nom, string description,string appellation, string cepage, string region, double degreAlcool, bool enPromotion, double prixAchat, double prixVente, FamilleProduit familleProduit, Fournisseur fournisseur)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;
    }
    
    public void AppliquerPromotion(Promotion promotion)
    {
        Promotion = promotion;
    }
    
    public bool EstEnPromotion()
    {
        return EnPromotion && Promotion != null;
    }
    
    private double CalculerPrixAvecPromotion()
    {
        double prix = PrixVente;
        
        if (Promotion != null)
        {
            prix = PrixVente - (PrixVente * Promotion.Pourcentage / 100);
        }
        
        return prix;
    }
    
    public double CalculerPrixDeVente()
    {
        return EstEnPromotion() ? CalculerPrixAvecPromotion() : PrixVente;
    }
    
    public ProduitDTO ToResponseDTO()
    {
        return new ProduitDTO
        {
            Id = Id,
            Nom = Nom,
            Description = Description,
            Appellation = Appellation,
            Cepage = Cepage,
            Region = Region,
            DegreAlcool = DegreAlcool,
            PrixAchat = PrixAchat,
            PrixVente = PrixVente,
            FamilleProduitNom = FamilleProduit.Nom,
            FournisseurNom = Fournisseur.Nom
        };
    }
    
    public AjouterProduitRequest ToRequestDTO()
    {
        return new AjouterProduitRequest
        {
            Nom = Nom,
            Description = Description,
            Appellation = Appellation,
            Cepage = Cepage,
            Region = Region,
            DegreAlcool = DegreAlcool,
            PrixAchat = PrixAchat,
            PrixVente = PrixVente,
            FamilleProduitId = FamilleProduit.Id,
            FournisseurId = Fournisseur.Id
        };
    }
}