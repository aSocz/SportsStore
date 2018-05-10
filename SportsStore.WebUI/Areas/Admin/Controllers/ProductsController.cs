using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public ViewResult Index()
        {
            var products = productService.GetAllProducts();
            var productsViewModel = Mapper.Map<Product[], ManageProductViewModel[]>(products.ToArray());

            return View(productsViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = GetCategories();
            var viewModel = new ProductDetailsViewModel { Categories = categories };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductDetailsViewModel viewModel)
        {
            var product = viewModel.ToProduct();
            await productService.CreateProduct(product);

            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public ActionResult Edit(int productId)
        {
            var product = productService.GetProduct(productId);
            var categories = GetCategories(product.CategoryId);
            var viewModel = product.ToViewModel(categories);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductDetailsViewModel viewModel)
        {
            var product = viewModel.ToProduct();
            await productService.EditProduct(product);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int productId)
        {
            await productService.DeleteProduct(productId);

            return RedirectToActionPermanent("Index");
        }

        private IEnumerable<SelectListItem> GetCategories(int? selectedCategoryId = null)
        {
            return categoryService.GetCategories()
                                  .Select(
                                       c => new SelectListItem
                                       {
                                           Text = c.Name,
                                           Value = c.CategoryId.ToString(),
                                           Selected = selectedCategoryId.HasValue && c.CategoryId == selectedCategoryId
                                       });
        }
    }
}