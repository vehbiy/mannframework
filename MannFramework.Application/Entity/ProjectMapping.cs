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
    public partial class ProjectMapping : ApplicationEntity
    {
        public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
        [NotSelected]
        [NotSaved]
        public int? ProjectId { get { return _projectId; } set { _projectId = value; } }
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string LocalPath { get; set; }
        public Member Member { get { return Get(_memberId, ref _member); } set { Set(ref _member, ref _memberId, value); } }
        [NotSelected]
        [NotSaved]
        public int? MemberId { get { return _memberId; } set { _memberId = value; } }
        [Required]
        public bool IsDefault { get; set; }
        [Required]
        public ProjectMappingType MappingType { get; set; }
        public override bool CachingEnabled => true;

        #region Lazy load
        private Project _project;
        private int? _projectId;
        private Member _member;
        private int? _memberId;
        #endregion

        public ProjectMapping()
        {
        }

    }
}

