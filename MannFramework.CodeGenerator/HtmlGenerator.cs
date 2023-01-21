using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class HtmlGenerator : Generator<HtmlTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.Html; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.Html; } }

        public HtmlGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        public override string GetFileName(Item item)
        {
            return item.Name + ".html";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Html",
                item.Name + ".html"
            };
        }

        internal override void InitializeParameters()
        {
        }
    }
}
