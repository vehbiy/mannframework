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
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
	public partial class MenuItemRole : ApplicationEntity
	{
		public MenuItem MenuItem { get { return Get(_menuItemId, ref _menuItem); } set { Set(ref _menuItem, ref _menuItemId, value); } }
		[NotSelected]
        [NotSaved]
		public int? MenuItemId { get { return _menuItemId; } set { _menuItemId = value; } }
		public Role Role { get { return Get(_roleId, ref _role); } set { Set(ref _role, ref _roleId, value); } }
		[NotSelected]
        [NotSaved]
		public int? RoleId { get { return _roleId; } set { _roleId = value; } }

		#region Lazy load
		private MenuItem _menuItem;
		private int? _menuItemId;
		private Role _role;
		private int? _roleId;
		#endregion

		public MenuItemRole()
		{
		}
	}
}

