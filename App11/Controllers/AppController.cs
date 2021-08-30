using App11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App11.Controllers
{
    public class AppController : Controller
    {
        [Route("validate/user")]
        public IActionResult ValidateUser([FromBody]User user) 
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            return Ok();
        }
    }
}
