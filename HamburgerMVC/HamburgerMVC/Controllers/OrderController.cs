using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models.ViewModels;
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
        private readonly MenuController menuController;

        public OrderController(Context _context, UserManager<AppUser> _userManager, MenuController _menuController)
        {
            context = _context;
            userManager = _userManager;
            menuController = _menuController;
        }
        OrderVM orderVM = new OrderVM();
        MenuVM menuVM = new MenuVM();
        public async Task<IActionResult> HomePage()
        {
            var menus = await context.Menus.ToListAsync(); 
            var viewModel = menus.Select(menu => new MenuDTO
            { 
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuPrice = menu.MenuPrice,
                //Image = menu.Image
            });

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            //var menu = await context.Menus. .GetByIdAsync(id);

            //if (menu == null)
            //{
            //    return NotFound();
            //}

            //var viewModel = new MenuViewModel
            //{
            //    Id = menu.Id,
            //    Name = menu.Name,
            //    Price = menu.Price,
            //    ImageUrl = menu.ImageUrl,
            //    ExtraIngredients = await _extraIngredientRepository.GetAllAsync()
            //};

            return View(viewModel);
        }
        //public async Task<IActionResult> HomePage()
        //{
        //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var orders = await context.Orders
        //        .Include(o => o.Menus)
        //        .Include(o => o.ExtraIngredients)
        //        .Where(o => o.AppUserId == user.Id)
        //        .ToListAsync();

        //    return View(orders);
        //}
        public async Task<IActionResult> OrderAdd()
        {
            return View(orderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderAdd(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return NotFound();
                }

                orderVM.Order.AppUserId = user.Id;

                context.Add(orderVM);
                await context.SaveChangesAsync();
                return RedirectToAction("HomePage");
            }
            return View(orderVM);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
