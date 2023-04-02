using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HamburgerMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CustormerDTO custormerDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = custormerDTO.UserName,
                    Email = custormerDTO.Email,

                };

                IdentityRole role = await roleManager.FindByNameAsync("Customer");
                if (role == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole("Customer"));
                    if (!result.Succeeded)
                    {
                        Errors(result);
                    }
                }

                IdentityResult identityResult = await userManager.CreateAsync(appUser, custormerDTO.Password);

                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "Customer");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(custormerDTO);
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl is null ? "/Home/Index" : returnUrl;
            return View(new Login()
            {
                ReturnUrl = returnUrl
            });

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(appUser, "Customer"))
                            return RedirectToAction("HomePage", "Order");
                        else
                            return RedirectToAction("ExtraIngredientList", "ExtraIngredient");
                    }
                    ModelState.AddModelError("", "Invalid password or username");
                }
            }
            return View(login);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                TempData["Error"] = $"{error.Code} - {error.Description}";
            }
        }

    }
}
