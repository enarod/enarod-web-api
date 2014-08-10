using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			if (ConfigurationManager.AppSettings["CorsEnabled"] == "true") config.EnableCors();

            // Web API configuration and services
			config.MapHttpAttributeRoutes();

			// Web API routes
			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
