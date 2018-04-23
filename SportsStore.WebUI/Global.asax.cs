using AutoMapper;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Binders;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SportsStore.WebUI.Areas.Admin.Models;

namespace SportsStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ManageProductViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductViewModel>());
        }
    }
}
