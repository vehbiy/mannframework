using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.HttpResponseMessage
{
    class BadRequest : UnSuccessRequestMessage
    {
        public BadRequest()
        {
            this.ErrorMessages.Add("Eksik veri girildi");
        }
    }
}
