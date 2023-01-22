using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Interface
{
    internal interface IMannFrameworkHttpController
    {
        IHttpResponseFactory HttpResponseMessageFactory { get; set; }
    }
}
