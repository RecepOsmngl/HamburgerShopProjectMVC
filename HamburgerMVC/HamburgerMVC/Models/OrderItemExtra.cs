using HamburgerMVC.Models.ViewModels;

namespace HamburgerMVC.Models
{
    public class OrderItemExtra
    {
        public OrderItem OrderItem { get; set; }
        public int ExtraIngredientId { get; set; }
    }
}
