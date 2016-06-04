using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace MindgemAPI.Routes
{
    public class RoutesHandler
    {
        public class Route
        {
            public string Name { get; set; }
            public string Template { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Method { get; set; }
            public bool Logging { get; set; }
        }

        public class RoutesConfig
        {
            public List<Route> Routes { get; set; }

            public RoutesConfig()
            {
                Routes = new List<Route>();
            }
        }

        public class RoutesConfigSectionHandler : IConfigurationSectionHandler
        {
            public object Create(object parent, object configContext, XmlNode section)
            {
                var routesConfig = new RoutesConfig();

                var routeNodes = section.SelectNodes("route");
                if (routeNodes != null)
                {
                    foreach (XmlElement element in routeNodes)
                    {
                        var route = new Route
                        {
                            Name = element.GetAttribute("name"),
                            Template = element.GetAttribute("template"),
                            Controller = element.GetAttribute("controller"),
                            Action = element.GetAttribute("action"),
                            Method = element.GetAttribute("method"),
                            Logging = Convert.ToBoolean(element.GetAttribute("logging"))
                        };

                        routesConfig.Routes.Add(route);
                    }
                }

                return routesConfig;
            }
        }
    }
}