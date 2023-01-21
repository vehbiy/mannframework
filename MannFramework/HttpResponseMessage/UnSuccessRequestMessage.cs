using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.HttpResponseMessage
{
    class UnSuccessRequestMessage : GarciaHttpResponseMessage
    {
        public UnSuccessRequestMessage()
        {
            this.Success = false;
        }
    }
}
