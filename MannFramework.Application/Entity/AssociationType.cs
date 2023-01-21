using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public enum AssociationType
    {
        [Default]
        Composition = 0,
        Aggregation // continues to live, select with join
    }
}
