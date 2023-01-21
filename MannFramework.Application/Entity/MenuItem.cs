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
    public partial class MenuItem : ApplicationEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Text { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Url { get; set; }
        public Icon Icon { get { return Get(_iconId, ref _icon); } set { Set(ref _icon, ref _iconId, value); } }
        [NotSelected]
        [NotSaved]
        public int? IconId { get { return _iconId; } set { _iconId = value; } }
        [Required]
        public int Order { get; set; }
        public MenuItem Parent { get { return Get(_parentId, ref _parent); } set { Set(ref _parent, ref _parentId, value); } }
        [NotSelected]
        [NotSaved]
        public int? ParentId { get { return _parentId; } set { _parentId = value; } }
        [Required]
        public List<MenuItemRole> AllowedRoles { get { return Get(ref _allowedRoles); } set { Set(ref _allowedRoles, value); } }

        #region Lazy load
        private Icon _icon;
        private int? _iconId;
        private MenuItem _parent;
        private int? _parentId;
        private List<MenuItemRole> _allowedRoles;
        #endregion

        public MenuItem()
        {
            this.AllowedRoles = new List<MenuItemRole>();
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}

