using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;
        private UserContext _productContext;

        public HomeController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, UserContext productContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _productContext = productContext;
        }
        
        public IActionResult Index()
        {
            ViewData["Title"] = "Shop";
            return View(_productContext.Products);
        }
    }
}
