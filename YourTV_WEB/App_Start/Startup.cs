using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using YourTV_BLL.Services;
using Microsoft.AspNet.Identity;
using YourTV_BLL.Interfaces;

[assembly: OwinStartup(typeof(YourTV_WEB.App_Start.Startup))]

namespace YourTV_WEB.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("YourTV");
        }
    }
}