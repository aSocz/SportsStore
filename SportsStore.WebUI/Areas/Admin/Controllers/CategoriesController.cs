using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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
        public async Task<ActionResult> Create(ManageCategoryViewModel viewModel)
        {
            var category = viewModel.ToCategory();
            await categoryService.CreateCategory(category);

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
        public async Task<ActionResult> Edit(ManageCategoryViewModel viewModel)
        {
            var category = viewModel.ToCategory();
            await categoryService.EditCategory(category);

            return RedirectToActionPermanent("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int categoryId)
        {
            await categoryService.DeleteCategory(categoryId);

            return RedirectToActionPermanent("Index");
        }
    }
}