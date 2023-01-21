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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
    public partial class Project : MannFramework.Application.ApplicationEntity
    {
        [WhitespaceValidation]
        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public List<SubProject> SubProjects { get { return Get(ref _subProjects); } set { Set(ref _subProjects, value); } }
        [Required]
        public GeneratorType GeneratorTypes { get; set; }
        [Required]
        public List<Item> Items { get { return Get(ref _items); } set { Set(ref _items, value); } }

        #region Lazy load
        private List<SubProject> _subProjects;
        private List<Item> _items;
        #endregion

        public Project()
        {
            this.SubProjects = new List<SubProject>();
            this.Items = new List<Item>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

