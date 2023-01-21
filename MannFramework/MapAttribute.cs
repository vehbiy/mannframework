using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GarciaMappingAttribute : Attribute
    {
        public MappingType MappingType { get; set; }

        public GarciaMappingAttribute(MappingType MappingType)
        {
            this.MappingType = MappingType;
        }
    }
}
