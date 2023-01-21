using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class ProviderRepository
    {
        public static Dictionary<Type, ProviderBase> Providers { get; set; }

        static ProviderRepository()
        {
            Providers = new Dictionary<Type, ProviderBase>();
            AddAllProviders();
        }

        public static void AddAllProviders()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            AddProviders(assembly);
            //assembly = Assembly.GetExecutingAssembly();
            //AddProviders(assembly);
            assembly = Assembly.GetEntryAssembly();
            AddProviders(assembly);
        }

        private static void AddProviders(Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }

            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(ProviderBase)))
                {
                    if (type.BaseType != null)
                    {
                        Type[] genericArguments = type.BaseType.GetGenericArguments();

                        if (genericArguments != null && genericArguments.Length != 0)
                        {
                            Type entityType = genericArguments[0];

                            if (entityType != null && !entityType.IsGenericParameter && entityType.IsSubclassOf(typeof(Entity)) && !Providers.ContainsKey(entityType))
                            {
                                MethodInfo instanceMethod = type.GetMethod("GetInstance", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                                ProviderBase instance = (ProviderBase)instanceMethod.Invoke(null, new object[] { type });
                                Providers.Add(entityType, instance);
                            }
                        }
                    }
                }
                else if (type.IsSubclassOf(typeof(Entity)))
                {
                    TableMappingAttribute attribute = type.GetCustomAttribute<TableMappingAttribute>();
                    string name = type.Namespace + "." + type.Name;

                    if (!TypeToNameMapper.Instance.ContainsKey(name))
                    {
                        if (attribute != null)
                        {
                            TypeToNameMapper.Instance.Add(name, attribute.TableName);
                        }
                        else
                        {
                            TypeToNameMapper.Instance.Add(name, type.Name);
                        }
                    }
                }
            }
        }

        internal static ProviderBase GetProvider(Type propertyType)
        {
            return Providers.ContainsKey(propertyType) ? Providers[propertyType] : null;
        }
    }
}
