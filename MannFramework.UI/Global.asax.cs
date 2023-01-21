using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using Newtonsoft.Json;
using MannFramework.Application;
using MannFramework.Macondo.BL;
using System.Security.Principal;

namespace MannFramework.Macondo
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ViewEngineConfig.RegisterViewEngines(ViewEngines.Engines);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //Startup.StartAll();
            List<Language> languages = EntityManager.Instance.GetItems<Language>();
            DependencyManager.Localizer = new GarciaApplicationLocalizer(languages.Select(x => x.CultureCode).ToList());
            GlobalFilters.Filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            ModelBinders.Binders.DefaultBinder = new FlagEnumModelBinder();
            UIStateManager.Instance.SelectProject(1);

            //Project project = EntityManager.Instance.GetItem<Project>(1);

            //if (project != null)
            //{
            //    ProjectSetting setting = project.GetProjectSetting();
            //    GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaConfiguration), setting);
            //    GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaApplicationConfiguration), setting);
            //}
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (GarciaConfiguration.CurrentMode == GarciaModeType.Production)
            {
                Exception exception = Server.GetLastError();
                Response.Clear();
                HttpException httpException = exception as HttpException;
                bool redirectToHome = false;

                if (httpException != null)
                {
                    //string action;

                    //switch (httpException.GetHttpCode())
                    //{
                    //    case 404:
                    //        action = "HttpError404";
                    //        break;
                    //    case 412:
                    //        action = "HttpError412";
                    //        redirectToHome = true;
                    //        break;
                    //    case 500:
                    //        action = "HttpError500";
                    //        break;
                    //    default:
                    //        action = "General";
                    //        break;
                    //}

                    Server.ClearError();

                    if (redirectToHome)
                    {
                        Response.Redirect("~/");
                    }
                    else
                    {
                        Response.Redirect("~/Error/Index");
                    }
                }
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //Member member = UIStateManager.Instance.Member;
            //IIdentity identity = User.Identity;
            //int x = 4;
        }
    }
}