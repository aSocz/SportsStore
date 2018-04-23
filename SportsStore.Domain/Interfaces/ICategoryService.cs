using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}