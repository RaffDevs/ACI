using System;
using Microsoft.AspNetCore.Identity;

namespace ACI.Webapp.Infrastructure.Configuration;

public class Seeder
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = new IdentityUser { UserName = "admin@mail.com", Email = "admin@mail.com" };
        string adminPassword = "admin@admin"; 
        
        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
