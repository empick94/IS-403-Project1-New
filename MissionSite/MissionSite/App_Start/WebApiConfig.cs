using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MissionSite.App_Start
{
    public class WebApiConfig
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
                {
                    config.MapHttpAttributeRoutes();

                    config.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional }
                        );
                });
        }

        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}