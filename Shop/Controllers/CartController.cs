using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Models;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserContext _context;

        public CartController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> ShowCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = _context.Cart
                .Include(c => c.Product)
                .Include(c => c.User)
                .Where(p => p.UserId == user.Id)
                .Select(c => c.Product)
                .ToList();
            var dict = new Dictionary<Product, int>();
            foreach (Product product in products)
            {
                int count = products.Count(p => p == product);
                dict.TryAdd(product, count);
            }

            ViewData["Title"] = "Cart";
            return View(dict);
        }

        public async Task<IActionResult> AddToCart(int prodId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Authorize", "Authorization");
            var cart = new CartModel()
            {
                ProductId = prodId,
                UserId = user.Id,
            };
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SubmitOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var products = _context.Cart
                .Include(c => c.Product)
                .Include(c => c.User)
                .Where(p => p.UserId == user.Id)
                .ToList();
            if (products.Count == 0)
                return RedirectToAction("ShowCart");
            _context.Cart.RemoveRange(products);
            await _context.SaveChangesAsync();


            return View();
        }
    }
}
