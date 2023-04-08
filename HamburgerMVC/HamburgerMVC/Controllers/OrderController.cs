using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models.ViewModels;
using HamburgerMVC.Repositories;
using HamburgerMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

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
        OrderVM orderVM = new OrderVM();
        MenuVM menuVM = new MenuVM();
        OrderDTO orderDTO = new OrderDTO();


        //Menu Listing on HomePage
        public async Task<IActionResult> HomePage()
        {
            var menus = await context.Menus.ToListAsync();
            menuVM.mList = menus.Select(menu => new MenuDTO
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                MenuPrice = menu.MenuPrice,
                //Image = menu.Image
            }).ToList();

            return View(menuVM);
        }


        public async Task<IActionResult> OrderDetail(int id)
        {
            AppUser appUser = await userManager.GetUserAsync(HttpContext.User);
            var orders = await context.Orders.Include(x => x.Menus).Include(x => x.ExtraIngredients).Where(x => x.AppUserId == appUser.Id).ToListAsync();


            var menu = await context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            //menuVM.
            var viewModel = new OrderVM()
            {
                Menu = menu,
                Orders = orders,
                DropDownForSize = FillSize(),
                ExtraIngredients = context.ExtraIngredients.ToList(),

            };


            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> OrderDetail(OrderVM viewModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            var menu = await context.Menus.FirstOrDefaultAsync(x => x.MenuId == viewModel.Menu.MenuId);

            if (menu == null)
            {
                return NotFound();
            }
            var extraIds = new List<int>();
            foreach (var item in viewModel.ExtraIngredients)
            {
                if (item.Selected)
                {
                    extraIds.Add(item.ExtraIngredientId);
                }
            }
            var isFirstOrder = !context.Orders.Any(o => o.AppUserId == user.Id);
            if (isFirstOrder)
            {
                viewModel.OrderNumber = 1;
            }
            else
            {

                var nextOrder = context.Orders.Where(o => o.AppUserId == user.Id)
                                               .OrderByDescending(o => o.OrderId)
                                               .FirstOrDefault();
                viewModel.OrderNumber = nextOrder.OrderNumber + 1;
            }

            var totalPrice = menu.MenuPrice;

            switch (viewModel.Size.ToString())
            {
                case "Regular":
                    break;
                case "Big":
                    totalPrice += 3.00M;
                    break;
                case "King":
                    totalPrice += 5.00M;
                    break;
                default:
                    return NotFound();
            }
            totalPrice *= viewModel.Quantity;


            //context.Orders.Add(order);
            //await context.SaveChangesAsync();
            Order order = new Order()
            {
                AppUserId = userId,
                Size = viewModel.Size,
                Quantity = viewModel.Quantity,
                TotalPrice = totalPrice,
                OrderNumber = viewModel.OrderNumber,
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var orderMenu = new MenuOrder
            {
                OrderId = order.OrderId,
                MenuId = menu.MenuId
            };
            context.MenuOrders.Add(orderMenu);
            await context.SaveChangesAsync();

            if (viewModel.ExtraIds != null)
            {
                foreach (var extraIngredientId in viewModel.ExtraIds)
                {
                    var extraOrder = new ExtraIngredientOrder
                    {
                        OrderId = order.OrderId,
                        ExtraIngredientId = extraIngredientId
                    };
                    context.ExtraIngredientOrders.Add(extraOrder);
                }
                await context.SaveChangesAsync();
            }

            //OrderVM orderVM = new OrderVM()
            //{
            //    OrderNumber = order.OrderNumber,
            //    Menu = menu,
            //    ExtraIngredients = viewModel.ExtraIngredients,
            //    Size = viewModel.Size,
            //    Quantity = viewModel.Quantity,
            //    TotalPrice = totalPrice
            //};

            //OrderVM orderVM = new OrderVM()
            //{
            //    OrderNumber = viewModel.OrderNumber,
            //    Menu = menu,
            //    ExtraIngredients = viewModel.ExtraIngredients,
            //    Size = viewModel.Size,
            //    Quantity = viewModel.Quantity,
            //    TotalPrice = totalPrice
            //};
            return RedirectToAction("OrderList");

        }

        

        public async Task<IActionResult> OrderList()
        {
            //OrderVM orderVM2 = new OrderVM()
            //{
            //    OrderNumber = orderVM.OrderNumber,
            //    Menu = orderVM.Menu,
            //    ExtraIngredients = orderVM.ExtraIngredients,
            //    Size = orderVM.Size,
            //    Quantity = orderVM.Quantity,
            //    TotalPrice = orderVM.TotalPrice
            //};
            var user = await userManager.GetUserAsync(HttpContext.User);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }

            OrderVM orderVM1 = new OrderVM();
            orderVM1.oList = context.Orders.Where(x=>x.AppUserId == userId).Select(x => new OrderDTO()
            {
                OrderNumber = x.OrderNumber,
                //Menus = x.Menus,
                //ExtraIngredients = x.ExtraIngredients, 
                Size = x.Size,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
            //List<OrderVM> orderVMs = new List<OrderVM>();
            //foreach (var order in orderVM.oList)
            //{
            //    var extraIngredients = order.ExtraIngredients
            //    .Select(x => x.ExtraIngredientName)
            //    .ToList();

            //    OrderVM orderVMNew = new OrderVM()
            //    {
            //        OrderNumber = order.OrderNumber,
            //        Menu = order.Menu,
            //        ExtraIngredientsNames = extraIngredients,
            //        DropDownForSize = order.DropDownForSize,
            //        Quantity = order.Quantity,
            //        TotalPrice = order.TotalPrice
            //    };

            //    orderVMs.Add(orderVMNew);
            return View(orderVM1);
        }
           


            //var user = await userManager.GetUserAsync(HttpContext.User);
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userId == null)
            //{
            //    return NotFound();
            //}

            //var orders = await context.Orders
            //    .Where(x => x.AppUserId == userId)
            //    .ToListAsync();
            //return View(orders);

        
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return RedirectToAction("OrderList");
        }
        private List<SelectListItem> FillSize()
        {
            var sizes = Enum.GetValues(typeof(Size))
         .Cast<Size>()
         .Select(s => new SelectListItem
         {
             Value = ((int)s).ToString(),
             Text = s.ToString()
         }).ToList();
            return sizes;
        }


    }
}
