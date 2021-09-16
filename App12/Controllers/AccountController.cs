using App12.DTO;
using App12.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel regInfo) 
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = new User() { UserName = regInfo.UserName, Email = regInfo.Email };
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
        [Route("roleOperation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleOperation([FromBody] UserRoleOperationModel operation, [FromServices] RoleManager<IdentityRole<int>> rm) 
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = await UserManager.FindByNameAsync(operation.UserName);
            if (user is null)
                ModelState.AddModelError("UserName", "Unknown user");
            var role = await rm.FindByNameAsync(operation.RoleName);
            if (role is null)
                ModelState.AddModelError("RoleName", "Unknown role");
            if (role == null || user == null)
                return ValidationProblem();
            IdentityResult res;
            if (operation.Operation == UserRoleOperation.Set)
                res = await UserManager.AddToRoleAsync(user, role.Name);
            else 
            {
                res = await UserManager.RemoveFromRoleAsync(user, role.Name);
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
