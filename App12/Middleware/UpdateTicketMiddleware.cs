using App12.Authentification;
using App12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App12.Middleware
{
    public class UpdateTicketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserUpdateManager<int> _userUpdateManager;
        public UpdateTicketMiddleware(RequestDelegate next, UserUpdateManager<int> userUpdateManager)
        {
            _next = next;
            _userUpdateManager = userUpdateManager;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var claim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                await _next(context);
                return;
            }
            var id = Int32.Parse(claim);
            if (_userUpdateManager.NeedsToBeUpdated(id)) 
            {
                var manager = context.RequestServices.GetService(typeof(SignInManager<User>)) as SignInManager<User>;
                await manager.SignOutAsync();
                context.Response.Redirect("/Account/Login");
                return;
            }
            await _next(context);
        }
    }
}
