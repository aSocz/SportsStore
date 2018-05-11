using SportsStore.Domain.Interfaces;
using System.Collections.Generic;

namespace SportsStore.Domain.Entities
{
    public class Product : IValidatable
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }

        public void Update(Product updatedProduct)
        {
            Name = updatedProduct.Name;
            Description = updatedProduct.Description;
            Price = updatedProduct.Price;
            CategoryId = updatedProduct.CategoryId;
        }
    }
}
