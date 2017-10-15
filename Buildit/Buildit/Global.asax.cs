using Buildit.App_Start;
using Buildit.Data;
using Buildit.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using System.Reflection;

namespace Buildit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //BuilditDbContext.Create().Database.Initialize(true);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BuilditDbContext, Configuration>());

            AutoMapperConfig.Execute(Assembly.Load("Buildit.Web.Models"));

            //ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}
