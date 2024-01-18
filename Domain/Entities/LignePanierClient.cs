using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class LignePanierClient
{
    public LignePanierClient(Produit produit, int quantite)
    {
        Produit = produit;
        Quantite = quantite;
        VerifierQuantite();
        PrixUnitaire = produit.PrixVente;
        Total = PrixUnitaire * Quantite;
    }

    public LignePanierClient(int id, Produit produit, int quantite)
    {
        Id = id;
        Produit = produit;
        Quantite = quantite;
        VerifierQuantite();
        PrixUnitaire = produit.PrixVente;
        Total = PrixUnitaire * Quantite;
    }

    public int Id { get; set; }
    public Produit Produit { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
    public double Total { get; set; }

    private void VerifierQuantite()
    {
        if (Quantite < 0) throw new QuantitePanierInvalide();
    }

    public void ModifierQuantite(int quantite)
    {
        Quantite = quantite;
        VerifierQuantite();
        Total = PrixUnitaire * Quantite;
    }
}