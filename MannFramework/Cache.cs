using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public abstract class Cache
    {
        public abstract object this[string key] { get; set; }
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        public abstract void Add(string typeName, object value);
        public abstract void Remove(string fullName);
    }
}
