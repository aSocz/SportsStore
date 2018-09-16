using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SportsStore.Infrastructure;
using SportsStore.Infrastructure.Identity;

namespace SportsStore.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.CreatePerOwinContext(SportsStoreContext.Create);
            appBuilder.CreatePerOwinContext<SportsStoreUserManager>(SportsStoreUserManager.Create);

            appBuilder.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Accounts/Login")
                });
        }
    }
}