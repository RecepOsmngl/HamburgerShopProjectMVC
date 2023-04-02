using HamburgerMVC.Models.DTOs;

namespace HamburgerMVC.Models.ViewModels
{
    public class ExtraIngredientVM
    {
        public ExtraIngredient ExtraIngredient { get; set; }
        public List<ExtraIngredientDTO> eList { get; set; }
    }
}
