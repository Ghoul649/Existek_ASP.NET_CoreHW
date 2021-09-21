using App12.DTO;
using App12.Filters;
using App12.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App12.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        public UserManager<User> UserManager { get; set; }
        public SignInManager<User> SignInManager { get; set; }
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel regInfo)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = new User() { UserName = regInfo.UserName, Email = regInfo.Email, RegistrationDate = DateTime.Now };
            var newIdentity = await UserManager.CreateAsync(user, regInfo.Password);
            if (!newIdentity.Succeeded)
            {
                foreach (var error in newIdentity.Errors)
                    ModelState.AddModelError("Register", error.Description);
                return ValidationProblem();
            }
            await SignInManager.SignInAsync(user, false);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginInfo)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var result = await SignInManager.PasswordSignInAsync(loginInfo.UserName, loginInfo.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Login", "Wrong username or password.");
                return ValidationProblem();
            }
            return Ok();
        }

        [HttpPost]
        [Route("roleOperation")]
        [Authorize(Roles = "Admin")]
        [TypeFilter(typeof(RoleFilter), Arguments = new object[]{ "Admin" })]
        public async Task<IActionResult> RoleOperation([FromBody] UserRoleOperationModel operation, [FromServices] RoleManager<IdentityRole<int>> rm) 
        {
            //var currentUser = await UserManager.GetUserAsync(User);
            //if (currentUser == null)
            //    return Unauthorized();
            //if (!await UserManager.IsInRoleAsync(currentUser,"Admin"))
            //    return NotFound();


            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = await UserManager.FindByNameAsync(operation.UserName);
            if (user is null)
            {
                ModelState.AddModelError("UserName", "Unknown user");
                return ValidationProblem();
            }

            IdentityResult res;
            if (operation.Operation == UserRoleOperation.Set)
                res = await UserManager.AddToRolesAsync(user, operation.RoleNames);
            else 
            {
                res = await UserManager.RemoveFromRolesAsync(user, operation.RoleNames);
            }
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                    ModelState.AddModelError("Register", error.Description);
                return ValidationProblem();
            }
            return Ok();
        }
    }
}
