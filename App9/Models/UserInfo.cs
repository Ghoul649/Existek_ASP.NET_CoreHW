using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App9.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
