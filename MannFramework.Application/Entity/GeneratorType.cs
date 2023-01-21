using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    [Flags]
    public enum GeneratorType
    {
        Entity = 1,
        Provider = 1 << 2,
        WebApiModel = 1 << 3,
        WebApiController = 1 << 4,
        MvcModel = 1 << 5,
        MvcEditView = 1 << 6,
        MvcListView = 1 << 7,
        MvcController = 1 << 8,
        Mssql = 1 << 9,
        Angular2Controller = 1 << 10,
        Html = 1 << 11
    }
}
