using AutoMapper;
using SportsStore.Business.Validation;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IValidator<Product> productValidator;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IValidator<Product> productValidator)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.productValidator = productValidator;
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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDetailsViewModel viewModel, HttpPostedFileBase image = null)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = GetCategories();
                return View(viewModel);
            }

            var product = viewModel.ToProduct();
            product.Thumbnail = ReadImage(image);

            var validationResult = productValidator.Validate(product);
            if (!validationResult.IsValid())
            {
                validationResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                viewModel.Categories = GetCategories();

                return View(viewModel);
            }

            await productService.CreateProduct(product);

            TempData["message"] = $"Dodano produkt {product.Name}";

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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDetailsViewModel viewModel, HttpPostedFileBase image = null)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = GetCategories(viewModel.CategoryId);
                return View(viewModel);
            }

            var product = viewModel.ToProduct();
            product.Thumbnail = ReadImage(image);

            var validationResult = productValidator.Validate(product);
            if (!validationResult.IsValid())
            {
                validationResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                viewModel.Categories = GetCategories();

                return View(viewModel);
            }

            await productService.EditProduct(product);

            TempData["message"] = $"Zapisano produkt {product.Name}";

            return RedirectToActionPermanent("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = productService.GetProduct(productId);
            await productService.DeleteProduct(productId);

            TempData["message"] = $"Usunięto produkt {product.Name}";

            return RedirectToActionPermanent("Index");
        }

        private static Image ReadImage(HttpPostedFileBase image)
        {
            if (image == null)
            {
                return null;
            }

            var imageData = new byte[image.ContentLength];
            image.InputStream.Read(imageData, 0, image.ContentLength);
            return new Image(imageData, image.ContentType);
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