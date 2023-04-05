using HamburgerMVC.Models;
using HamburgerMVC.Models.DTOs;
using HamburgerMVC.Models.ViewModels;
using HamburgerMVC.Repositories;
using HamburgerMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var menu = await context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            OrderVM viewModel = new OrderVM
            {
                Menu = menu,
                Order = new Order(),
                oList = new List<OrderDTO>(),
                DropDownForSize = FillSize(),
                ExtraIngredients = await context.ExtraIngredients.ToListAsync()
            };



            viewModel.Order.Menus.Add(new MenuOrder { Menu = viewModel.Menu });

            foreach (var extra in await context.ExtraIngredients.ToListAsync())
            {
                viewModel.oList.Add(new OrderDTO
                {
                    ExtraIngredient = extra,
                    DropDownForSize = FillSize()
                });
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> OrderDetail(OrderVM viewModel)
        {
            var order = viewModel.Order;
            order.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Orders.Add(order);
            context.SaveChanges();
            return RedirectToAction("OrderList", "Order", new { id = viewModel.Order.OrderId });
            //if (ModelState.IsValid)
            //{
            //    var order = viewModel.Order;
            //    order.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //    foreach (var item in viewModel.oList)
            //    {
            //        if (item.SizeId != 0)
            //        {
            //            var orderVM = new Order()
            //            {
            //                OrderId = item.OrderId,
            //                Size = item.Size,
            //                Quantity = item.Quantity
            //            };
            //            //context.Orders.Add();
            //            //context.SaveChanges();

            //        }

            //    }
            //    context.Orders.Add(viewModel.Order);
            //    context.SaveChangesAsync();

            //    return RedirectToAction("OrderList", "Order", new { id = viewModel.Order.OrderId });
            //}

            //viewModel.DropDownForSize = FillSize();
            //return View(viewModel);

            //var orderItem = new OrderItem
            //{
            //    MenuId = orderDTO.MenuId,
            //    SizeId = orderDTO.SizeId,
            //    Quantity = orderDTO.Quantity

            //};

            //if (orderDTO.ExtraIngredients != null)
            //{
            //    foreach (var item in orderDTO.ExtraIngredients)
            //    {
            //        var orderItemExtra = new OrderItemExtra
            //        {
            //            OrderItem = orderItem,
            //            ExtraIngredientId = item.ExtraIngredientId
            //        };

            //        orderItem.OrderItemExtras.Add(orderItemExtra);
            //    }
            //}

            ////_cartService.AddToCart(orderItem);

            //return View(orderItem);
        }

        //public IActionResult Cart()
        //{
        //    var cart = _cartService.GetCart();
        //    var viewModel = new CartDTO
        //    {
        //        OrderItems = cart.OrderItems,
        //        TotalPrice = cart.GetTotalPrice()
        //    };

        //    return View(viewModel);
        //}
        private List<SelectListItem> FillSize()
        {
            var sizes = Enum.GetValues(typeof(Size))
         .Cast<Size>()
         .Select(s => new SelectListItem
         {
             Value = ((int)s).ToString(),
             Text = s.ToString()
         }).ToList();
            foreach (var item in sizes)
            {
                item.Selected = (item.Value == "1"); // or any other default selection condition
            }
            return sizes;
        }

        public async Task<IActionResult> OrderList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await context.Orders
                .Where(o => o.AppUserId == userId)
                .Include(o => o.ExtraIngredients)
                .Include(o => o.Menus)
                .ToListAsync();

            return View(orders);

        }



    }
}
