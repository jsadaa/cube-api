using ApiCube.Persistence.Models;
using Bogus;

namespace ApiCube.Persistence.Seeders;

public static class FournisseurSeeder
{
    public static List<FournisseurModel> SeedFournisseurs(ApiDbContext context, int count = 10)
    {
        if (context.Fournisseurs.Any()) return context.Fournisseurs.ToList();

        var fournisseurGenerator = new Faker<FournisseurModel>()
            .RuleFor(o => o.Nom, f => f.Company.CompanyName())
            .RuleFor(o => o.Adresse, f => f.Address.StreetAddress())
            .RuleFor(o => o.CodePostal, f => f.Address.ZipCode())
            .RuleFor(o => o.Ville, f => f.Address.City())
            .RuleFor(o => o.Pays, f => f.Address.Country())
            .RuleFor(o => o.Telephone, f => f.Phone.PhoneNumber())
            .RuleFor(o => o.Email, f => f.Internet.Email());

        var fournisseurs = fournisseurGenerator.Generate(count);
        context.Fournisseurs.AddRange(fournisseurs);
        context.SaveChanges();

        return fournisseurs;
    }
}