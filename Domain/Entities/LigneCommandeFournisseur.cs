using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class LigneCommandeFournisseur
{
    public LigneCommandeFournisseur(int quantite, double prixUnitaire, double remise, double total, Produit produit,
        int commandeFournisseurId)
    {
        Quantite = quantite;
        PrixUnitaire = prixUnitaire;
        Remise = remise;
        Total = total;
        Produit = produit;
        CommandeFournisseurId = commandeFournisseurId;
    }

    public LigneCommandeFournisseur(int id, int quantite, double prixUnitaire, double remise, double total,
        Produit produit, int commandeFournisseurId)
    {
        Id = id;
        Quantite = quantite;
        PrixUnitaire = prixUnitaire;
        Remise = remise;
        Total = total;
        Produit = produit;
        CommandeFournisseurId = commandeFournisseurId;
    }

    public int Id { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
    public double Remise { get; set; }
    public double Total { get; set; }
    public Produit Produit { get; set; }
    public int CommandeFournisseurId { get; set; }

    public void MettreAJour(int quantite, double prixUnitaire, double remise, double total, Produit produit,
        int commandeFournisseurId)
    {
        Quantite = quantite;
        PrixUnitaire = prixUnitaire;
        Remise = remise;
        Total = total;
        Produit = produit;
        CommandeFournisseurId = commandeFournisseurId;
    }

    private void VerifierValiditeQuantite()
    {
        if (Quantite <= 0) throw new QuantiteProduitCommandeInvalide();
    }

    public void VerifierValidite()
    {
        VerifierValiditeQuantite();
    }
}