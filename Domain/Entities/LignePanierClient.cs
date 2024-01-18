using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class LignePanierClient
{
    public int Id { get; set; }
    public Produit Produit { get; set; }
    public int Quantite { get; set; }

    public LignePanierClient(Produit produit, int quantite)
    {
        Produit = produit;
        Quantite = quantite;
        VerifierQuantite();
    }

    public LignePanierClient(int id, Produit produit, int quantite)
    {
        Id = id;
        Produit = produit;
        Quantite = quantite;
        VerifierQuantite();
    }

    private void VerifierQuantite()
    {
        if (Quantite < 0)
        {
            throw new QuantitePanierInvalide();
        }
    }
}