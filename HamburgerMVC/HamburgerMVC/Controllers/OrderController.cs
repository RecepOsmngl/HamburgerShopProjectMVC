using HamburgerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HamburgerMVC.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly Context context;
        private readonly UserManager<AppUser> userManager;

        public OrderController(Context _context, UserManager<AppUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }
        public async Task<IActionResult> HomePage()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            var orders = await context.Orders
                .Include(o => o.Menus)
                .Include(o => o.ExtraIngredients)
                .Where(o => o.AppUserId == user.Id)
                .ToListAsync();

            return View(orders);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
