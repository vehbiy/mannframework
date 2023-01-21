using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class ItemProperty : ISearchable
    {
        public bool IsLazyLoad
        {
            get
            {
                return this.Type == ItemPropertyType.Class;
            }
        }

        public override bool CachingEnabled => true;

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ItemProperty)
            {
                return ((ItemProperty)obj).Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region ISearchable
        public string Title { get { return this.Name; } }
        public string Description { get { return this.Name; } }
        public string Icon { get { return ""; } }
        #endregion
    }
}
