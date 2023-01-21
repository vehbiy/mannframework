using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public abstract class Startup
    {
        public abstract void Start();

        public static void StartAll()
        {
            //DateTime dt1 = DateTime.Now;
            AssemblyName[] assemblies = Assembly.GetCallingAssembly().GetReferencedAssemblies();

            foreach (AssemblyName assemblyName in assemblies)
            {
                Assembly assembly = Assembly.Load(assemblyName.ToString());

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Startup)))
                    {
                        Startup instance = (Startup)Activator.CreateInstance(type);
                        instance.Start();
                    }
                }
            }

            //DateTime dt2 = DateTime.Now;
            //TimeSpan ts = dt2 - dt1;
            //Debug.WriteLine("Startup in " + ts.TotalMilliseconds.ToString());
        }
    }
}