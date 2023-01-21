using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [Serializable]
    public class DatabaseResponse : List<Dictionary<string, object>>
    {
        public DatabaseResponse()
        {
        }

        public DatabaseResponse(IEnumerable<Dictionary<string, object>> items) : base(items)
        {
        }
    }
}
