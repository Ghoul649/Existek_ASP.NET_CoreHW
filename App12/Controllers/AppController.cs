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
        [HttpGet]
        public IActionResult Test() 
        {
            return Ok();
        }

        [Route("policy/InRole1or2")]
        [Authorize(Policy = "InRole1or2")]
        [HttpGet]
        public IActionResult InRole1or2()
        {
            return Ok();
        }

        [Route("policy/OnlyForCoolUsers")]
        [Authorize(Policy = "OnlyForCoolUsers")]
        [HttpGet]
        public IActionResult OnlyForCoolUsers()
        {
            return Ok();
        }

        [Route("policy/OnlyForNewUsers")]
        [Authorize(Policy = "OnlyForNewUsers")]
        [HttpGet]
        public IActionResult OnlyForNewUsers()
        {
            return Ok();
        }

    }
}
