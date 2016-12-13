using System.Web.Mvc;
using System.Web.Routing;

namespace TrustSystems3.ClientWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "ReviewAjax",
                url: "reviewajax/{action}/{id}",
                defaults: new { controller = "Review", action = "Like", id = UrlParameter.Optional }
            );

            //Company reviews
            routes.MapRoute(
                name: "Review",
                url: "review/{slug}",
                defaults: new { controller = "Review", action = "Index", slug = UrlParameter.Optional }
            );

            //Show review by id
            routes.MapRoute(
                name: "UserReview",
                url: "showreview/{id}",
                defaults: new { controller = "Review", action = "Show", id = UrlParameter.Optional }
            );
            

            routes.MapRoute(
               name: "Evaluate",
               url: "evaluate/{slug}",
               defaults: new { controller = "Review", action = "Evaluate", slug = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "UserReviews",
               url: "users/{id}",
               defaults: new { controller = "Review", action = "UserReviews", id = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "Categories lang",
                url: "{culture}/categories/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = @"[a-z]{2,3}(?:-[A-Z]{2,3})?" }
            );
            routes.MapRoute(
                name: "Categories",
                url: "categories/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ShowCategory",
                url: "category/{slug}",
                defaults: new { controller = "Category", action = "CategoryCompanies", slug = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "Pages",
              url: "pages/{action}/{id}",
              defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
          );



            routes.MapRoute(
                name: "Default lang",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = @"[a-z]{2,3}(?:-[A-Z]{2,3})?" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}