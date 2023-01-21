using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class ValidationResults : List<ValidationResult>
    {
        public bool IsValid
        {
            get
            {
                return this.Count(x => !x.IsValid) == 0;
            }
        }
    }

    public class ValidationResults<T> : List<ValidationResult<T>>
    {
        public bool IsValid
        {
            get
            {
                return this.Count(x => !x.IsValid) == 0;
            }
        }
    }
}
