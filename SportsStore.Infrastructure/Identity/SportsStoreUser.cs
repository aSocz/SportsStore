using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsStore.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Identity
{
    public class SportsStoreUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public SportsStoreUser(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        public int UserInformationId { get; set; }
        public virtual UserInformation UserInformation { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SportsStoreUserManager manager) =>
            await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        public void Update(SportsStoreUser updatedUser, string password, IPasswordHasher passwordHasher)
        {
            Email = updatedUser.Email;
            UserName = updatedUser.UserName;
            if (!string.IsNullOrWhiteSpace(password))
            {
                PasswordHash = passwordHasher.HashPassword(password);
            }
        }
    }
}