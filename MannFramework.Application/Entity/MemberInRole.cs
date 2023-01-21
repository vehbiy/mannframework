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
	public partial class MemberInRole : ApplicationEntity
	{
		public Member Member { get { return Get(_memberId, ref _member); } set { Set(ref _member, ref _memberId, value); } }
		[NotSelected]
        [NotSaved]
		public int? MemberId { get { return _memberId; } set { _memberId = value; } }
		public Role Role { get { return Get(_roleId, ref _role); } set { Set(ref _role, ref _roleId, value); } }
		[NotSelected]
        [NotSaved]
		public int? RoleId { get { return _roleId; } set { _roleId = value; } }

		#region Lazy load
		private Member _member;
		private int? _memberId;
		private Role _role;
		private int? _roleId;
		#endregion

		public MemberInRole()
		{
		}
	}
}

