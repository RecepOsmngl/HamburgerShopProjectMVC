using HamburgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HamburgerMVC.Models.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public List<Size> Sizes { get; set; }
        public ExtraIngredient ExtraIngredient { get; set; }
        public List<Menu> Menus { get; set; }
        public Menu Menu { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        public int MenuId { get; set; }
        public int SizeId { get; set; }
        public List<int> ExtraIngredientsIds { get; set; }
        public List<ExtraIngredient> ExtraIngredients { get; set; }
        public List<SelectListItem> DropDownForSize { get; set; }
        public int OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
