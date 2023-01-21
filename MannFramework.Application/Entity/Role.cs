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
    public partial class Role : ApplicationEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public List<MenuItemRole> AllowedMenuItems { get { return Get(ref _allowedMenuItems); } set { Set(ref _allowedMenuItems, value); } }

        #region Lazy load
        private List<MenuItemRole> _allowedMenuItems;
        #endregion

        public Role()
        {
            this.AllowedMenuItems = new List<MenuItemRole>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

