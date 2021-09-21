using App12.Policy.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Policy
{
    public static class ServiceCollectionExtension
    {
        public static void AddAuthorizationHandlers(this IServiceCollection services) 
        {
            services.AddSingleton<IAuthorizationHandler, MemberOfRolesHandler>();
        }
    }
}
