using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsStore.Domain.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetPagedProducts(int page, int pageSize);

        IEnumerable<Product> GetCategoryPagedProducts(int categoryId, int page, int pageSize);

        int GetProductsCount(int? categoryId = null);

        Product GetProduct(int productId);

        Task CreateProduct(Product product);

        Task EditProduct(Product updatedProduct);

        Task DeleteProduct(int productId);
    }
}
