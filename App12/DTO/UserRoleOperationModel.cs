using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App12.DTO
{
    public enum UserRoleOperation 
    {
        Set,
        Delete
    }
    public class UserRoleOperationModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public UserRoleOperation Operation { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
