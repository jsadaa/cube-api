using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Seeders;

public class RoleSeeder
{
    public async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Client", "Manager", "Employe" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Créer les rôles et les ajouter à la base de données
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}