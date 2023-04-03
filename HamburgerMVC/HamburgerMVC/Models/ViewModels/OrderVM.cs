using HamburgerMVC.Models.DTOs;

namespace HamburgerMVC.Models.ViewModels
{
    public class OrderVM
    {
        public List<OrderDTO> oList { get; set; }
        public Order Order { get; set; }
    }
}
