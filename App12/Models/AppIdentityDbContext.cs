using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Models
{
    public class AppIdentityDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            var res = Database.EnsureCreated();
        }
        public static async Task CheckBaseRoles(RoleManager<IdentityRole<int>> roleManager, ILogger logger)
        {
            await EnsureRoleCreated(roleManager, "Role1", logger);
            await EnsureRoleCreated(roleManager, "Role2", logger);
            await EnsureRoleCreated(roleManager, "Admin", logger);
        }
        private static async Task<bool> EnsureRoleCreated(RoleManager<IdentityRole<int>> roleManager, string RoleName,ILogger logger)
        {
            if (!await roleManager.RoleExistsAsync(RoleName))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(RoleName));
                logger.LogInformation($"Role \"{RoleName}\" created");
                return false;
            }
            return true;
        }
    }
}
