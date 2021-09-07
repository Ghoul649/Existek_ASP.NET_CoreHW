using App12.DTO;
using App12.Models;
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
    }
}
