using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.HttpResponseMessage
{
    class NoContent : UnSuccessRequestMessage
    {
        public NoContent()
        {
            this.ErrorMessages.Add("Veri bulunamadı.");
        }
    }
}
