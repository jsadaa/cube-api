using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class LigneCommandeClient
{
    public LigneCommandeClient(int quantite, Produit produit)
    {
        Quantite = quantite;
        VerifierQuantite();
        Produit = produit;
        PrixUnitaire = Produit.PrixVente;
        Total = Produit.PrixVente * Quantite;
    }

    public LigneCommandeClient(int id, int quantite, Produit produit)
    {
        Id = id;
        Quantite = quantite;
        Produit = produit;
        VerifierQuantite();
        PrixUnitaire = Produit.PrixVente;
        Total = Produit.PrixVente * Quantite;
    }

    public int Id { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
    public double Total { get; set; }
    public Produit Produit { get; set; }

    public void VerifierQuantite()
    {
        if (Quantite < 0) throw new QuantiteProduitCommandeInvalide();
    }
}