using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MannFramework;
using Newtonsoft.Json.Linq;
using MannFramework.CodeGenerator.Template;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public class MssqlGenerator : Generator<MssqlTemplate>
    {
        public override GeneratorContentType ContentType { get { return GeneratorContentType.Sql; } }
        //private string connectionStringName;
        public override GeneratorType GeneratorType { get { return GeneratorType.Mssql; } }

        public MssqlGenerator(string baseFolder = "", string baseNamespace = "") : base(baseFolder, baseNamespace)
        {
        }

        //public string CompareSchema(Item item)
        //{
        //    if (string.IsNullOrEmpty(this.connectionStringName) || item == null)
        //    {
        //        return string.Empty;
        //    }

        //    SqlDatabaseConnection connection = new SqlDatabaseConnection(this.connectionStringName);
        //}

        public override string GetInnerTypeName(ItemProperty Property, bool UseCollections = true)
        {
            string sqlType = "";

            switch (Property.Type)
            {
                case ItemPropertyType.Integer:
                    sqlType = "int";
                    break;
                case ItemPropertyType.Double:
                    sqlType = "float";
                    break;
                case ItemPropertyType.Float:
                    sqlType = "float";
                    break;
                case ItemPropertyType.Decimal:
                    sqlType = "numeric";
                    break;
                case ItemPropertyType.DateTime:
                    sqlType = "datetime";
                    break;
                case ItemPropertyType.TimeSpan:
                    sqlType = "int";
                    break;
                case ItemPropertyType.String:
                    if (Property.MaxLength == 0)
                    {
                        Property.MaxLength = 1;
                    }

                    sqlType = Property.IsUnicode ? "n" : "";
                    sqlType += Property.MinLength == Property.MaxLength ? "char" : "varchar";
                    sqlType += Property.MaxLength > 8000 ? "(max)" : "(" + Property.MaxLength + ")";
                    break;
                case ItemPropertyType.Char:
                    sqlType = "char(1)";
                    break;
                case ItemPropertyType.Class:
                case ItemPropertyType.Enum:
                    sqlType = "int";
                    break;
                case ItemPropertyType.Boolean:
                    sqlType = "bit";
                    break;
                default:
                    break;
            }

            if (!Property.IsNullable)
            {
                sqlType += " not null";
            }

            return sqlType;
        }

        public override string GetFileName(Item item)
        {
            return item.Name + ".sql";
        }

        protected override List<string> GetFoldersAndFile(Item item)
        {
            return new List<string>()
            {
                item.Name + ".sql"
            };
        }

        protected override string InnerGenerate(Item item)
        {
            string code = base.InnerGenerate(item);
            //List<ItemProperty> properties = item.Properties.Where(x => x.MappingType == ItemPropertyMappingType.List && x.InnerType != null && x.AssociationType == AssociationType.Aggregation).ToList();

            //foreach (ItemProperty property in properties)
            //{
            //    Item temp = new Item()
            //    {
            //        Name = item.Name + property.InnerType.Name
            //    };

            //    temp.Properties.Add(new ItemProperty()
            //    {
            //        Name = item.Name,
            //        Type = ItemPropertyType.Class,
            //        InnerType = item
            //    });

            //    temp.Properties.Add(new ItemProperty()
            //    {
            //        Name = property.InnerType.Name,
            //        Type = ItemPropertyType.Class,
            //        InnerType = property.InnerType
            //    });

            //    string tempCode = this.Generate(temp);
            //    code += tempCode;
            //}

            return code;
        }

        internal override void InitializeParameters()
        {
            this.AddParameter("GenerateForeignKeyConstraint", GarciaGeneratorConfiguration.GenerateForeignKeyConstraint);
            this.AddParameter("UseInsertTime", GarciaGeneratorConfiguration.UseInsertTime);
            this.AddParameter("UseDeleteTime", GarciaGeneratorConfiguration.UseDeleteTime);
            this.AddParameter("GenerateStoredProcedures", GarciaGeneratorConfiguration.GenerateStoredProcedures);
        }
    }

    #region Commented
    //public class DatabaseGeneratorOld : Generator
    //{
    //    public override GeneratorContentType ContentType { get { return GeneratorContentType.Sql; } }

    //    public override string GetFileName(Item item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override List<string> GetFoldersAndFile(Item item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override string InnerGenerate(Item Item)
    //    {
    //        //XmlDocument document = Helpers.ReadXmlFromFile("SqlTemplate.xml");
    //        //XmlNode node = document.SelectSingleNode("TableTemplate");

    //        string text = Helpers.ReadFromFile("SqlTemplateOld.json");
    //        JObject obj = JObject.Parse(text);
    //        string tableTemplate = obj["TableTemplate"].ToString();
    //        string insertTemplate = obj["InsertTemplate"].ToString();
    //        string updateTemplate = obj["UpdateTemplate"].ToString();
    //        string deleteTemplate = obj["DeleteTemplate"].ToString();
    //        string selectTemplate = obj["SelectTemplate"].ToString();
    //        //string tableColumnTemplate = obj["TableColumnTemplate"].ToString();
    //        //string parameterTemplate = obj["ParameterTemplate"].ToString();
    //        //string spColumnTemplate = obj["SpColumnTemplate"].ToString();

    //        tableTemplate = tableTemplate.ReplaceWithPrefix("table", Item.Name);
    //        insertTemplate = insertTemplate.ReplaceWithPrefix("table", Item.Name);
    //        updateTemplate = updateTemplate.ReplaceWithPrefix("table", Item.Name);
    //        deleteTemplate = deleteTemplate.ReplaceWithPrefix("table", Item.Name);
    //        selectTemplate = selectTemplate.ReplaceWithPrefix("table", Item.Name);

    //        GarciaStringBuilder columnsBuilder = new GarciaStringBuilder();
    //        GarciaStringBuilder parametersBuilder = new GarciaStringBuilder();
    //        GarciaStringBuilder spColumnsBuilder = new GarciaStringBuilder();
    //        GarciaStringBuilder parameterValuesBuilder = new GarciaStringBuilder();
    //        GarciaStringBuilder columnsWithValuesBuilder = new GarciaStringBuilder();
    //        int index = 0;

    //        foreach (var column in Item.Properties)
    //        {
    //            if (column.Type == ItemPropertyType.Class)
    //            {
    //                continue;
    //            }

    //            //if (!column.Name.StartsWith("["))
    //            //{
    //            //    column.Name = "[" + column.Name;
    //            //}

    //            //if (!column.Name.EndsWith("]"))
    //            //{
    //            //    column.Name = column.Name + "]";
    //            //}

    //            column.Name = column.Name.RemoveWhiteSpaces();
    //            string sqlType = this.GetSqlType(column);

    //            columnsBuilder += column.Name;
    //            columnsBuilder += " ";
    //            columnsBuilder += sqlType;

    //            spColumnsBuilder += column.Name;
    //            //spColumnsBuilder += " ";
    //            //spColumnsBuilder += sqlType;

    //            parametersBuilder += "@";
    //            parametersBuilder += column.Name;
    //            parametersBuilder += " ";
    //            parametersBuilder += sqlType;

    //            parameterValuesBuilder += "@";
    //            parameterValuesBuilder += column.Name;

    //            columnsWithValuesBuilder += column.Name;
    //            columnsWithValuesBuilder += " = @";
    //            columnsWithValuesBuilder += column.Name;

    //            if (column.IsNullable)
    //            {
    //                parametersBuilder += " = null";
    //            }

    //            if (index != Item.Properties.Count - 1)
    //            {
    //                parametersBuilder += ",\n\t";
    //                parameterValuesBuilder += ", ";
    //                columnsBuilder += ",\n\t";
    //                spColumnsBuilder += ", ";
    //                columnsWithValuesBuilder += ",\n\t\t\t";
    //                index++;
    //            }
    //        }

    //        string columns = columnsBuilder.ToString();
    //        string parameters = parametersBuilder.ToString();
    //        string spColumns = spColumnsBuilder.ToString();
    //        string parameterValues = parameterValuesBuilder.ToString();
    //        string columnsWithValues = columnsWithValuesBuilder.ToString();
    //        tableTemplate = tableTemplate.ReplaceWithPrefix("columns", columns);
    //        insertTemplate = insertTemplate.ReplaceWithPrefix("columns", spColumns);
    //        insertTemplate = insertTemplate.ReplaceWithPrefix("parameters", parameters);
    //        insertTemplate = insertTemplate.ReplaceWithPrefix("parametervalues", parameterValues);
    //        updateTemplate = updateTemplate.ReplaceWithPrefix("parameters", parameters);
    //        updateTemplate = updateTemplate.ReplaceWithPrefix("columnswithvalues", columnsWithValues);

    //        GarciaStringBuilder scriptBuilder = new GarciaStringBuilder("begin tran\ngo\n");
    //        scriptBuilder += tableTemplate;
    //        scriptBuilder += "\n";
    //        scriptBuilder += insertTemplate;
    //        scriptBuilder += "\n";
    //        scriptBuilder += updateTemplate;
    //        scriptBuilder += "\n";
    //        scriptBuilder += deleteTemplate;
    //        scriptBuilder += "\n";
    //        scriptBuilder += selectTemplate;
    //        scriptBuilder += "\ncommit tran\ngo";
    //        return scriptBuilder.ToString();
    //    }

    //    private string GetSqlType(ItemProperty Property)
    //    {
    //        string sqlType = "";

    //        switch (Property.Type)
    //        {
    //            case ItemPropertyType.Integer:
    //                sqlType = "int";
    //                break;
    //            case ItemPropertyType.Double:
    //                sqlType = "numeric";
    //                break;
    //            case ItemPropertyType.Float:
    //                sqlType = "numeric";
    //                break;
    //            case ItemPropertyType.Decimal:
    //                sqlType = "numeric";
    //                break;
    //            case ItemPropertyType.DateTime:
    //                sqlType = "datetime";
    //                break;
    //            case ItemPropertyType.TimeSpan:
    //                sqlType = "int";
    //                break;
    //            case ItemPropertyType.String:
    //                if (Property.MaxLength == 0)
    //                {
    //                    Property.MaxLength = 1;
    //                }

    //                sqlType = Property.IsUnicode ? "n" : "";
    //                sqlType += Property.MinLength == Property.MaxLength ? "char" : "varchar";
    //                sqlType += Property.MaxLength > 8000 ? "(max)" : "(" + Property.MaxLength + ")";
    //                break;
    //            case ItemPropertyType.Char:
    //                sqlType = "char(1)";
    //                break;
    //            case ItemPropertyType.Class:
    //                sqlType = "";
    //                break;
    //            case ItemPropertyType.Enum:
    //                sqlType = "int";
    //                break;
    //            default:
    //                break;
    //        }

    //        return sqlType;
    //    }
    //} s
    #endregion
}