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
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
	public partial class ProjectGroup :  ApplicationEntity
	{
		[Required]
		[StringLength(100, MinimumLength = 1)]
		public string Name { get; set; }
		[Required]
		public int Priority { get; set; }
		public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
		[NotSelected]
        [NotSaved]
		public int? ProjectId { get { return _projectId; } set { _projectId = value; } }

		#region Lazy load
		private Project _project;
		private int? _projectId;
		#endregion

		public ProjectGroup()
		{
		}

	}
}

