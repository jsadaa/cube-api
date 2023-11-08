using ApiCube.Persistence.Models;
using Bogus;

namespace ApiCube.Persistence.Seeders;

public static class FamilleProduitSeeder
{
    public static List<FamilleProduitModel> SeedFamilleProduits(ApiDbContext context, int count = 10)
    {
        if (context.FamillesProduits.Any()) return context.FamillesProduits.ToList();
        
        // create a liste fo 10 wine families (vin rouge, vin blanc, etc.)
        var wineFamilies = new List<string>
        {
            "Vin rouge",
            "Vin blanc",
            "Vin rosé",
            "Vin pétillant",
            "Vin doux",
            "Vin de dessert",
            "Vin de glace",
            "Vin de liqueur",
            "Vin de fruit",
            "Vin de fleur"
        };
        
        // create count products families from the list of wine families (if one is picked, remove it from the list)
        var familleProduitGenerator = new Faker<FamilleProduitModel>()
            .CustomInstantiator(f =>
            {
                var nom = f.PickRandom(wineFamilies);
                wineFamilies.Remove(nom);
                return new FamilleProduitModel
                {
                    Nom = nom,
                    Description = f.Commerce.ProductDescription()
                };
            });

        /*var uniqueNames = new HashSet<string>(context.FamillesProduits.Select(fp => fp.Nom));
        var familleProduitGenerator = new Faker<FamilleProduitModel>()
            .CustomInstantiator(f =>
            {
                string nom;
                do
                {
                    nom = f.PickRandom(wineFamilies);
                } while (uniqueNames.Contains(nom));

                uniqueNames.Add(nom); // Add the unique name to the set.

                return new FamilleProduitModel
                {
                    Nom = nom,
                    Description = f.Commerce.ProductDescription()
                };
            });*/

        var familleProduits = familleProduitGenerator.Generate(count);
        context.FamillesProduits.AddRange(familleProduits);
        context.SaveChanges();

        return familleProduits;
    }
}