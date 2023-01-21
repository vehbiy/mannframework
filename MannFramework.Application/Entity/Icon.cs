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
    public partial class Icon : ApplicationEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 0)]
        public string CssClass { get; set; }
        [StringLength(100, MinimumLength = 0)]
        public string Image { get; set; }

        #region Lazy load
        #endregion

        public Icon()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

