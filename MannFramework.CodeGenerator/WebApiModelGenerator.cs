using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MannFramework.CodeGenerator.Template;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public class WebApiModelGenerator : Generator<WebApiModelTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.WebApiModel; } }
        public override string Namespace { get { return this.BaseNamespace + ".Models"; } }

        public WebApiModelGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        protected override string GetInnerTypeClassName(string InnerTypeName)
        {
            return InnerTypeName + "Model";
        }

        public override string GetInnerTypeName(ItemProperty property, bool useCollections = true)
        {
            return base.GetInnerTypeName(property, useCollections);
        }

        public override string GetFileName(Item item)
        {
            return "Models\\" + item.Name + "ApiModel.cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Models",
                item.Name + "ApiModel.cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("BaseClass", GarciaGeneratorConfiguration.BaseWebApiModelName);
            //this.AddParameter("WebApiNamespaceSuffix", GarciaGeneratorConfiguration.WebApiNamespaceSuffix);
            this.AddParameter("WebApiModelNamespaceSuffix", GarciaGeneratorConfiguration.WebApiModelNamespaceSuffix);
        }
    }

    #region Commented
    //public class WebApiModelGeneratorOld : Generator
    //{
    //    protected override string InnerGenerate(Item Item)
    //    {
    //        string template = Helpers.ReadFromFile("WebApiModelTemplate.cs");
    //        template = template.ReplaceWithPrefix("entity", Item.Name.RemoveWhiteSpaces());
    //        template = template.ReplaceWithPrefix("namespace", Item.ProjectName.RemoveWhiteSpaces());
    //        string propertyTemplate = "public #type# #property# { get; set; }\n\t\t";
    //        GarciaStringBuilder propertyBuilder = new GarciaStringBuilder();
    //        string collectionInitialization = "";

    //        foreach (ItemProperty property in Item.Properties)
    //        {
    //            string innerType = this.GetInnerTypeName(property);
    //            string propertyText = propertyTemplate.ReplaceWithPrefix("property", property.Name);
    //            propertyText = propertyText.ReplaceWithPrefix("type", innerType);
    //            propertyBuilder += propertyText;

    //            switch (property.MappingType)
    //            {
    //                case ItemPropertyMappingTypesEnum.Property:
    //                    break;
    //                case ItemPropertyMappingTypesEnum.List:
    //                    collectionInitialization += "\n\t\t\tthis." + property.Name + " = new " + innerType + "();";
    //                    break;
    //                case ItemPropertyMappingTypesEnum.Array:
    //                    // TODO
    //                    break;
    //            }
    //        }

    //        template = template.ReplaceWithPrefix("properties", propertyBuilder.ToString());
    //        template = template.ReplaceWithPrefix("collectioninitialization", collectionInitialization);
    //        return template;
    //    }

    //    protected override string GetInnerTypeClassName(string InnerTypeName)
    //    {
    //        return InnerTypeName + "Model";
    //    }
    //} 
    #endregion
}
