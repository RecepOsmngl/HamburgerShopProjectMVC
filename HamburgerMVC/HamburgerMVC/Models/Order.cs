
using System.ComponentModel.DataAnnotations;

namespace HamburgerMVC.Models
{
    public enum Size { Small, Medium, Larger }

    public class Order
    {
        public Order()
        {
            ExtraIngredients = new HashSet<ExtraIngredient>();
            Menus= new HashSet<Menu>();
        }
        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public int OrderId { get; set; }
        public ICollection<Menu> Menus { get; set; }
        public ICollection<ExtraIngredient> ExtraIngredients { get; set; }
        public int Quantity { get; set; }

        //public DateTime OrderDate { get; set; }
    }
}
