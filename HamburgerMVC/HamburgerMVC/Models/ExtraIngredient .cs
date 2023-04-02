namespace HamburgerMVC.Models
{
    public class ExtraIngredient
    {
        public int ExtraIngredientId { get; set; }
        public string ExtraIngredientName { get; set; }
        public decimal ExtraIngredientPrice { get; set; }
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
