using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MannFramework.CodeGenerator.Template;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public class EntityGenerator : Generator<EntityTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.Entity; } }

        public EntityGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
            this.generateEnumCode = true;
        }

        public override string GetFileName(Item item)
        {
            return "Entity\\" + item.Name + ".cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Entity",
                item.Name + ".cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("BaseClass", GarciaGeneratorConfiguration.BaseEntityName);
            this.AddParameter("LazyLoad", GarciaGeneratorConfiguration.LazyLoad);
            this.AddParameter("OrmType", GarciaGeneratorConfiguration.OrmType);
        }
    }
}
