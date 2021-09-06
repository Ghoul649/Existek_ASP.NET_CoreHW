using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Models
{
    public class User : IdentityUser<int>
    {
        public string UserName { get; set; }
    }
}
