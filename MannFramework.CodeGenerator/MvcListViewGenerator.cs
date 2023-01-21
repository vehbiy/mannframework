using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class MvcListViewGenerator : Generator<MvcListViewTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.Html; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.MvcListView; } }

        public MvcListViewGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        public override string GetFileName(Item item)
        {
            return "Views\\" + item.Name + "\\List.cshtml";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Views",
                item.Name,
                "List" + ".cshtml"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("GenerateMvcModel", GarciaGeneratorConfiguration.GenerateMvcModel);
        }
    }
}
