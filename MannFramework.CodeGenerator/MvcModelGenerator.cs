using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class MvcModelGenerator : Generator<MvcModelTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.MvcModel; } }
        public override string Namespace { get { return this.BaseNamespace + ".Models"; } }

        public MvcModelGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        protected override string GetInnerTypeClassName(string InnerTypeName)
        {
            return InnerTypeName + "Model";
        }

        public override string GetFileName(Item item)
        {
            return "Models\\" + item.Name + "Model.cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Models",
                item.Name + "Model.cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("BaseClass", GarciaGeneratorConfiguration.BaseMvcModelName);
        }
    }
}
