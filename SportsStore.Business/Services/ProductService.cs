using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;

        public ProductService(IUnitOfWork unitOfWork)
        {
            productRepository = unitOfWork.GetRepository<Product>();
        }

        public IEnumerable<Product> GetAllProducts()
            => productRepository.GetAllNoTracking();

        public Product GetProduct(int productId) =>
            productRepository.GetNoTracking(p => p.ProductId == productId).FirstOrDefault();


        public IEnumerable<Product> GetPagedProducts(int page, int pageSize)
            => PageProducts(GetAllProducts(), page, pageSize);

        public IEnumerable<Product> GetCategoryPagedProducts(int categoryId, int page, int pageSize)
            => PageProducts(productRepository.GetNoTracking(p => p.CategoryId == categoryId), page, pageSize);

        public int GetProductsCount(int? categoryId)
            => productRepository.GetNoTracking(p => !categoryId.HasValue || p.CategoryId == categoryId.Value).Count();

        private static IEnumerable<Product> PageProducts(IEnumerable<Product> products, int page, int pageSize)
            => products.OrderBy(p => p.Name).Skip((page - 1) * pageSize).Take(pageSize);
    }
}
