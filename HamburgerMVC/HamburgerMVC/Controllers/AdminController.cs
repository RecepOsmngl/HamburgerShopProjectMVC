using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerMVC.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public AdminController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustormerDTO customer)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                };
                await userManager.AddToRoleAsync(appUser, "Customer");
                IdentityResult result = await userManager.CreateAsync(appUser, customer.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("CustomerList");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(customer);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
