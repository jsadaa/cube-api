using ApiCube.Persistence.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ApiCube.Persistence.Seeders;

public static class ProduitSeeder
{
    public static List<ProduitModel> SeedProduits(ApiDbContext context, List<FamilleProduitModel> familles,
        List<FournisseurModel> fournisseurs, int count = 20)
    {
        if (context.Produits.Any()) return context.Produits.ToList();

        var appellations = new List<string>
        {
            "AOC",
            "IGP",
            "Vin de France"
        };

        var cepages = new List<string>
        {
            "Cabernet Franc",
            "Cabernet Sauvignon",
            "Carmenère",
            "Chardonnay",
            "Chenin",
            "Cinsault",
            "Clairette",
            "Colombard",
            "Cot",
            "Folle Blanche",
            "Gamay",
            "Grenache",
            "Malbec",
            "Melon de Bourgogne",
            "Merlot",
            "Mourvèdre",
            "Muscat",
            "Petit Manseng",
            "Petit Verdot",
            "Pinot Blanc",
            "Pinot Gris",
            "Pinot Meunier",
            "Pinot Noir",
            "Riesling",
            "Sauvignon",
            "Sauvignon Blanc",
            "Sémillon",
            "Syrah",
            "Tannat",
            "Ugni Blanc",
            "Viognier"
        };

        var regions = new List<string>
        {
            "Alsace",
            "Beaujolais",
            "Bordeaux",
            "Bourgogne",
            "Champagne",
            "Corse",
            "Jura",
            "Languedoc-Roussillon",
            "Lorraine",
            "Provence",
            "Savoie",
            "Sud-Ouest",
            "Vallée de la Loire",
            "Vallée du Rhône"
        };

        var existingProducts = context.Produits
            .AsNoTracking()
            .ToList()
            .Select(p => (p.Nom, p.Annee))
            .ToHashSet();

        var uniqueNameYearPairs = existingProducts;
        var produitGenerator = new Faker<ProduitModel>()
            .CustomInstantiator(f =>
            {
                string nom;
                int annee;
                double prixAchat, prixVente;
                do
                {
                    nom = "Château " + f.Name.LastName();
                    annee = f.Date.Past(50).Year;
                } while (uniqueNameYearPairs.Contains((nom, annee)));

                uniqueNameYearPairs.Add((nom, annee));

                prixAchat = Math.Round(f.Random.Double(5, 50), 2);
                prixVente = Math.Round(prixAchat + f.Random.Double(3, 20), 2);
                var familleProduit = f.PickRandom(familles);
                var fournisseur = f.PickRandom(fournisseurs);

                return new ProduitModel
                {
                    Nom = nom,
                    Description = f.Lorem.Sentence(),
                    Appellation = f.PickRandom(appellations),
                    Cepage = f.PickRandom(cepages),
                    Region = f.PickRandom(regions),
                    Annee = annee,
                    DegreAlcool = Math.Round(f.Random.Double(10, 20), 2),
                    PrixAchat = prixAchat,
                    PrixVente = prixVente,
                    DatePeremption = f.Date.Future(100),
                    EnPromotion = false,
                    FamilleProduitId = familleProduit.Id,
                    FournisseurId = fournisseur.Id,
                    FamilleProduit = familleProduit,
                    Fournisseur = fournisseur
                };
            });

        var produits = produitGenerator.Generate(count);
        context.Produits.AddRange(produits);
        context.SaveChanges();

        return produits;
    }
}