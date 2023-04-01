using HamburgerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HamburgerMVC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ExtraIngredientController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        Context context;

        public ExtraIngredientController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, Context _context)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            context = _context;
        }
        public IActionResult ExtraIngredientList()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
