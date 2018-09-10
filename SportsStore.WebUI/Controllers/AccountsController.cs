using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.Infrastructure.Identity;
using SportsStore.WebUI.Infrastructure;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IOwinContext owinContext;
        private readonly IUserInformationService userInformationService;

        public AccountsController(IOwinContextProvider owinContextProvider, IUserInformationService userInformationService)
        {
            this.userInformationService = userInformationService;
            owinContext = owinContextProvider.GetOwinContext(HttpContext.ApplicationInstance.Context);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var viewModel = new AccountLoginViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await UserManager.FindAsync(viewModel.Name, viewModel.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid name or password");
                return View(viewModel);
            }

            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut();
            AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            return Redirect(viewModel.ReturnUrl);
        }

        public ActionResult Create(string returnUrl)
        {
            var viewModel = new AccountDetailsViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new SportsStoreUser(viewModel.Name, viewModel.Email);
            var userInformation = new UserInformation(Mapper.Map<AddressViewModel, Address>(viewModel.Address));

            var result = await UserManager.CreateAsync(
                user,
                viewModel.Password,
                userInformation,
                userInformationService);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e));
                return View(viewModel);
            }

            var loginViewModel = GetLoginViewModel(viewModel);
            return RedirectToAction("Login", loginViewModel);
        }

        public async Task<ActionResult> Edit(string returnUrl)
        {
            var userId = User.Identity.GetUserId<int>();
            var user = await UserManager.FindByIdAsync(userId);
            var userInformation = userInformationService.GetUserInformation(userId);

            var viewModel = new AccountDetailsViewModel
            {
                Email = user.Email,
                Name = user.UserName,
                ReturnUrl = returnUrl,
                Address = Mapper.Map<Address, AddressViewModel>(userInformation.Address)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccountDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userId = User.Identity.GetUserId<int>();
            var user = new SportsStoreUser(viewModel.Name, viewModel.Email);
            var userInformation = new UserInformation(Mapper.Map<AddressViewModel, Address>(viewModel.Address));
            var result = await UserManager.UpdateAsync(
                userId,
                user,
                viewModel.Password,
                userInformation,
                userInformationService);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e));
                return View(viewModel);
            }

            return Redirect(viewModel.ReturnUrl);
        }

        private SportsStoreUserManager UserManager => owinContext.GetUserManager<SportsStoreUserManager>();

        private IAuthenticationManager AuthManager => owinContext.Authentication;

        private static AccountLoginViewModel GetLoginViewModel(AccountDetailsViewModel viewModel) => new AccountLoginViewModel
        {
            Name = viewModel.Name,
            Password = viewModel.Password,
            ReturnUrl = viewModel.ReturnUrl
        };
    }
}