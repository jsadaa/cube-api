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
    
    public Produit(string nom, string description, int quantite, int seuilDisponibilite, string statutStock, double prixAchat, double prixVente, DateTime dateAchat, DateTime datePeremption, FamilleProduit familleProduit)
    {
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
        return Promotion != null;
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
    
    
}