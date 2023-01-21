using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    [Serializable]
    public class LocalizationItem : ApplicationEntity
    {
        public string Code { get; set; }
        public string CultureCode { get; set; }
        public string Value { get; set; }
        public override bool CachingEnabled => true;

        internal LocalizationItem()
        {
        }
    }
}
