using Microsoft.Owin;
using System.Web;

namespace SportsStore.WebUI.Infrastructure.Abstraction
{
    public interface IOwinContextProvider
    {
        IOwinContext GetOwinContext(HttpContext context);
    }
}