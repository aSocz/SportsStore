using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SportsStore.Business.Extensions;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Identity
{
    public class SportsStoreUserManager : UserManager<SportsStoreUser, int>
    {
        public SportsStoreUserManager(IUserStore<SportsStoreUser, int> store)
            : base(store)
        {
        }

        public static SportsStoreUserManager Create(
            IdentityFactoryOptions<SportsStoreUserManager> options,
            IOwinContext context)
        {
            var manager = new SportsStoreUserManager(new UserStore(context.Get<SportsStoreContext>()));
            manager.UserValidator = new UserValidator<SportsStoreUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }

        public async Task<IdentityResult> CreateAsync(
            SportsStoreUser user,
            string password,
            UserInformation userInformation,
            IUserInformationService userInformationService)
        {
            var identityResult = await base.CreateAsync(user, password);
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            userInformation.UserId = user.Id;
            await userInformationService.CreateUserInformation(userInformation);

            return identityResult;
        }

        public async Task<IdentityResult> UpdateAsync(
            int userId,
            SportsStoreUser user,
            string password,
            UserInformation userInformation,
            IUserInformationService userInformationService)
        {
            var storedUser = await FindByIdAsync(userId);
            storedUser.ThrowIfNull();
            if (!string.IsNullOrWhiteSpace(password))
            {
                var validationResult = await PasswordValidator.ValidateAsync(password);
                if (!validationResult.Succeeded)
                {
                    return validationResult;
                }
            }

            storedUser.Update(user, password, PasswordHasher);

            var identityResult = await base.UpdateAsync(storedUser);
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            var storedUserInformation = userInformationService.GetUserInformation(user.Id);
            userInformation.UserId = storedUserInformation.UserId;
            userInformation.UserInformationId = storedUserInformation.UserInformationId;

            await userInformationService.EditUserInformation(userInformation);

            return identityResult;
        }
    }
}