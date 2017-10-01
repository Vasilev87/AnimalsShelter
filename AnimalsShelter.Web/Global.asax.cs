﻿using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AnimalsShelter.Data;
using AnimalsShelter.Data.Migrations;
using AnimalsShelter.Web.App_Start;

namespace AnimalsShelter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var mapper = new AutoMapperConfig();
            //mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
