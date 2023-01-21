using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class CacheFactory
    {
        public static Cache CreateCache()
        {
            return new InProcessCache();
        }
    }
}
