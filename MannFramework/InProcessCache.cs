using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class InProcessCache : Cache
    {
        private Dictionary<string, object> items = new Dictionary<string, object>();

        public override object this[string Key]
        {
            get
            {
                if (!this.items.ContainsKey(Key))
                {
                    return null;
                }

                return this.items[Key];
            }
            set
            {
                this.items[Key] = value;
            }
        }

        public override void Add(string TypeName, object Value)
        {
            this.items[TypeName] = Value;
        }

        public override void Remove(string TypeName)
        {
            this.items[TypeName] = null;
        }
    }
}
