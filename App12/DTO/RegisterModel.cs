using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App12.DTO
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression("[A-Za-z0-9_]{3,32}")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(128)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
