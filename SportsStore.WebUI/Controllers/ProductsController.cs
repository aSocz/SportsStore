using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private const int pageSize = 4;
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult List(int page = 1, int? categoryId = null)
        {
            var products = categoryId.HasValue
                ? productService.GetCategoryPagedProducts(categoryId.Value, page, pageSize)
                : productService.GetPagedProducts(page, pageSize);
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = productService.GetProductsCount(categoryId)
            };

            var productViewModel = Mapper.Map<Product[], ProductViewModel[]>(products.ToArray());
            var viewModel = new ProductListViewModel
            {
                PagingInfo = pagingInfo,
                Products = productViewModel,
                CategoryId = categoryId
            };

            return View(viewModel);
        }
    }
}