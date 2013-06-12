using System.Web.Mvc;
using System.Web.Routing;

namespace KoloLos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "listOfArticles",
                            url: "{listName}/{pageIndex}",
                            defaults: new { controller = "listOfArticles", action = "Index", pageIndex = 0 },
                            constraints: new { listName = @"(^News$)|(^Resolutions$)", pageIndex = @"\b\d+\b" });

            routes.MapRoute(name: "articlesDetails",
                            url: "Details/{id}",
                            defaults: new { controller = "listOfArticles", action = "Details", id = 0 });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}