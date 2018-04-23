using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Collections.Generic;

namespace SportsStore.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            categoryRepository = unitOfWork.GetRepository<Category>();
        }

        public IEnumerable<Category> GetCategories()
            => categoryRepository.GetAllNoTracking();
    }
}
