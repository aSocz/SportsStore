using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                "",
                new
                {
                    controller = "Products",
                    action = "List",
                    categoryId = (int?)null,
                    page = 1
                },
                new[] { "SportsStore.WebUI.Controllers" });

            routes.MapRoute(
                null,
                "Page{page}",
                new
                {
                    controller = "Products",
                    action = "List",
                    categoryId = (int?)null,
                },
                new
                { page = @"\d+" },
                new[] { "SportsStore.WebUI.Controllers" });

            routes.MapRoute(
                null,
                "{categoryId}",
                new
                {
                    controller = "Products",
                    action = "List",
                    page = 1
                },
                new
                { categoryId = @"\d+" },
                new[] { "SportsStore.WebUI.Controllers" });

            routes.MapRoute(
                null,
                "{categoryId}/Page{page}",
                new
                {
                    controller = "Products",
                    action = "List",
                },
                new
                {
                    categoryId = @"\d+",
                    page = @"\d+"
                },
                new[] { "SportsStore.WebUI.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SportsStore.WebUI.Controllers" }
            );
        }
    }
}
