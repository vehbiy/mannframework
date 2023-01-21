using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    // TODO: ne ise yaradigina bakmak lazim
    public interface IParameterUpdater
    {
        Dictionary<string, object> UpdateParameterValues(Dictionary<string, object> Parameters);
    }
}
