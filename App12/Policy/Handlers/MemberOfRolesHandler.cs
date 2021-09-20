using App12.Policy.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Policy.Handlers
{
    public class MemberOfRolesHandler : AuthorizationHandler<MemberOfRolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberOfRolesRequirement requirement)
        {
            if (requirement.MembershipType == MembershipType.All)
                foreach (var role in requirement.Roles)
                {
                    if (context.User.IsInRole(role))
                        continue;
                    return Task.CompletedTask;
                }
            else
                foreach (var role in requirement.Roles)
                    if (context.User.IsInRole(role))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
            return Task.CompletedTask;
        }
    }
}
