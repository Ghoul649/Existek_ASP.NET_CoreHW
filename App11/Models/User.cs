using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App11.Models
{
    public class User
    {
        [Required]
        [RegularExpression("[A-Za-z1-9_]{3,32}")]
        public string UserName { get; set; }
        [RegularExpression("[^\\W\\s\\d]{1,40}")]
        public string FirstName { get; set; }
        [RegularExpression("[^\\W\\s\\d]{1,40}")]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
