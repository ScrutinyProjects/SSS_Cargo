using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CargoWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{param1}",
                defaults: new { controller = "account", action = "login", param1 = UrlParameter.Optional }
            );

           // routes.MapRoute(
           //    name: "login",
           //    url: "login/{param1}",
           //    defaults: new { controller = "account", action = "login", param1 = UrlParameter.Optional }
           //);
        }
    }
}
