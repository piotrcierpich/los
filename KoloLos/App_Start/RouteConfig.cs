﻿using System.Web.Mvc;
using System.Web.Routing;

using KoloLos.Models.Manager;

namespace KoloLos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          routes.MapRoute(name: "articleById",
                            url: "{category}/{id}",
                            defaults: new { controller = "Article", action = "ById" },
                            constraints: new { category=@"(^News$)|(^Resolutions$)", id = @"\d+" });

          routes.MapRoute(name: "articleByCategory",
                            url: "{category}",
                            defaults: new { controller = "Article", action = "ByCategory" },
                            constraints: new { category = @"(^History$)|(^Contact$)" });

          routes.MapRoute(name: "galleries",
                          url: "gallery/{action}/{id}",
                          defaults: new { controller = "gallery", action = "index", id = UrlParameter.Optional });

          routes.MapRoute(name: "listOfArticles",
                            url: "{listName}",
                            defaults: new { controller = "listOfArticles", action = "Index" },
                            constraints: new { listName = @"(^News$)|(^Resolutions$)" });

            routes.MapRoute(name: "galleryManager",
                            url: "manager/gallery/",
                            defaults: new { controller = "gallerymanager", action = "Index" });

            routes.MapRoute(name: "galleryManagerGalleryDetails",
                            url: "manager/gallery/{id}",
                            defaults: new { controller = "gallerymanager", action = "Edit" });

            routes.MapRoute(name: "galleryManagerImageAction",
                            url: "manager/gallery/{id}/{action}",
                            defaults: new { controller = "gallerymanager" });

            routes.MapRoute(name: "slideshowManager",
                            url: "manager/slideshow",
                            defaults: new { controller = "SlideShowManagerController" });

            routes.MapRoute(name: "manager",
                            url: "manager/{category}/{action}/{id}",
                            defaults: new { controller = "manager", category = UrlParameter.Optional, action = "Index", id = UrlParameter.Optional },
                            constraints: new { category = new CategoryConstraint() });

            routes.MapRoute(name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}