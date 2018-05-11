using SportsStore.Domain.Interfaces;
using System.Collections.Generic;

namespace SportsStore.Domain.Entities
{
    public class Category : IValidatable
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public void Update(Category updatedCategory)
        {
            Name = updatedCategory.Name;
        }
    }
}
