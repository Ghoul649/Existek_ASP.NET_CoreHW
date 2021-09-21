using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App12.Models
{
    public class AppIdentityDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            var res = Database.EnsureCreated();
        }
        public async Task FillEmptyDataBase(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ILogger logger) 
        {
            if (Users.Count() == 0)
            {
                var user = new User()
                {
                    UserName = "test01",
                    RegistrationDate = DateTime.Now,
                    IsCool = true
                };
                await CreateUserAsync(userManager, roleManager, logger, user, "test01", "Admin");
                user = new User()
                {
                    UserName = "test02",
                    RegistrationDate = DateTime.Now - TimeSpan.FromDays(1)
                };
                await CreateUserAsync(userManager, roleManager, logger, user, "test02", "Role1");
                user = new User()
                {
                    UserName = "test03",
                    RegistrationDate = DateTime.Now
                };
                await CreateUserAsync(userManager, roleManager, logger, user, "test03", "Role2");
            }
        }
        private async Task CreateUserAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ILogger logger, User user, string password, params string[] roles) 
        {
            var res = await userManager.CreateAsync(user, password);
            if (!res.Succeeded)
            {
                var strBuilder = new StringBuilder($"Failed to create user '{user.UserName}':");
                foreach (var error in res.Errors)
                    strBuilder.Append($"\t\n{error.Code} - {error.Description};");
                logger.LogError(strBuilder.ToString());
                return;
            }
            logger.LogInformation($"Created user '{user.UserName}' with password '{password}'");

            foreach (var roleName in roles) 
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null) 
                {
                    logger.LogError($"Failed to add user '{user.UserName}' to role '{roleName}': role '{roleName}' is not found");
                    continue;
                }
                var addToRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                if (!addToRoleResult.Succeeded) 
                {
                    var strBuilder = new StringBuilder($"Failed to add user '{user.UserName}' to role '{roleName}':");
                    foreach (var error in res.Errors)
                        strBuilder.Append($"\n\t{error.Code} - {error.Description};");
                    logger.LogError(strBuilder.ToString());
                    continue;
                }
                logger.LogInformation($"User '{user.UserName}' added to role '{role.Name}'");
            }
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
