using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public abstract class SingletonBase
    {
        private static HybridDictionary instances = new HybridDictionary();

        public static object GetInstance(Type Type)
        {
            string fullName = Type.FullName;

            if (!instances.Contains(fullName))
            {
                lock (typeof(object))
                {
                    if (!instances.Contains(fullName))
                    {
                        object instance = Activator.CreateInstance(Type, true);
                        instances.Add(fullName, instance);
                    }
                }
            }

            return instances[fullName];
        }

        protected SingletonBase()
        {
        }
    }

    public abstract class SingletonBase<T> : SingletonBase
    {
        public static T Instance
        {
            get
            {
                return (T)GetInstance(typeof(T));
            }
        }

        protected SingletonBase()
        {
        }
    }
}
