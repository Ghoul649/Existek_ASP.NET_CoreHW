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
        }
    }
}
