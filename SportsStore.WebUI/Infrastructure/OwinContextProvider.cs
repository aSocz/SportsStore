using Microsoft.Owin;
using SportsStore.WebUI.Infrastructure.Abstraction;
using System.Web;

namespace SportsStore.WebUI.Infrastructure
{
    public class OwinContextProvider : IOwinContextProvider
    {
        public IOwinContext GetOwinContext(HttpContext context) => context.GetOwinContext();
    }
}