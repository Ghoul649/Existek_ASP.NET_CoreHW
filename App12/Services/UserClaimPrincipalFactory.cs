using App12.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App12.Services
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole<int>>
    {
        public AppUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("IsCool", user.IsCool.ToString()));
            identity.AddClaim(new Claim("RegistrationDate", user.RegistrationDate.ToString()));
            return identity;
        }
    }
}
