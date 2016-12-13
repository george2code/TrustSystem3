using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrustSystems3.AdminWeb.Startup))]
namespace TrustSystems3.AdminWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
