using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrustSystems3.ClientWeb.Startup))]
namespace TrustSystems3.ClientWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
