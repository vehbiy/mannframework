using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Interface
{
    internal interface IGarciaHttpController
    {
        IHttpResponseFactory HttpResponseMessageFactory { get; set; }
    }
}
