using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MannFramework.Application;
using MannFramework.Macondo.BL;
using MannFramework.Application.Controller;

namespace MannFramework.Macondo.Controllers
{
    public class DashboardController : GarciaDashboardController
    {
        // GET: Dashboard
        public override ActionResult Index()
        {
            if (UIStateManager.Instance.Project == null)
            {
                return RedirectToAction("List", "Project");
            }

            return View();
        }
    }
}