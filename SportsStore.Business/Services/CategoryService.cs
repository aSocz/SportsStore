using SportsStore.Business.Extensions;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            categoryRepository = unitOfWork.GetRepository<Category>();
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories() => categoryRepository.GetAllNoTracking();

        public async Task CreateCategory(Category category)
        {
            categoryRepository.Insert(category);
            await unitOfWork.SaveChangesAsync();
        }

        public Category GetCategory(int categoryId)
        {
            return categoryRepository.GetNoTracking(c => c.CategoryId == categoryId).FirstOrDefault();
        }

        public async Task EditCategory(Category updatedCategory)
        {
            var category = categoryRepository.Get(c => c.CategoryId == updatedCategory.CategoryId).FirstOrDefault();
            category.ThrowIfNull();

            category.Update(updatedCategory);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var orderRepository = unitOfWork.GetRepository<Order>();

            var category = categoryRepository.Get(c => c.CategoryId == categoryId).FirstOrDefault();
            category.ThrowIfNull();

            var categoryOrders = category.Products.SelectMany(p => p.CartLines.Select(cl => cl.Order)).ToList();

            categoryOrders.ForEach(o => orderRepository.Delete(o));
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
        }
    }
}