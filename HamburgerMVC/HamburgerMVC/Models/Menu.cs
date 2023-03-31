namespace HamburgerMVC.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
