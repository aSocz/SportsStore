using System.Collections.Generic;

namespace SportsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public IReadOnlyCollection<CartItemViewModel> Items { get; set; }
        public string ReturnUrl { get; set; }
    }
}