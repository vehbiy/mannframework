using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class TypeToNameMapper : Dictionary<string, string>
    {
        public static TypeToNameMapper Instance { get; protected set; }

        static TypeToNameMapper()
        {
            Instance = new TypeToNameMapper();
        }

        public string GetEntityNameFromMapper(Type type)
        {
            //string key = type.Assembly.FullName + "." + type.FullName;
            string key = this.GetKeyName(type);
            return this.ContainsKey(key) ? this[key] : type.Name;
        }

        public void Add(Type type, string mapping)
        {
            this.Add(this.GetKeyName(type), mapping);
        }

        private string GetKeyName(Type type)
        {
            return type.Namespace + "." + type.Name;
        }
    }
}
