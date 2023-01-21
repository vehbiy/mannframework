using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public static class CodeGeneratorHelper
    {
        private static Dictionary<string, Item> generatedItems = new Dictionary<string, Item>();

        public static Item GetItemFromType(Type type)
        {
            if (type == null || (!type.IsEnum && (!type.IsClass || PrimitiveType.IsPrimitive(type))))
            {
                return null;
            }

            Item item = new Item()
            {
                Name = type.Name,
                Project = new Project() { Name = type.Namespace },
                IsEnum = type.IsEnum
            };

            if (!generatedItems.ContainsKey(item.Name))
            {
                generatedItems.Add(item.Name, item);
            }

            //PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            //PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly); // internal olanlar problem cikariyor
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public); // kalitimda da calismali

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                Type propertyType = property.PropertyType;
                Item innerType = null;
                ItemPropertyMappingType mappingType = ItemPropertyMappingType.Property;
                ItemPropertyType itemPropertyType = GetPropertyTypeFromType(propertyType);
                bool notSelected = property.HasAttribute<NotSelectedAttribute>();
                bool notSaved = property.HasAttribute<NotSavedAttribute>();
                bool mvcIgnore = property.HasAttribute<MvcIgnoreAttribute>() || notSelected || notSaved;
                bool mvcListIgnore = property.HasAttribute<MvcListIgnoreAttribute>() || notSelected || notSaved;

                //if (propertyType.IsList())
                if (propertyType.IsCollection())
                {
                    mappingType = ItemPropertyMappingType.List;
                    Type genericType = propertyType.GetGenericArguments()?[0];
                    innerType = GetItemFromType(genericType);

                    if (innerType != null)
                    {
                        ItemProperty innerProperty = new ItemProperty()
                        {
                            MappingType = ItemPropertyMappingType.Property,
                            IsNullable = false,
                            Type = ItemPropertyType.Class,
                            Name = item.Name,
                            InnerType = item,
                            MvcIgnore = mvcIgnore,
                            MvcListIgnore = mvcListIgnore,
                            NotSelected = notSelected,
                            NotSaved = notSaved
                        };

                        if (!innerType.Properties.Contains(innerProperty))
                        {
                            innerType.Properties.Add(innerProperty);
                        }
                    }
                }
                else if (propertyType.IsArray)
                {
                    mappingType = ItemPropertyMappingType.Array;
                }
                else if ((propertyType.IsClass || propertyType.IsEnum) && (propertyType.IsEnum || !PrimitiveType.IsPrimitive(propertyType)))
                {
                    if (generatedItems.ContainsKey(propertyType.Name))
                    {
                        innerType = generatedItems[propertyType.Name];
                    }
                    else
                    {
                        innerType = GetItemFromType(propertyType);
                    }
                }

                ItemProperty itemProperty = new ItemProperty()
                {
                    Name = property.Name,
                    IsNullable = propertyType.IsNullable(),
                    MappingType = mappingType,
                    InnerType = innerType,
                    Type = itemPropertyType,
                    MvcIgnore = mvcIgnore,
                    MvcListIgnore = mvcListIgnore,
                    NotSelected = notSelected,
                    NotSaved = notSaved
                };

                item.Properties.Add(itemProperty);
            }

            return item;
        }

        public static ItemPropertyType GetPropertyTypeFromType(Type type)
        {
            ItemPropertyType propertyType = ItemPropertyType.Unknown;

            //if (type.IsCollection())
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()?[0];
            }

            switch (type.Name.ToLowerInvariant())
            {
                case "int16":
                case "int32":
                case "int":
                    propertyType = ItemPropertyType.Integer;
                    break;
                case "double":
                    propertyType = ItemPropertyType.Double;
                    break;
                case "float":
                    propertyType = ItemPropertyType.Float;
                    break;
                case "decimal":
                    propertyType = ItemPropertyType.Decimal;
                    break;
                case "datetime":
                    propertyType = ItemPropertyType.DateTime;
                    break;
                case "timespan":
                    propertyType = ItemPropertyType.TimeSpan;
                    break;
                case "string":
                    propertyType = ItemPropertyType.String;
                    break;
                case "char":
                    propertyType = ItemPropertyType.Char;
                    break;
                case "bool":
                case "boolean":
                    propertyType = ItemPropertyType.Boolean;
                    break;
                default:
                    propertyType = type.IsClass ? ItemPropertyType.Class : type.IsEnum ? ItemPropertyType.Enum : ItemPropertyType.Unknown;
                    break;
            }

            return propertyType;
        }
    }
}
