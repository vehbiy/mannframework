using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class MvcControllerGenerator : Generator<MvcControllerTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.MvcController; } }
        public override string Namespace { get { return this.BaseNamespace + ".Controllers"; } }
        
        public MvcControllerGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
            //this.GenerateInnerItems = false;

            if (GarciaGeneratorConfiguration.GenerateMvcModel)
            {
                MvcModelGenerator generator = new MvcModelGenerator(baseFolder, baseNamespace);

                if (!string.IsNullOrEmpty(generator.Namespace) && !this.Includes.Contains(generator.Namespace))
                {
                    this.Includes.Add(generator.Namespace);
                }
            }
        }

        public override string GetFileName(Item item)
        {
            return "Controllers\\" + item.Name + "Controller.cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Controllers",
                item.Name + "Controller.cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("BaseMvcControllerName", GarciaGeneratorConfiguration.BaseMvcControllerName);
            this.AddParameter("GenerateGetOne", false);
            this.AddParameter("GenerateGetAll", false);
            this.AddParameter("GeneratePost", false);
            this.AddParameter("GenerateDelete", false);
            this.AddParameter("GenerateInnerGet", false); // TODO: gerek yok sanki
            this.AddParameter("GenerateMvcModel", GarciaGeneratorConfiguration.GenerateMvcModel);
        }
    }
}
