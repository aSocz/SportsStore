using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SportsStore.Infrastructure.Identity;
using SportsStore.WebUI.Areas.Admin.Models;
using SportsStore.WebUI.Infrastructure.Abstraction;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IOwinContext owinContext;

        public UsersController(IOwinContextProvider owinContextProvider)
        {
            owinContext = owinContextProvider.GetOwinContext(HttpContext.ApplicationInstance.Context);
        }

        public ActionResult Index()
        {
            var userManager = owinContext.GetUserManager<SportsStoreUserManager>();
            var roleManager = owinContext.GetUserManager<RoleManager<Role, int>>();
            var roles = roleManager.Roles.ToList();
            var users = userManager.Users
                                   .ToList()
                                   .Select(
                                        u => new UserGridViewModel
                                        {
                                            UserName = u.UserName,
                                            Email = u.Email,
                                            Roles = string.Join(
                                                ", ",
                                                u.Roles.Select(r => roles.First(role => role.Id == r.RoleId).Name))
                                        });

            return View(users);
        }
    }
}