using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public enum AccessorType
    {
        [Default]
        Public = 0,
        Private,
        Protected,
        Internal,
        ProtectedInternal
    }
}
