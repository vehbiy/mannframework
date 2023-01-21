using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MannFramework.CodeGenerator.Template;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public class ProviderGenerator : Generator<ProviderTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.CSharp; } }
        public override GeneratorType GeneratorType { get { return GeneratorType.Provider; } }

        public ProviderGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        public override string GetFileName(Item item)
        {
            return "Provider\\" + item.Name + "Provider.cs";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                "Provider",
                item.Name + "Provider.cs"
            };
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("EnableCaching", GarciaGeneratorConfiguration.EnableCaching);
            this.AddParameter("GenerateCreateEntity", GarciaGeneratorConfiguration.GenerateCreateEntity);
            this.AddParameter("GenerateGetCommonParameters", GarciaGeneratorConfiguration.GenerateGetCommonParameters);
            this.AddParameter("GenerateInitializeEntity", GarciaGeneratorConfiguration.GenerateInitializeEntity);
            this.AddParameter("BaseClass", GarciaGeneratorConfiguration.BaseProviderName);
            this.AddParameter("UseAlias", GarciaGeneratorConfiguration.UseAlias);
        }
    }

    #region Commented
    //public class ProviderGeneratorOld : Generator
    //{
    //    private string initializePrimitiveTypeTemplate = "Values.GetValue<#type#>(\"#name#\")";
    //    private string initializeEntityTemplate = "EntityManager.Instance.GetItem<#innerentity#>(\"#innerentityid#\")";
    //    private string initializeEntityCollectionTemplate = "EntityManager.Instance.GetItems<#innerentity#>(\"#entity#Id\", Entity.Id)";
    //    string foreachTemplate = "\n\n\t\t\tforeach (#innertypename# innerEntity in #propertyname#)\n\t\t\t{\n\t\t\t\tEntityManager.Instance.Save(innerEntity);\n\t\t\t}\n";

    //    protected override string InnerGenerate(Item Item)
    //    {
    //        string template = Helpers.ReadFromFile("ProviderTemplate.cs");
    //        template = template.ReplaceWithPrefix("entity", Item.Name.RemoveWhiteSpaces());
    //        template = template.ReplaceWithPrefix("namespace", Item.ProjectName.RemoveWhiteSpaces() + ".BL");

    //        GarciaStringBuilder propertyBuilder = new GarciaStringBuilder();
    //        GarciaStringBuilder parameterBuilder = new GarciaStringBuilder();
    //        int index = 0;

    //        foreach (ItemProperty property in Item.Properties)
    //        {
    //            propertyBuilder += "\n\t\t\tEntity.";
    //            propertyBuilder += property.Name;
    //            propertyBuilder += " = ";
    //            propertyBuilder += this.GetInitializeCode(Item, property);
    //            propertyBuilder += ";";

    //            //parameterBuilder += "\n\t\t\tparameters.Add(\"";
    //            //parameterBuilder += property.Name;
    //            //parameterBuilder += "\", ";
    //            parameterBuilder += this.GetCommonParameterCode(Item, property);
    //            //parameterBuilder += ";";
    //            index++;
    //        }

    //        template = template.ReplaceWithPrefix("initialization", propertyBuilder.ToString());
    //        template = template.ReplaceWithPrefix("parameters", parameterBuilder.ToString());
    //        return template;
    //    }

    //    protected string GetInitializeCode(Item Item, ItemProperty Property)
    //    {
    //        string text = "";

    //        switch (Property.MappingType)
    //        {
    //            case ItemPropertyMappingTypesEnum.Property:
    //                if (Property.Type == ItemPropertyTypesEnum.Class)
    //                {
    //                    text = this.initializeEntityTemplate.ReplaceWithPrefix("innerentity", Property.Name);
    //                    text = text.ReplaceWithPrefix("innerentityid", Property.Name + "Id");
    //                }
    //                else
    //                {
    //                    text = this.initializePrimitiveTypeTemplate.ReplaceWithPrefix("type", this.GetInnerTypeName(Property));
    //                    text = text.ReplaceWithPrefix("name", Property.Name);
    //                }
    //                break;
    //            case ItemPropertyMappingTypesEnum.List:
    //                text = this.initializeEntityCollectionTemplate.ReplaceWithPrefix("entity", Item.Name);
    //                text = text.ReplaceWithPrefix("innerentity", this.GetInnerTypeName(Property, false));
    //                break;
    //            case ItemPropertyMappingTypesEnum.Array:
    //                // TODO
    //                break;
    //        }

    //        return text;
    //    }

    //    protected string GetCommonParameterCode(Item Item, ItemProperty Property)
    //    {
    //        //string text = "";
    //        GarciaStringBuilder builder = new GarciaStringBuilder();
    //        switch (Property.MappingType)
    //        {
    //            case ItemPropertyMappingTypesEnum.Property:
    //                builder += "\n\t\t\tparameters.Add(\"";
    //                builder += Property.Name;
    //                builder += "\", ";

    //                if (Property.Type == ItemPropertyTypesEnum.Class)
    //                {
    //                    builder += ("Entity." + Property.Name + ".Id");
    //                }
    //                else
    //                {
    //                    builder += ("Entity." + Property.Name);
    //                }

    //                builder += ");";
    //                break;
    //            case ItemPropertyMappingTypesEnum.List:
    //                string text = this.foreachTemplate.ReplaceWithPrefix("innertypename", this.GetInnerTypeName(Property, false));
    //                text = text.ReplaceWithPrefix("propertyname", Property.Name);
    //                builder += text;
    //                break;
    //            case ItemPropertyMappingTypesEnum.Array:
    //                // TODO
    //                break;
    //        }

    //        return builder.ToString();
    //    }
    //} 
    #endregion
}
