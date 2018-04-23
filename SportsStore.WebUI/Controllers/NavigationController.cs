using SportsStore.Domain.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ICategoryService categoryService;

        public NavigationController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [ChildActionOnly]
        public ActionResult Menu(int? categoryId, bool horizontal)
        {
            var categories = categoryService.GetCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString(),
                Selected = c.CategoryId == categoryId
            });

            return PartialView(categories);
        }
    }
}