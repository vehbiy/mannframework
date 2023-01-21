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
using MannFramework.CodeGenerator;
using Microsoft.AspNet.Identity;

namespace MannFramework.Macondo
{
    public class UIStateManager : StateManager<UIStateManager>
    {
        //public Project Project { get { return Read<Project>(); } set { Write<Project>(value); } }
        public Project Project
        {
            get
            {
                Project project = Read<Project>();

                if (project == null)
                {
                    project = this.Member.DefaultProject;

                    if (project != null)
                    {
                        this.SelectProject(project.Id);
                    }

                    this.ProjectId = project?.Id.ToString();
                }

                return project;
            }
            set
            {
                Write<Project>(value);
                this.ProjectId = value?.Id.ToString();
            }
        }

        public string ProjectId
        {
            get { return Read<string>("ProjectId"); }
            set { Write("ProjectId", value); }
        }

        public void RefreshProject()
        {
            //this.Project = EntityManager.Instance.GetItem<Project>(this.Project.Id);
            this.SelectProject(this.Project.Id);
        }

        public bool CheckProject()
        {
            return this.Project != null && (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ProjectId"]) || this.Project.Id.ToString() == HttpContext.Current.Request.QueryString["ProjectId"]);
        }

        public void SelectProject(int id)
        {
            Project project = EntityManager.Instance.GetItem<Project>(id);

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.Project = project;
            }

            if (project != null)
            {
                ProjectSetting setting = project.GetProjectSetting();
                GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaConfiguration), setting);
                GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaApplicationConfiguration), setting);
                GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaGeneratorConfiguration), setting);
            }
        }
    }
}
