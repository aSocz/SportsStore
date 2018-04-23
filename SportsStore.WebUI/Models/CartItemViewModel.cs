namespace SportsStore.WebUI.Models
{
    public class CartItemViewModel
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public int ProductId { get; set; }
    }
}