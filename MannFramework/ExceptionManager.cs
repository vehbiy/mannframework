using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class ExceptionManager
    {
        public static void HandleException(Exception Exception)
        {
            // TODO: log

            if (MannFrameworkConfiguration.CurrentMode == MannFrameworkModeType.Development)
            {
                throw Exception;
            }
        }
    }
}
