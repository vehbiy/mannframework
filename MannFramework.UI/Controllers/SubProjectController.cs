using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MannFramework.Macondo.Controllers
{
    public class SubProjectController : GarciaEntityMvcController<SubProject>
    {
        protected override void AfterEdit()
        {
            UIStateManager.Instance.Project.SubProjects = EntityManager.Instance.GetItems<SubProject>("ProjectId", UIStateManager.Instance.Project.Id);
        }
    }
}