using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.BusinessAbstraction.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}