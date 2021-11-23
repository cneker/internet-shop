using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Shop.Models;

namespace Shop.Controllers
{
    public class AuthorizationController : Controller
    {
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;

        public AuthorizationController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Registration()
        {
            ViewData["Title"] = "Registration";
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel() {Email = model.Email, UserName = model.NickName};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                }
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public IActionResult Authorize()
        {
            ViewData["Title"] = "Authorization";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorize(LoginModel model)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    //var claim = new Claim("UserId", user.Id);
                    //var result1 = await _userManager.AddClaimAsync(user, claim);
                    //if(result1.Succeeded)
                        return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
            }

            return RedirectToAction(actionName: "Authorize", controllerName: "Authorization");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
