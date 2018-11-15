using Microsoft.AspNetCore.Identity;
using Nackowskisss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskisss.Data
{
    public interface ISeeder
    {
        void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager);

        void SeedAdminRoleToUser(UserManager<ApplicationUser> userManager);

        void SeedRoles(RoleManager<IdentityRole> roleManager);
    }
}
