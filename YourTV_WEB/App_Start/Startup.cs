using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using YourTV_BLL.Services;
using Microsoft.AspNet.Identity;
using YourTV_BLL.Interfaces;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

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
            IUserService userService = serviceCreator.CreateUserService("YourTV");
            userService.EmailService = new EmailService();
            userService.SetDefaultTokenProvider();
            return userService;
        }
    }
}