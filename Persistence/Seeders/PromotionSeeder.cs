using ApiCube.Persistence.Models;
using Bogus;

namespace ApiCube.Persistence.Seeders;

public static class PromotionSeeder
{
    public static List<PromotionModel> SeedPromotions(ApiDbContext context, int count = 10)
    {
        if (context.Promotions.Any()) return context.Promotions.ToList();

        var promotionGenerator = new Faker<PromotionModel>()
            .RuleFor(o => o.Nom, f => f.Commerce.ProductAdjective() + " Promo")
            .RuleFor(o => o.Description, f => f.Lorem.Sentence())
            .RuleFor(o => o.DateDebut, f => f.Date.Recent())
            .RuleFor(o => o.DateFin, f => f.Date.Soon(30))
            .RuleFor(o => o.Pourcentage, f => Math.Round(f.Random.Double(5, 50), 2));

        var promotions = promotionGenerator.Generate(count);
        context.Promotions.AddRange(promotions);
        context.SaveChanges();

        return promotions;
    }
}