using Microsoft.Owin;
using System.Web;

namespace SportsStore.WebUI.Infrastructure
{
    public interface IOwinContextProvider
    {
        IOwinContext GetOwinContext(HttpContext context);
    }
}