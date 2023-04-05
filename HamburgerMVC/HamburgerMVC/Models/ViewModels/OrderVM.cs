using HamburgerMVC.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamburgerMVC.Models.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            ExtraIngredients= new List<ExtraIngredient>();
        }
        public List<OrderDTO> oList { get; set; }
        public Order Order { get; set; }   
        public Menu Menu { get; set; }
        public List<SelectListItem> DropDownForSize { get; set; }
        public List<ExtraIngredient> ExtraIngredients { get; set; }
    }
}
