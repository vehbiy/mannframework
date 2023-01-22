﻿/*
	This file was generated automatically by MannFramework Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
    public partial class Item : MannFramework.Application.ApplicationEntity
    {
        [Order]
        public string Name { get; set; }
        public List<ItemProperty> Properties { get { return Get(ref _properties); } set { Set(ref _properties, value); } }
        public bool IsEnum { get; set; }
        public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
        [NotSelected]
        [NotSaved]
        public int? ProjectId { get { return _projectId; } set { _projectId = value; } }

        #region Lazy load
        private List<ItemProperty> _properties;
        private Project _project;
        private int? _projectId;
        #endregion

        public Item()
        {
            this.Properties = new List<ItemProperty>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

