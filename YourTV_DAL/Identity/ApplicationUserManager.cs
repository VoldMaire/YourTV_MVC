using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using YourTV_DAL.Entities;

namespace YourTV_DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {}
    }
}
