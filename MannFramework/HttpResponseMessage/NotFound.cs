using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.HttpResponseMessage
{
    class NotFound : UnSuccessRequestMessage
    {
        public NotFound()
        {
            this.ErrorMessages.Add("Kayıt bulunamadı");
        }
        
    }
}
