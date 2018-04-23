using SportsStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart : IShoppingCart
    {
        private readonly List<CartLine> items;

        public Cart()
        {
            items = new List<CartLine>();
        }

        public Cart(IEnumerable<CartLine> items)
        {
            this.items = items.ToList();
        }

        public IReadOnlyCollection<CartLine> Items => items;

        public void AddItem(Product product, int quantity)
        {
            if (product == default(Product) || quantity == default(int))
            {
                return;
            }

            var cartLine = items.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
            if (cartLine != default(CartLine))
            {
                cartLine.Quantity += quantity;
                return;
            }

            items.Add(new CartLine { Product = product, Quantity = quantity });
        }

        public void RemoveItem(Product product)
        {
            if (product == default(Product))
            {
                return;
            }

            items.RemoveAll(i => i.Product.ProductId == product.ProductId);
        }

        public void Clear() => items.Clear();

        public decimal ComputeTotal() => items.Sum(i => i.Product.Price * i.Quantity);
    }
}
