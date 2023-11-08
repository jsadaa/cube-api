using ApiCube.Persistence.Models;
using Bogus;

namespace ApiCube.Persistence.Seeders;

public static class FamilleProduitSeeder
{
    public static List<FamilleProduitModel> SeedFamilleProduits(ApiDbContext context, int count = 10)
    {
        if (context.FamillesProduits.Any()) return context.FamillesProduits.ToList();

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

        var familleProduitGenerator = new Faker<FamilleProduitModel>()
            .CustomInstantiator(f =>
            {
                var nom = f.PickRandom(wineFamilies);
                wineFamilies.Remove(nom);
                return new FamilleProduitModel
                {
                    Nom = nom,
                    Description = f.Lorem.Sentence()
                };
            });

        var familleProduits = familleProduitGenerator.Generate(count);
        context.FamillesProduits.AddRange(familleProduits);
        context.SaveChanges();

        return familleProduits;
    }
}