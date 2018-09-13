using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SportsStore.Business.Extensions;
using SportsStore.Business.Interfaces;
using SportsStore.Business.Models;
using SportsStore.Business.Validation;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using SportsStore.Infrastructure.Identity;
using SportsStore.WebUI.Infrastructure.Abstraction;
using SportsStore.WebUI.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IOwinContext owinContext;
        private readonly IUserInformationService userInformationService;

        public AccountsController(
            IOwinContextProvider owinContextProvider,
            IUserInformationService userInformationService)
        {
            this.userInformationService = userInformationService;
            owinContext = owinContextProvider.GetOwinContext(HttpContext.ApplicationInstance.Context);
            accountService = new AccountService(
                AuthManager,
                UserManager,
                userInformationService);
        }

        private SportsStoreUserManager UserManager => owinContext.GetUserManager<SportsStoreUserManager>();
        private IAuthenticationManager AuthManager => owinContext.Authentication;

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            returnUrl.ThrowIfNullOrEmpty();

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

            var loginResult = await accountService.Login(viewModel.Name, viewModel.Password);
            if (loginResult.IsValid())
            {
                return Redirect(viewModel.ReturnUrl);
            }

            FeedModelStateErrors(loginResult);
            return View(viewModel);

        }

        public ActionResult Logout(string returnUrl)
        {
            returnUrl.ThrowIfNullOrEmpty();

            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(returnUrl);
        }

        public ActionResult Create(string returnUrl)
        {
            returnUrl.ThrowIfNullOrEmpty();

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

            var creationResult =
                await accountService.Create(Mapper.Map<AccountDetailsViewModel, AccountDto>(viewModel));

            if (creationResult.IsValid())
            {
                var loginViewModel = GetLoginViewModel(viewModel);
                return RedirectToAction("Login", loginViewModel);
            }

            FeedModelStateErrors(creationResult);
            return View(viewModel);
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
            var updateResult =
                await accountService.Update(userId, Mapper.Map<AccountDetailsViewModel, AccountDto>(viewModel));

            if (updateResult.IsValid())
            {
                return Redirect(viewModel.ReturnUrl);
            }

            FeedModelStateErrors(updateResult);
            return View(viewModel);
        }

        private static AccountLoginViewModel GetLoginViewModel(AccountDetailsViewModel viewModel)
        {
            return new AccountLoginViewModel
            {
                Name = viewModel.Name,
                Password = viewModel.Password,
                ReturnUrl = viewModel.ReturnUrl
            };
        }

        private void FeedModelStateErrors(ValidationResult result)
        {
            result.Errors.ForEach(e => ModelState.AddModelError(e.PropertyName, e.ErrorMessage));
        }
    }
}