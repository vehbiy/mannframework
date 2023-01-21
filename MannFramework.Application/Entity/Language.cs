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
using System.Web.Http.Routing;


namespace MannFramework.Application
{
    public partial class Language : ApplicationEntity
    {
        public string CultureCode { get; set; }
        [Order]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public override bool CachingEnabled => true;

        #region Lazy load
        #endregion  

        public Language()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
