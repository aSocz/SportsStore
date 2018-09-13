using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SportsStore.Business.Interfaces;
using SportsStore.Business.Models;
using SportsStore.Business.Validation;
using SportsStore.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Identity
{
    public class AccountService : IAccountService
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly IUserInformationService userInformationService;
        private readonly SportsStoreUserManager userManager;

        public AccountService(
            IAuthenticationManager authenticationManager,
            SportsStoreUserManager userManager,
            IUserInformationService userInformationService)
        {
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
            this.userInformationService = userInformationService;
        }

        public async Task<ValidationResult> Login(string userName, string password)
        {
            var user = await userManager.FindAsync(userName, password);
            if (user == null)
            {
                var validationError = new ValidationError("Invalid name or password", string.Empty);
                return new ValidationResult(new[] { validationError });
            }

            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut();
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            return new ValidationResult(null);
        }

        public async Task<ValidationResult> Create(AccountDto accountDto)
        {
            var result = await userManager.CreateAsync(accountDto, userInformationService);

            if (result.Succeeded)
            {
                return new ValidationResult(null);
            }

            var errors = result.Errors.Select(e => new ValidationError(e, string.Empty));
            return new ValidationResult(errors);
        }

        public async Task<ValidationResult> Update(int userId, AccountDto accountDto)
        {
            var result = await userManager.UpdateAsync(userId, accountDto, userInformationService);

            if (result.Succeeded)
            {
                return new ValidationResult(null);
            }

            var errors = result.Errors.Select(e => new ValidationError(e, string.Empty));
            return new ValidationResult(errors);
        }
    }
}