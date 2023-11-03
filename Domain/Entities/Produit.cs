using ApiCube.DTOs.Requests;
using ApiCube.DTOs.Responses;

namespace ApiCube.Domain.Entities;

public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public int Quantite { get; set; }
    public int SeuilDisponibilite { get; set; }
    public string StatutStock { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public DateTime DateAchat { get; set; }
    public DateTime DatePeremption { get; set; }
    public Promotion? Promotion { get; set; }
    public FamilleProduit FamilleProduit { get; set; }
    public Fournisseur Fournisseur { get; set; }
    
    public Produit(int id, string nom, string description, int quantite, int seuilDisponibilite, string statutStock, double prixAchat, double prixVente, DateTime dateAchat, DateTime datePeremption, FamilleProduit familleProduit, Fournisseur fournisseur)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        StatutStock = statutStock;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        DateAchat = dateAchat;
        DatePeremption = datePeremption;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;
    }
    
    public void MettreStockAjour(int quantite)
    {
        Quantite = quantite;
    }   
    
    public void AppliquerPromotion(Promotion promotion)
    {
        Promotion = promotion;
    }
    
    public bool EstDisponible()
    {
        return Quantite > SeuilDisponibilite;
    }
    
    public bool EstEnPromotion()
    {
        return Promotion != null && Promotion.EstValide();
    }
    
    public bool EstPerime()
    {
        return DatePeremption < DateTime.Now;
    }
    
    public bool EstEnRuptureDeStock()
    {
        return Quantite == 0;
    }
    
    public bool EstEnStock()
    {
        return Quantite > 0;
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
            Quantite = Quantite,
            SeuilDisponibilite = SeuilDisponibilite,
            StatutStock = StatutStock,
            PrixAchat = PrixAchat,
            PrixVente = PrixVente,
            DateAchat = DateAchat,
            DatePeremption = DatePeremption,
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
            Quantite = Quantite,
            SeuilDisponibilite = SeuilDisponibilite,
            StatutStock = StatutStock,
            PrixAchat = PrixAchat,
            PrixVente = PrixVente,
            DateAchat = DateAchat,
            DatePeremption = DatePeremption,
            FamilleProduitId = FamilleProduit.Id,
            FournisseurId = Fournisseur.Id
        };
    }
    
    
}