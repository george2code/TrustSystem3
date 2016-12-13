using System.Web.Mvc;
using System.Web.Routing;

namespace TrustSystems3.CompanyWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product",
                url: "product/{action}/{id}",
                defaults: new { controller = "Product", action = "Review", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Resources",
                url: "resources/{action}/{id}",
                defaults: new { controller = "Resources", action = "All", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "WizardAddInvitations",
                url: "dashboard/invitations/invitecustomers/add/{action}/{id}",
                defaults: new { controller = "WizardAddInvites", action = "Add", id = UrlParameter.Optional });
            routes.MapRoute(
               name: "WizarImportInvitations",
               url: "dashboard/invitations/invitecustomers/import/{action}/{id}",
               defaults: new { controller = "WizardImportInvites", action = "Add", id = UrlParameter.Optional });


            routes.MapRoute(
                name: "Invitations",
                url: "dashboard/invitations/{action}/{id}",
                defaults: new { controller = "Invitations", action = "History", id = UrlParameter.Optional }
            );

          


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}