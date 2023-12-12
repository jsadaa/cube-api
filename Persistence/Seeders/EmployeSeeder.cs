using ApiCube.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Seeders;

public class EmployeSeeder
{
    public static async Task CreateEmployes(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApiDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUserModel>>();

        if (context.Employes.Any()) return;

        var applicationUser1 = new ApplicationUserModel
        {
            UserName = "Admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };

        var applicationUser2 = new ApplicationUserModel
        {
            UserName = "Client",
            Email = "client@gmail.com",
            EmailConfirmed = true
        };

        var password1 = "Admin123!";
        var password2 = "Client123!";

        await userManager.CreateAsync(applicationUser1, password1);
        await userManager.CreateAsync(applicationUser2, password2);

        var userId1 = await userManager.GetUserIdAsync(applicationUser1);
        var userId2 = await userManager.GetUserIdAsync(applicationUser2);

        var employe1 = new EmployeModel
        {
            Nom = "Admin",
            Prenom = "Admin",
            Email = "admin@gmail.com",
            DateEmbauche = DateTime.Now,
            Poste = "Responsable",
            ApplicationUserId = userId1
        };

        var employe2 = new EmployeModel
        {
            Nom = "Employe",
            Prenom = "Employe",
            Email = "employe@gmail.com",
            DateEmbauche = DateTime.Now,
            Poste = "Saisonnier",
            ApplicationUserId = userId2
        };

        context.Employes.Add(employe1);
        context.Employes.Add(employe2);
        context.SaveChanges();
    }
}