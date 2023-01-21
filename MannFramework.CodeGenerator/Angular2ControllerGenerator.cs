using MannFramework.Application;
using MannFramework.CodeGenerator.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class Angular2ControllerGenerator : Generator<Angular2ControllerTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.Javascript; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.Angular2Controller; } }

        public Angular2ControllerGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        public override string GetFileName(Item item)
        {
            return "Scripts\\angular\\Controller\\" + item.Name + ".Ctrl.js";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Scripts",
                "angular",
                "Controller",
                item.Name + ".Ctrl.js"
            };
        }

        internal override void InitializeParameters()
        {
        }
    }
}
