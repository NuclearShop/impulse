using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImpulseApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ShortAdUrl",
                url: "ad/{shorturl}",
                defaults: new { controller = "AdOutbound", action = "OutboundHtml", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "ShortAbUrl",
                url: "ab/{shorturl}",
                defaults: new { controller = "AdOutbound", action = "OutboundAbTest", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
