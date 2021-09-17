using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Filters
{
    public class RoleFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly UserManager<Models.User> _manager;
        private readonly string _role;
        public RoleFilter(UserManager<Models.User> urm, string Role) 
        {
            _manager = urm;
            _role = Role;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = await _manager.GetUserAsync(context.HttpContext.User);
            if (user == null)
                context.Result = new UnauthorizedResult();
            if (!await _manager.IsInRoleAsync(user, _role))
                context.Result = new NotFoundResult();
        }
    }
}
