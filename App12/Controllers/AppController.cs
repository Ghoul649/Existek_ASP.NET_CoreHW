using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App12.Controllers
{
    [Route("app")]
    public class AppController : Controller
    {
        [Route("test")]
        [Authorize]
        public IActionResult Test() 
        {
            return Ok();
        }
    }
}
