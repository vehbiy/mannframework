/*
	This file was generated automatically by Garcia Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using System.Web.Http.Routing;
using MannFramework.CodeGenerator;
using System.Web.Http;
using MannFramework.Application;

namespace MannFramework.WebApi.Controllers
{
    public partial class ProjectController : GarciaEntityApiController<Project>
    {
        [Route("api/Project/{id}/")]
        public GarciaHttpResponseMessage<List<SubProject>> GetSubProjects(int id)
        {
            Project entity = EntityManager.Instance.GetItem<Project>(id);
            GarciaHttpResponseMessage<List<SubProject>> item = new GarciaHttpResponseMessage<List<SubProject>>(entity.SubProjects);
            return item;
        }

        //[Route("api/Project/{id}/")]
        //public GarciaHttpResponseMessage<List<ProjectSetting>> GetProjectSettings(int id)
        //{
        //    Project entity = EntityManager.Instance.GetItem<Project>(id);
        //    GarciaHttpResponseMessage<List<ProjectSetting>> item = new GarciaHttpResponseMessage<List<ProjectSetting>>(entity.Settings);
        //    return item;
        //}
    }
}