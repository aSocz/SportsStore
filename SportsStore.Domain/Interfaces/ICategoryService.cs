using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Task CreateCategory(Category category);

        Category GetCategory(int categoryId);

        Task EditCategory(Category updatedCategory);

        Task DeleteCategory(int categoryId);
    }
}