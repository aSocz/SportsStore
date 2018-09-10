using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Identity
{
    public class SportsStoreSignInManager : SignInManager<SportsStoreUser, int>
    {
        public SportsStoreSignInManager(SportsStoreUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(SportsStoreUser user)
        {
            return user.GenerateUserIdentityAsync((SportsStoreUserManager)UserManager);
        }

        public static SportsStoreSignInManager Create(IdentityFactoryOptions<SportsStoreSignInManager> options, IOwinContext context)
        {
            return new SportsStoreSignInManager(context.GetUserManager<SportsStoreUserManager>(), context.Authentication);
        }
    }
}