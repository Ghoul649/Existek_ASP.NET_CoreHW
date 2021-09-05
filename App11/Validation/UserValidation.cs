using App11.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App11.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var obj = value as User;
            return obj.Field1 != obj.Field2;
        }
        public override string FormatErrorMessage(string name)
        {
            return "Field1 and Field2 must be different."; 
        }
    }
}
