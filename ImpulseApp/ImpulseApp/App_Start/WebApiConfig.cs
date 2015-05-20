using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImpulseApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.MapHttpAttributeRoutes();
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );
            config.Routes.MapHttpRoute(
                name: "NewDefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }
}
