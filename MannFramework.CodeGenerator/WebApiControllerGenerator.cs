using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MannFramework.CodeGenerator.Template;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public class WebApiControllerGenerator : Generator<WebApiControllerTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.WebApiController; } }
        //public override string Namespace { get { return this.BaseNamespace + ".Controllers"; } }
        public override string Namespace { get { return this.BaseNamespace + "." + GarciaGeneratorConfiguration.WebApiControllerNamespaceSuffix; } }

        public WebApiControllerGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
            this.GenerateInnerItems = false;

            if (GarciaGeneratorConfiguration.GenerateWebApiModel)
            {
                WebApiModelGenerator generator = new WebApiModelGenerator(baseFolder, baseNamespace);

                if (!string.IsNullOrEmpty(generator.Namespace) && !this.Includes.Contains(generator.Namespace))
                {
                    this.Includes.Add(generator.Namespace);
                }
            }
        }

        public override string GetFileName(Item item)
        {
            return "Controllers\\" + item.Name + "ApiController.cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Controllers",
                item.Name + "ApiController.cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("BaseWebApiControllerName", GarciaGeneratorConfiguration.BaseWebApiControllerName);
            //this.AddParameter("WebApiNamespaceSuffix", GarciaGeneratorConfiguration.WebApiNamespaceSuffix);
            this.AddParameter("WebApiControllerNamespaceSuffix", GarciaGeneratorConfiguration.WebApiControllerNamespaceSuffix);
            this.AddParameter("WebApiModelNamespaceSuffix", GarciaGeneratorConfiguration.WebApiModelNamespaceSuffix);
            this.AddParameter("GenerateWebApiModel", GarciaGeneratorConfiguration.GenerateWebApiModel);
            this.AddParameter("GenerateWebApiGetOne", GarciaGeneratorConfiguration.GenerateWebApiGetOne);
            this.AddParameter("GenerateWebApiGetAll", GarciaGeneratorConfiguration.GenerateWebApiGetAll);
            this.AddParameter("GenerateWebApiPost", GarciaGeneratorConfiguration.GenerateWebApiPost);
            this.AddParameter("GenerateWebApiDelete", GarciaGeneratorConfiguration.GenerateWebApiDelete);
        }
    }
}
