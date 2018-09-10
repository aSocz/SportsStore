using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.Infrastructure.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }

    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<SportsStoreUser, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public UserStore(SportsStoreContext context) : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(SportsStoreContext context) : base(context)
        {
        }
    }
}