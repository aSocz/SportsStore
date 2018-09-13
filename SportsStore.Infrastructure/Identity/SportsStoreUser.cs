using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsStore.Business.Extensions;
using SportsStore.Business.Models;
using SportsStore.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Identity
{
    public class SportsStoreUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public SportsStoreUser()
        {
        }

        public SportsStoreUser(string userName, string email, UserInformation userInformation)
        {
            userName.ThrowIfNullOrEmpty();
            email.ThrowIfNullOrEmpty();
            userInformation.ThrowIfNull();

            UserInformation = userInformation;
            UserName = userName;
            Email = email;
        }

        public int UserInformationId { get; protected set; }
        public virtual UserInformation UserInformation { get; protected set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SportsStoreUserManager manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void Update(AccountDto updatedUserInfo, IPasswordHasher passwordHasher)
        {
            updatedUserInfo.ThrowIfNull();
            passwordHasher.ThrowIfNull();

            Email = updatedUserInfo.Email;
            UserName = updatedUserInfo.UserName;
            PasswordHash = passwordHasher.HashPassword(updatedUserInfo.Password);
        }
    }
}