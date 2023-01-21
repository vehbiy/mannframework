using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MannFramework.Macondo.Controllers
{
    [AllowAnonymous]
    public class PluginController : GarciaMvcController
    {
        public PluginController()
        {
            ViewBag.DonotUseLayout = true;
        }

        public override ActionResult Index()
        {
            RouteConfig.RegisterRoutes(new RouteCollection());
            return View(RouteConfig.Routes);
        }
    }
}