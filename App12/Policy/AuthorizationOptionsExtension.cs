using App12.Policy.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Policy
{
    public static class AuthorizationOptionsExtension
    {
        public static void AddPolicy(this AuthorizationOptions options) 
        {
            options.AddPolicy("InRole1or2", configuration =>
            {
                configuration.AddRequirements(new MemberOfRolesRequirement(MembershipType.Any, "Role1", "Role2"));
            });
            options.AddPolicy("OnlyForCoolUsers", configuration => 
            {
                configuration.RequireClaim("IsCool", "True");
            });
            options.AddPolicy("OnlyForNewUsers", configuration =>
            {
                configuration.RequireAssertion(context => 
                {
                    var claim = context.User.FindFirst("RegistrationDate");
                    if (claim == null)
                        return false;
                    if (DateTime.TryParse(claim.Value, out DateTime regDateTime)) 
                        return DateTime.Now - regDateTime <= new TimeSpan(24, 0, 0);
                    return false;
                });
            });
        }
    }
}
