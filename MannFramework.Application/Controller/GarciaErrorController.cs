using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MannFramework.Factory;
using MannFramework.Interface;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;

namespace MannFramework.Application.Controller
{
    public class GarciaErrorController : System.Web.Mvc.Controller
    {
        public virtual ActionResult Index()
        {
            Exception exception = Server.GetLastError();
            string message = string.Empty;

            if (exception != null)
            {
                message = exception.Message;
            }

            ViewBag.Message = message;
            Server.ClearError();
            return View();
        }
    }
}
