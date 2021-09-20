using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Models
{
    public class User : IdentityUser<int>
    {
        public DateTime RegistrationDate { get; set; }
        public bool IsCool { get; set; }
    }
}
