using System.ComponentModel.DataAnnotations;

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
    }
}
