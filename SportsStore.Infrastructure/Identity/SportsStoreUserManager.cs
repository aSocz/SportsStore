using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SportsStore.Business.Extensions;
using SportsStore.Business.Models;
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
            AccountDto accountDto,
            IUserInformationService userInformationService)
        {
            var userInformation = new UserInformation(Mapper.Map<AddressDto, Address>(accountDto.AddressDto));

            var user = new SportsStoreUser(accountDto.UserName, accountDto.Email, userInformation);
            return await base.CreateAsync(user, accountDto.Password);
        }

        public async Task<IdentityResult> UpdateAsync(
            int userId,
            AccountDto accountDto,
            IUserInformationService userInformationService)
        {
            var storedUser = await FindByIdAsync(userId);
            storedUser.ThrowIfNull();

            if (!string.IsNullOrWhiteSpace(accountDto.Password))
            {
                var validationResult = await PasswordValidator.ValidateAsync(accountDto.Password);
                if (!validationResult.Succeeded)
                {
                    return validationResult;
                }
            }

            storedUser.Update(accountDto, PasswordHasher);

            var identityResult = await base.UpdateAsync(storedUser);
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            var userInformation = new UserInformation(Mapper.Map<AddressDto, Address>(accountDto.AddressDto));
            var storedUserInformation = userInformationService.GetUserInformation(storedUser.Id);
            userInformation.UserId = storedUserInformation.UserId;
            userInformation.UserInformationId = storedUserInformation.UserInformationId;

            await userInformationService.EditUserInformation(userInformation);

            return identityResult;
        }
    }
}