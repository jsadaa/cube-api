using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Entities;

public class Stock
{
    public int Id { get; set; }
    
    public int Quantite { get; set; }
    
    public int SeuilDisponibilite { get; set; }
    
    public StatutStock Statut { get; set; }
    
    public Produit Produit { get; set; }
    
    public List<TransactionStock?> Transactions { get; set; }
    
    public DateTime DateCreation { get; set; }
    
    public DateTime DatePeremption { get; set; }
    
    public DateTime DateModification { get; set; }
    
    public DateTime? DateSuppression { get; set; }
    
    public Stock(int id, int quantite, int seuilDisponibilite, StatutStock statut, Produit produit, List<TransactionStock> transactionStocks, DateTime dateCreation, DateTime datePeremption, DateTime dateModification, DateTime? dateSuppression)
    {
        Id = id;
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Statut = statut;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DatePeremption = datePeremption;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
    }
    
    public Stock(int quantite, int seuilDisponibilite, StatutStock statut, Produit produit, List<TransactionStock> transactionStocks, DateTime dateCreation, DateTime datePeremption, DateTime dateModification, DateTime? dateSuppression)
    {
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Statut = statut;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DatePeremption = datePeremption;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
    }
    
    public bool EstDisponible()
    {
        return Statut == StatutStock.EnStock;
    }
    
    public bool EstPerime()
    {
        return DatePeremption < DateTime.Now;
    }
    
    public bool EstEnRupture()
    {
        return Quantite <= SeuilDisponibilite;
    }
    
    public bool EstEnStock()
    {
        return EstDisponible() && !EstPerime() && !EstEnRupture();
    }
    
    public void AjouterQuantite(int quantite)
    {
        Quantite += quantite;
        AdapterStatut();
    }
    
    public void RetirerQuantite(int quantite)
    {
        Quantite -= quantite;
        AdapterStatut();
    }
    
    public void ModifierSeuilDisponibilite(int seuilDisponibilite)
    {
        SeuilDisponibilite = seuilDisponibilite;
        AdapterStatut();
    }
    
    private void AdapterStatut()
    {
        if (Quantite > SeuilDisponibilite) Statut = StatutStock.EnStock;
        else if (Quantite <= SeuilDisponibilite || Statut != StatutStock.EnCommande) Statut = StatutStock.Indisponible;
        else if (Quantite <= 0) Statut = StatutStock.EnRuptureDeStock;
    }
    
    public bool DoitEtreRecommande()
    {
        return EstEnRupture() || EstPerime();
    }
    
    public AjouterStockRequest ToRequestDTO()
    {
        return new AjouterStockRequest
        {
            Quantite = Quantite,
            SeuilDisponibilite = SeuilDisponibilite,
            ProduitId = Produit.Id,
            DateCreation = DateCreation,
            DatePeremption = DatePeremption,
            DateModification = DateModification,
            DateSuppression = DateSuppression
        };
    }
    
    public StockDTO ToResponseDTO()
    {
        return new StockDTO
        {
            Id = Id,
            Quantite = Quantite,
            SeuilDisponibilite = SeuilDisponibilite,
            Statut = Statut.ToString(),
            Produit = Produit.ToResponseDTO(),
            DateCreation = DateCreation,
            DatePeremption = DatePeremption,
            DateModification = DateModification,
            DateSuppression = DateSuppression
        };
    }
}