using ApiCube.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Seeders;

public class ClientSeeder
{
    public static async Task CreateClients(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApiDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUserModel>>();

        if (context.Clients.Any()) return;

        var applicationUser1 = new ApplicationUserModel
        {
            UserName = "MichelGagnant",
            Email = "michel@mail.fr",
            EmailConfirmed = true
        };

        var password1 = "Doudou58!";

        await userManager.CreateAsync(applicationUser1, password1);

        var userId1 = await userManager.GetUserIdAsync(applicationUser1);

        var client = new ClientModel
        {
            Nom = "Michel",
            Prenom = "Gagnant",
            Email = "michel@mail.fr",
            Adresse = "12, rue des fleurs",
            CodePostal = "75000",
            Ville = "Paris",
            Pays = "France",
            Telephone = "0123456789",
            DateNaissance = new DateTime(1980, 1, 1),
            DateInscription = DateTime.Now,
            ApplicationUserId = userId1
        };

        context.Clients.Add(client);
        context.SaveChanges();
    }
}
