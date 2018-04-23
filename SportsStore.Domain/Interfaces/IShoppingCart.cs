using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Interfaces
{
    public interface IShoppingCart
    {
        void AddItem(Product product, int quantity);
        void RemoveItem(Product product);
        void Clear();
        decimal ComputeTotal();
        IReadOnlyCollection<CartLine> Items { get; }
    }
}
