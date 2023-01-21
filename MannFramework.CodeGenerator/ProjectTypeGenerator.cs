using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class ProjectTypeGenerator : Dictionary<ProjectType, List<Type>>
    {
        public static ProjectTypeGenerator Instance { get; set; }

        static ProjectTypeGenerator()
        {
            Instance = new ProjectTypeGenerator();
        }

        private ProjectTypeGenerator()
        {
            //this.Add(ProjectType.BL, new EntityGenerator(), new ProviderGenerator());
            //this.Add(ProjectType.Desktop, null);
            //this.Add(ProjectType.MSSQL, new MssqlGenerator());
            //this.Add(ProjectType.MVC, new MvcControllerGenerator(), new MvcEditViewGenerator(), new MvcListViewGenerator(), new MvcModelGenerator());
            //this.Add(ProjectType.MySql, null);
            //this.Add(ProjectType.SocketIO, null);
            //this.Add(ProjectType.UnitTest, null);
            //this.Add(ProjectType.WebApi, new WebApiControllerGenerator(), new Angular2ControllerGenerator(), new WebApiModelGenerator());

            this.Add(ProjectType.BL, typeof(EntityGenerator), typeof(ProviderGenerator));
            this.Add(ProjectType.Desktop, null);
            this.Add(ProjectType.MSSQL, typeof(MssqlGenerator));
            this.Add(ProjectType.MVC, typeof(MvcControllerGenerator), typeof(MvcEditViewGenerator), typeof(MvcListViewGenerator), typeof(MvcModelGenerator));
            this.Add(ProjectType.MySql, null);
            this.Add(ProjectType.SocketIO, null);
            this.Add(ProjectType.UnitTest, null);
            this.Add(ProjectType.WebApi, typeof(WebApiControllerGenerator), typeof(WebApiModelGenerator));
            this.Add(ProjectType.Angular2, typeof(Angular2ControllerGenerator));
        }

        //private void Add(ProjectType key, params Generator[] values)
        //{
        //    this.Add(key, values == null ? new List<Generator>() : values.ToList());
        //}

        private void Add(ProjectType key, params Type[] values)
        {
            this.Add(key, values == null ? new List<Type>() : values.ToList());
        }

        public List<Generator> GetGenerators(Project project, ProjectType projectType, string baseFolder, string baseNamespace, List<string> includes)
        {
            ProjectType[] values = (ProjectType[])Enum.GetValues(projectType.GetType());
            List<ProjectType> projectTypes = values.Where(v => (v & projectType) != 0).ToList();
            List<Type> types = new List<Type>();

            foreach (ProjectType temp in projectTypes)
            {
                types.AddRange(this[temp]);
            }

            List<Generator> generators = new List<Generator>();

            if (types.Count != 0)
            {
                foreach (Type type in types)
                {
                    //Generator generator = ObjectFactory.CreateObject(type, baseFolder) as Generator;
                    Generator generator = (Generator)Activator.CreateInstance(type, new object[] { baseFolder, baseNamespace });
                    generator.Project = project;

                    if (generator != null)
                    {
                        if (includes != null && includes.Count != 0)
                        {
                            //generator.Includes.AddRange(includes.Where(x => x != generator.Namespace).ToList());
                            generator.Includes.AddRange(includes.Select(x => "using " + x + ";\n").ToList());
                        }

                        generators.Add(generator);
                    }
                }
            }

            return generators;
        }
    }
}
