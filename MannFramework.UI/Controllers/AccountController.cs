using MannFramework.Application;
using MannFramework.Application.Manager;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Mvc.Html;

namespace MannFramework.Macondo.Controllers
{
    [AllowAnonymous]
    public class AccountController : Application.GarciaAccountController
    {
        protected override void InnerLogout()
        {
            base.InnerLogout();
            UIStateManager.Instance.Project = null;
        }
    }
}