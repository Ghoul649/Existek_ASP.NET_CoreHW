using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App9.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public UserInfo UserInfo { get; set; }
    }
}
