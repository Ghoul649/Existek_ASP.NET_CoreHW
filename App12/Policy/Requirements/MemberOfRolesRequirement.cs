using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Policy.Requirements
{
    public enum MembershipType
    {
        Any,
        All
    }
    public class MemberOfRolesRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Roles { get; set; }
        public MembershipType MembershipType { get; set; }
        public MemberOfRolesRequirement() { }
        public MemberOfRolesRequirement(MembershipType type, params string[] roles) 
        {
            Roles = roles;
            MembershipType = type;
        }
    }
}
