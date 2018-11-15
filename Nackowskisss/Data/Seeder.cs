using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nackowskisss.Models;

namespace Nackowskisss.Data
{
    public class Seeder
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Regular").Result)
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = "Regular";

                IdentityResult roleResult = roleManager.CreateAsync(newRole).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = "Admin";

                IdentityResult roleResult = roleManager.CreateAsync(newRole).Result;
            }
        }

        public static void SeedAdminRoleToUser(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = userManager.FindByEmailAsync("denbuw@gmail.com").Result;

            if (user != null)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
