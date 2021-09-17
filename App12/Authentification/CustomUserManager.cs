using App12.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Authentification
{
    public class CustomUserManager : UserManager<User>
    {
        protected UserUpdateManager<int> _updateManager;
        public CustomUserManager(UserUpdateManager<int> userUpdateManager,IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) 
            :base(store,optionsAccessor,passwordHasher,userValidators,passwordValidators,keyNormalizer,errors,services,logger)
        {
            _updateManager = userUpdateManager;
        }
        public virtual void RolesChanged(User user) 
        {
            _updateManager.Changed(user.Id);
        }
        public override async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            var result = await base.AddToRoleAsync(user, role);
            if (result.Succeeded)
                RolesChanged(user);
            return result;
        }
        public override async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            var result = await base.AddToRolesAsync(user, roles);
            if (result.Succeeded)
                RolesChanged(user);
            return result;
        }
        public override async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            var result = await base.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
                RolesChanged(user);
            return result;
        }
        public override async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        {
            var result = await base.RemoveFromRolesAsync(user, roles);
            if (result.Succeeded)
                RolesChanged(user);
            return result;
        }
    }
}
