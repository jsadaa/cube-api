using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Entities;

public class Stock
{
    public int Id { get; set; } = 0;

    public int Quantite { get; set; }

    public int SeuilDisponibilite { get; set; }

    public StatutStock Statut { get; set; } = StatutStock.EnStock;

    public Produit Produit { get; set; }

    public List<TransactionStock> Transactions { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime DatePeremption { get; set; }

    public DateTime DateModification { get; set; }

    public DateTime? DateSuppression { get; set; }

    public Stock(int id, int quantite, int seuilDisponibilite, Produit produit,
        List<TransactionStock> transactionStocks, DateTime dateCreation, DateTime datePeremption,
        DateTime dateModification, DateTime? dateSuppression)
    {
        Id = id;
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DatePeremption = datePeremption;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
        AdapterStatut();
    }

    public Stock(int quantite, int seuilDisponibilite, Produit produit, List<TransactionStock> transactionStocks,
        DateTime dateCreation, DateTime datePeremption, DateTime dateModification, DateTime? dateSuppression)
    {
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DatePeremption = datePeremption;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
        AdapterStatut();
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

    private void AjouterQuantite(int quantite)
    {
        Quantite += quantite;
        AdapterStatut();
    }

    private void RetirerQuantite(int quantite)
    {
        quantite = Math.Abs(quantite);
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

    public void AjouterTransaction(TransactionStock transactionStock)
    {
        Transactions.Add(transactionStock);
        if (transactionStock.EstUneSortie()) RetirerQuantite(transactionStock.Quantite);
        else if (transactionStock.EstUneEntree()) AjouterQuantite(transactionStock.Quantite);
        else if (transactionStock.EstUneModificationInterne() && transactionStock.Quantite > 0)
            AjouterQuantite(transactionStock.Quantite);
        else if (transactionStock.EstUneModificationInterne() && transactionStock.Quantite < 0)
            RetirerQuantite(transactionStock.Quantite);
        AdapterStatut();
    }

    public void MettreAJour(Stock stock)
    {
        Quantite = stock.Quantite;
        SeuilDisponibilite = stock.SeuilDisponibilite;
        Produit = stock.Produit;
        DatePeremption = stock.DatePeremption;
        DateModification = stock.DateModification;
        DateSuppression = stock.DateSuppression;
        AdapterStatut();
    }

    public void ModifierDatePeremption(DateTime stockUpdateDatePeremption)
    {
        DatePeremption = stockUpdateDatePeremption;
        AdapterStatut();
    }
}