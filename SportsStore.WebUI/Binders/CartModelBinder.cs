using System.Web.Mvc;
using SportsStore.WebUI.Extensions;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) =>
            controllerContext.HttpContext.Session.GetCart();
    }
}