using ApiCube.Persistence.Models;
using Bogus;

namespace ApiCube.Persistence.Seeders;

public static class ProduitSeeder
{
    public static List<ProduitModel> SeedProduits(ApiDbContext context, List<FamilleProduitModel> familles, List<FournisseurModel> fournisseurs, int count = 50)
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

        var uniqueProductNames = new HashSet<string>(context.Produits.Select(p => p.Nom));
        var produitGenerator = new Faker<ProduitModel>()
            .CustomInstantiator(f =>
            {
                ProduitModel produit;
                do
                {
                    produit = new ProduitModel
                    {
                        Nom = "Château " + f.Name.LastName(),
                        Description = f.Lorem.Sentence(),
                        Appellation = f.PickRandom(appellations),
                        Cepage = f.PickRandom(cepages),
                        Region = f.PickRandom(regions),
                        DegreAlcool = Math.Round(f.Random.Double(10, 20), 2),
                        PrixAchat = Math.Round(f.Random.Double(5, 10), 2),
                        PrixVente = Math.Round(f.Random.Double(11, 20), 2),
                        EnPromotion = false,
                        FamilleProduitId = f.PickRandom(familles).Id,
                        FournisseurId = f.PickRandom(fournisseurs).Id
                    };
                } while (uniqueProductNames.Contains(produit.Nom));

                uniqueProductNames.Add(produit.Nom);
                return produit;
            });

        var produits = produitGenerator.Generate(count);
        context.Produits.AddRange(produits);
        context.SaveChanges();

        return produits;
    }
}