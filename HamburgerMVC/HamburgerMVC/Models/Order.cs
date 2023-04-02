
using System.ComponentModel.DataAnnotations;

namespace HamburgerMVC.Models
{
    public enum Size { Small, Medium, Larger }

    public class Order
    {
        public Order()
        {
            ExtraIngredients = new HashSet<ExtraIngredientOrder>();
            Menus = new HashSet<MenuOrder>();
        }
        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public int OrderId { get; set; }
        public ICollection<ExtraIngredientOrder> ExtraIngredients { get; set; }
        public int Quantity { get; set; }
        public ICollection<MenuOrder> Menus { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
