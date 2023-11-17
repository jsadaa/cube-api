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

    public DateTime DateModification { get; set; }

    public DateTime? DateSuppression { get; set; } = null;

    public Stock(int id, int quantite, int seuilDisponibilite, StatutStock statut, Produit produit,
        List<TransactionStock> transactionStocks, DateTime dateCreation,
        DateTime dateModification, DateTime? dateSuppression)
    {
        Id = id;
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Statut = statut;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
        MettreAJourStatut();
    }

    public Stock(int quantite, int seuilDisponibilite, Produit produit, List<TransactionStock> transactionStocks,
        DateTime dateCreation, DateTime dateModification, DateTime? dateSuppression)
    {
        Quantite = quantite;
        SeuilDisponibilite = seuilDisponibilite;
        Produit = produit;
        Transactions = transactionStocks;
        DateCreation = dateCreation;
        DateModification = dateModification;
        DateSuppression = dateSuppression;
        MettreAJourStatut();
    }

    public bool EstSupprime()
    {
        return Statut == StatutStock.Supprime;
    }

    public bool EstEnCommande()
    {
        return Statut == StatutStock.EnCommande;
    }

    public bool EstDisponible()
    {
        return Statut == StatutStock.EnStock;
    }

    public bool EstEnRupture()
    {
        return Quantite <= SeuilDisponibilite;
    }

    public bool EstEnStock()
    {
        return EstDisponible() && !EstEnRupture();
    }

    private void AjouterQuantite(int quantite)
    {
        Quantite += quantite;
        MettreAJourStatut();
    }

    private void RetirerQuantite(int quantite)
    {
        quantite = Math.Abs(quantite);
        Quantite -= quantite;
        MettreAJourStatut();
    }

    public void ModifierSeuilDisponibilite(int seuilDisponibilite)
    {
        SeuilDisponibilite = seuilDisponibilite;
        MettreAJourStatut();
    }

    private void MettreAJourStatut()
    {
        if (EstSupprime()) return;
        if (Quantite > SeuilDisponibilite) Statut = StatutStock.EnStock;
        else if (Quantite <= SeuilDisponibilite || !EstEnCommande()) Statut = StatutStock.Indisponible;
        else if (Quantite <= 0) Statut = StatutStock.EnRuptureDeStock;
        if (Produit.EstPerime()) Statut = StatutStock.Perime;
    }

    public bool DoitEtreRecommande()
    {
        return EstEnRupture();
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
        else if (transactionStock.EstUneSuppression()) MarquerCommeSupprime();

        MettreAJourStatut();
    }

    private void MarquerCommeSupprime()
    {
        Quantite = 0;
        DateSuppression = DateTime.Now;
        Statut = StatutStock.Supprime;
    }
}