using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class MvcEditViewGenerator : Generator<MvcEditViewTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.Html; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.MvcEditView; } }
        public bool UseAngular2 { get { return this.Project != null && this.Project.GeneratorTypes.HasFlag(GeneratorType.Angular2Controller); } }

        public MvcEditViewGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        public override string GetFileName(Item item)
        {
            return "Views\\" + item.Name + "\\Edit.cshtml";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Views",
                item.Name,
                "Edit" + ".cshtml"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("GenerateMvcModel", GarciaGeneratorConfiguration.GenerateMvcModel);
            this.AddParameter("ItemInRowCount", 2);
            this.AddParameter("UseAngular2", this.UseAngular2);
            this.AddParameter("AddSaveLinksToTop", GarciaGeneratorConfiguration.AddSaveLinksToTop);
            this.AddParameter("AddSaveLinksToBottom", GarciaGeneratorConfiguration.AddSaveLinksToBottom);
        }
    }
}
