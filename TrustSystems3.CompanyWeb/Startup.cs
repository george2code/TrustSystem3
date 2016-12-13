using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrustSystems3.CompanyWeb.Startup))]
namespace TrustSystems3.CompanyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
