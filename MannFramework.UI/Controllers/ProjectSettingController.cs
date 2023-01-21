using MannFramework.Application;
using MannFramework.CodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MannFramework.Macondo.Controllers
{
    public class ProjectSettingController : GarciaEntityMvcController<ProjectSetting>
    {
        [HttpGet]
        public override ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                string projectIdString = Request.QueryString["ProjectId"];

                if (!string.IsNullOrEmpty(projectIdString))
                {
                    int projectId = Helpers.GetValueFromObject<int>(projectIdString);

                    if (projectId != 0)
                    {
                        EntityManager.Instance.ClearCache(typeof(Project).FullName);
                        Project project = EntityManager.Instance.GetItem<Project>(projectId);
                        ProjectSetting item = project.GetProjectSetting();

                        if (item != null && item.Id != 0)
                        {
                            return RedirectToAction("Edit", new RouteValueDictionary() { { "Id", item.Id } });
                        }
                        else
                        {
                            return View(item);
                        }
                    }
                }
            }

            return base.Edit(id);
        }

        protected override void AfterEdit()
        {
            UIStateManager.Instance.RefreshProject();
        }
    }
}