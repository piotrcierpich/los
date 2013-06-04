﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using KoloLosDataLayer;

using KoloLosLogic;

namespace KoloLos
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ArticlesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LosDataContext>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<LosDataContext>().Articles).As<IDbSet<Article>>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<LosDataContext>().Categories).As<IDbSet<Category>>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}