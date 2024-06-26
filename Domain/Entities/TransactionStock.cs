using ApiCube.Domain.Enums.Stock;

namespace ApiCube.Domain.Entities;

public class TransactionStock
{
    public TransactionStock(int id, int variationQuantite, DateTime date, TypeTransactionStock type, Stock stock,
        double prixUnitaire, int quantiteAvant, int quantiteApres)
    {
        Id = id;
        VariationQuantite = variationQuantite;
        Date = date;
        Type = type;
        Stock = stock;
        PrixUnitaire = prixUnitaire;
        PrixTotal = CalculerPrixTotal();
        QuantiteAvant = quantiteAvant;
        QuantiteApres = quantiteApres;
    }

    public TransactionStock(int variationQuantite, DateTime date, TypeTransactionStock type, Stock stock,
        double prixUnitaire,
        int quantiteAvant, int quantiteApres)
    {
        VariationQuantite = variationQuantite;
        Date = date;
        Type = type;
        Stock = stock;
        PrixUnitaire = prixUnitaire;
        PrixTotal = CalculerPrixTotal();
        QuantiteAvant = quantiteAvant;
        QuantiteApres = quantiteApres;
    }

    public int Id { get; set; }

    public int VariationQuantite { get; set; }

    public DateTime Date { get; set; }

    public TypeTransactionStock Type { get; set; }

    public Stock Stock { get; set; }

    public double PrixUnitaire { get; set; }

    public double PrixTotal { get; set; }

    public int QuantiteAvant { get; set; }

    public int QuantiteApres { get; set; }

    public double CalculerPrixTotal()
    {
        return Math.Abs(VariationQuantite) * PrixUnitaire;
    }

    public bool EstUneEntree()
    {
        return Type is TypeTransactionStock.Achat or TypeTransactionStock.Retour;
    }

    public bool EstUneSortie()
    {
        return Type is TypeTransactionStock.Vente or TypeTransactionStock.Perte or TypeTransactionStock.Vol
            or TypeTransactionStock.Peremption;
    }

    public bool EstUneModificationInterne()
    {
        return Type is TypeTransactionStock.ModificationInterne;
    }

    public bool EstUnePerte()
    {
        return Type is TypeTransactionStock.Perte;
    }

    public bool EstUnVol()
    {
        return Type is TypeTransactionStock.Vol;
    }

    public bool EstUnePeremption()
    {
        return Type is TypeTransactionStock.Peremption;
    }

    public bool EstUneVente()
    {
        return Type is TypeTransactionStock.Vente;
    }

    public bool EstUnAchat()
    {
        return Type is TypeTransactionStock.Achat;
    }

    public bool EstUnRetour()
    {
        return Type is TypeTransactionStock.Retour;
    }

    public bool EstUneEntreeEnStock()
    {
        return EstUneEntree() && !EstUnRetour();
    }

    public bool EstUneSortieDeStock()
    {
        return EstUneSortie() && !EstUnePeremption();
    }

    public bool EstUneSuppression()
    {
        return Type is TypeTransactionStock.Suppression;
    }
}