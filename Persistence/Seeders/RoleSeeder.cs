using Microsoft.AspNetCore.Identity;

namespace ApiCube.Persistence.Seeders;

public class RoleSeeder
{
    public static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Client", "Manager", "Employe" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}