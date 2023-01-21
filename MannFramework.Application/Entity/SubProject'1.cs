using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class SubProject : ISearchable
    {
        #region ISearchable
        public string Title { get { return this.Name; } }
        public string Description { get { return this.Name; } }
        public string Icon { get { return ""; } }
        #endregion
    }
}
