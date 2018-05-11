using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}