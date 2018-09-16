using AutoMapper;
using SportsStore.Business.Validation;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IValidator<Category> categoryValidator;

        public CategoriesController(ICategoryService categoryService, IValidator<Category> categoryValidator)
        {
            this.categoryService = categoryService;
            this.categoryValidator = categoryValidator;
        }

        public ViewResult Index()
        {
            var categories = categoryService.GetCategories();
            var productsViewModel = Mapper.Map<Category[], ManageCategoryViewModel[]>(categories.ToArray());

            return View(productsViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new ManageCategoryViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ManageCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var category = viewModel.ToCategory();

            var validationResult = categoryValidator.Validate(category);
            if (!validationResult.IsValid())
            {
                validationResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                return View(viewModel);
            }

            await categoryService.CreateCategory(category);

            TempData["message"] = $"Dodano kategorie {category.Name}";

            return RedirectToActionPermanent("Index");
        }

        [HttpGet]
        public ActionResult Edit(int categoryId)
        {
            var category = categoryService.GetCategory(categoryId);
            var viewModel = category.ToViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ManageCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var category = viewModel.ToCategory();

            var validationResult = categoryValidator.Validate(category);
            if (!validationResult.IsValid())
            {
                validationResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
                return View(viewModel);
            }

            await categoryService.EditCategory(category);

            TempData["message"] = $"Zapisano kategorie {category.Name}";

            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int categoryId)
        {
            var category = categoryService.GetCategory(categoryId);
            await categoryService.DeleteCategory(categoryId);

            TempData["message"] = $"Usunięto kategorie {category.Name}";


            return RedirectToActionPermanent("Index");
        }
    }
}