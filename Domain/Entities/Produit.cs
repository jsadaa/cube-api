using ApiCube.Domain.Exceptions;

namespace ApiCube.Domain.Entities;

public class Produit
{
    public int Id { get; set; } = 0;
    public string Nom { get; set; }
    public string Description { get; set; }
    public string Appellation { get; set; }
    public string Cepage { get; set; }
    public string Region { get; set; }
    public double DegreAlcool { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public bool EnPromotion { get; set; }
    public Promotion? Promotion { get; set; }
    public FamilleProduit FamilleProduit { get; set; }
    public Fournisseur Fournisseur { get; set; }

    public Produit(string nom, string description, string appellation, string cepage, string region, double degreAlcool,
        bool enPromotion, double prixAchat, double prixVente, FamilleProduit familleProduit, Fournisseur fournisseur)
    {
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;

        VerifierValiditePromotion();
    }

    public Produit(int id, string nom, string description, string appellation, string cepage, string region,
        double degreAlcool, bool enPromotion, double prixAchat, double prixVente, FamilleProduit familleProduit,
        Fournisseur fournisseur)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;

        VerifierValiditePromotion();
    }

    public Produit(int id, string nom, string description, string appellation, string cepage, string region,
        double degreAlcool, bool enPromotion, double prixAchat, double prixVente, FamilleProduit familleProduit,
        Fournisseur fournisseur, Promotion promotion)
    {
        Id = id;
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;
        FamilleProduit = familleProduit;
        Fournisseur = fournisseur;
        Promotion = promotion;

        VerifierValiditePromotion();
    }

    public void VerifierValiditePromotion()
    {
        if (Promotion != null && !Promotion.EstValide()) SupprimerPromotion();
    }

    public void AppliquerPromotion(Promotion promotion)
    {
        Promotion = promotion;
        EnPromotion = true;
        PrixVente = CalculerPrixAvecPromotion();
    }

    public void SupprimerPromotion()
    {
        if (Promotion == null) throw new ProduitNonEnPromotion();
        var pourcentageReduction = Promotion.Pourcentage / 100;
        PrixVente /= (1 - pourcentageReduction);
        Promotion = null;
        EnPromotion = false;
    }

    public bool EstEnPromotion()
    {
        return EnPromotion && Promotion != null;
    }

    private double CalculerPrixAvecPromotion()
    {
        double prix = PrixVente;

        if (Promotion != null) prix = PrixVente - (PrixVente * Promotion.Pourcentage / 100);

        return prix;
    }

    public void MettreAJour(string nom, string description, string appellation, string cepage, string region,
        double degreAlcool, double prixAchat, double prixVente, bool enPromotion)
    {
        Nom = nom;
        Description = description;
        Appellation = appellation;
        Cepage = cepage;
        Region = region;
        DegreAlcool = degreAlcool;
        PrixAchat = prixAchat;
        PrixVente = prixVente;
        EnPromotion = enPromotion;

        VerifierValiditePromotion();
    }
}