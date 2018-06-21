using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace XSIS.SHOP.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "CustomerMethodApiParam1",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { action= "get", id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
             name: "CustomerMethodApiParam2",
             routeTemplate: "api/{controller}/{action}/{id}/{id2}",
             defaults: new { action = "get", id = RouteParameter.Optional, id2 = RouteParameter.Optional}
         );
      //      config.Routes.MapHttpRoute(
      //    name: "CustomerMethodApiParam2",
      //    routeTemplate: "api/{controller}/{action}/{id}/{id2/{id3}",
      //    defaults: new { action = "get", id = RouteParameter.Optional, id2 = RouteParameter.Optional, id3 = RouteParameter.Optional }
      //);
        }
    }
}
