using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public interface ILocalizer
    {
        string Localize(string cultureCode, string key);
    }
}
