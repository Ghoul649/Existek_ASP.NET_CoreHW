using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App12.DTO
{
    public class LoginModel
    {
        [Required]
        [RegularExpression("[A-Za-z0-9_]{3,32}")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(128)]
        public string Password { get; set; }
    }
}
