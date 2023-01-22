/*
	This file was generated automatically by MannFramework Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using System.Web.Http.Routing;


namespace MannFramework.Application
{
    public partial class SubProject : Application.ApplicationEntity
    {
        public ProjectType ProjectType { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
        [NotSelected]
        [NotSaved]
        public int? ProjectId { get { return _projectId; } set { _projectId = value; } }

        #region Lazy load
        private Project _project;
        private int? _projectId;
        #endregion

        public SubProject()
        {
        }

    }
}

