namespace HamburgerMVC.Models
{
    public class OrderItem
    {
        public int MenuId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public List<OrderItemExtra> OrderItemExtras { get; set; }

    }
}
