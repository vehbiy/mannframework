using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace MannFramework.Macondo
{
    public class RouteConfig
    {
        public static Dictionary<string, List<string>> Routes { get; set; }

        static RouteConfig()
        {
            Routes = new Dictionary<string, List<string>>();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Dashboard", action = "Index" }
            );

            //routes.MapCodeRoutes(typeof(MvcContact.ContactController), new CodeRoutingSettings() { EnableEmbeddedViews = true });

            string pluginDirectory = ConfigurationManager.AppSettings["GarciaPluginDirectory"];
            pluginDirectory = "";

            #region Commented
            //if (!string.IsNullOrEmpty(pluginDirectory))
            //{
            //    IEnumerable<string> files = Directory.EnumerateFiles(pluginDirectory);

            //    foreach (string file in files)
            //    {
            //        Assembly ass = null;

            //        //try
            //        {
            //            //ass = Assembly.LoadFile(file);
            //            ass = Assembly.LoadFrom(file);

            //            if (ass != null)
            //            {
            //                if (!Routes.ContainsKey(ass.FullName))
            //                {
            //                    Routes.Add(ass.FullName, new List<string>());
            //                }

            //                foreach (Type type in ass.DefinedTypes)
            //                {
            //                    //if (type.IsSubclassOf(typeof(Controller)))
            //                    if (typeof(Controller).IsAssignableFrom(type))
            //                    {
            //                        string name = type.Name.Replace("Controller", "");

            //                        //if (name.ToLowerInvariant().Contains("swagger"))
            //                        //{
            //                        //    continue;
            //                        //}

            //                        //routes.MapCodeRoutes(
            //                        //    baseRoute: name,
            //                        //    rootController: type,
            //                        //    settings: new CodeRoutingSettings
            //                        //    {
            //                        //        EnableEmbeddedViews = true,
            //                        //        //RouteFormatter = args =>
            //                        //        //{
            //                        //        //    return args.OriginalSegment + "dssd";
            //                        //        //}
            //                        //    }
            //                        //);

            //                        //routes.MapRoute(
            //                        //    namespaces: new string[] { type.Namespace },
            //                        //    name: name,
            //                        //    url: "{controller}/{action}"
            //                        //);

            //                        //routes.MapCodeRoutes(type, new CodeRoutingSettings() { EnableEmbeddedViews = true });

            //                        if (!Routes[ass.FullName].Contains(name))
            //                        {
            //                            Routes[ass.FullName].Add(name);
            //                        }
            //                    }
            //                }

            //                ass = null;
            //            }
            //        }
            //        //catch (Exception ex)
            //        //{
            //        //    throw;
            //        //}
            //    }
            //} 
            #endregion
        }
    }

    #region Commented
    //public class Initializer
    //{
    //    public static void Initialize()
    //    {
    //        string pluginDirectory = ConfigurationManager.AppSettings["GarciaPluginDirectory"];

    //        if (!string.IsNullOrEmpty(pluginDirectory))
    //        {
    //            IEnumerable<string> files = Directory.EnumerateFiles(pluginDirectory);

    //            foreach (string file in files)
    //            {
    //                Assembly ass = null;

    //                //try
    //                {
    //                    //ass = Assembly.LoadFile(file);
    //                    ass = Assembly.LoadFrom(file);

    //                    if (ass != null)
    //                    {
    //                        BuildManager.AddReferencedAssembly(ass);
    //                        BuildManager.AddCompilationDependency(ass.FullName);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //} 
    #endregion
}
