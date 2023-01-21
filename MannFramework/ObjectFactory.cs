using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class ObjectFactory
    {
        public static TObject CreateObject<TObject>()
        {
            //return (TObject)Activator.CreateInstance(typeof(TObject), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
            return CreateObject<TObject>(null);
        }

        public static TObject CreateObject<TObject>(params object[] Parameters)
        {
            return (TObject)Activator.CreateInstance(typeof(TObject), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null, Parameters);
        }

        public static object CreateObject(Type Type)
        {
            return CreateObject(Type, null);
        }

        public static object CreateObject(Type Type, params object[] Parameters)
        {
            return Activator.CreateInstance(Type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null, Parameters);
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
