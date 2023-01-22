using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MannFrameworkMappingAttribute : Attribute
    {
        public MappingType MappingType { get; set; }

        public MannFrameworkMappingAttribute(MappingType MappingType)
        {
            this.MappingType = MappingType;
        }
    }
}
