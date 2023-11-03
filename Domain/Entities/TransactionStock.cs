using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Entities;

public class TransactionStock
{
    
    public int Id { get; set; }
    
    public int Quantite { get; set; }
    
    public DateTime Date { get; set; }
    
    public TypeTransactionStock Type { get; set; }
    
    public Produit Produit { get; set; }
    
    public double PrixUnitaire { get; set; }
    
    public double PrixTotal { get; set; }
    
    public TransactionStock(int id, int quantite, DateTime date, TypeTransactionStock type, Produit produit, double prixUnitaire)
    {
        Id = id;
        Quantite = quantite;
        Date = date;
        Type = type;
        Produit = produit;
        PrixUnitaire = prixUnitaire;
        PrixTotal = CalculerPrixTotal();
    }
    
    public TransactionStock(int quantite, DateTime date, TypeTransactionStock type, Produit produit, double prixUnitaire)
    {
        Quantite = quantite;
        Date = date;
        Type = type;
        Produit = produit;
        PrixUnitaire = prixUnitaire;
        PrixTotal = CalculerPrixTotal();
    }
    
    public void MettreAJourQuantite(int quantite)
    {
        Quantite = quantite;
    }
    
    public double CalculerPrixTotal()
    {
        return Quantite * PrixUnitaire;
    }
    
    public bool EstUneEntree()
    {
        return Type == TypeTransactionStock.Achat || Type == TypeTransactionStock.Retour;
    }
    
    public bool EstUneSortie()
    {
        return Type == TypeTransactionStock.Vente || Type == TypeTransactionStock.Perte || Type == TypeTransactionStock.Vol || Type == TypeTransactionStock.Peremption;
    }
    
    public bool EstUnePerte()
    {
        return Type == TypeTransactionStock.Perte;
    }
    
    public bool EstUnVol()
    {
        return Type == TypeTransactionStock.Vol;
    }
    
    public bool EstUnePeremption()
    {
        return Type == TypeTransactionStock.Peremption;
    }
    
    public bool EstUneVente()
    {
        return Type == TypeTransactionStock.Vente;
    }
    
    public bool EstUnAchat()
    {
        return Type == TypeTransactionStock.Achat;
    }
    
    public bool EstUnRetour()
    {
        return Type == TypeTransactionStock.Retour;
    }
    
    public bool EstUneEntreeEnStock()
    {
        return EstUneEntree() && !EstUnRetour();
    }
    
    public bool EstUneSortieDeStock()
    {
        return EstUneSortie() && !EstUnePeremption();
    }
    
    public TransactionStockDTO ToDTO()
    {
        return new TransactionStockDTO
        {
            Id = Id,
            Quantite = Quantite,
            Date = Date,
            Type = Type.ToString(),
            ProduitId = Produit.Id,
            PrixUnitaire = PrixUnitaire,
            PrixTotal = PrixTotal,
        };
    }
}