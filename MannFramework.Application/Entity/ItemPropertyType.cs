using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public enum ItemPropertyType
    {
        Integer = 0,
        Double,
        Float,
        Decimal,
        DateTime,
        TimeSpan,
        [Default]
        String,
        Char,
        Class,
        Unknown,
        Boolean,
        Enum
    }
}
