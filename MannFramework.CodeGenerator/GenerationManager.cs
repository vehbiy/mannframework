using MannFramework.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class GenerationManager
    {
        private List<Generator> generators = new List<Generator>();
        private List<Item> generatedItems = new List<Item>();
        public Project Project { get; protected set; }
        public ProjectMapping ProjectMapping { get; set; }

        public GenerationManager(Project project, ProjectMapping projectMapping)
        {
            this.Project = project;
            this.ProjectMapping = projectMapping;

            if (this.Project != null)
            {
                List<string> blProjects = this.Project.SubProjects.Where(x => x.ProjectType.HasFlag(ProjectType.BL)).Select(x => x.Name).ToList();

                foreach (SubProject subProject in this.Project.SubProjects)
                {
                    List<Generator> subProjectGenerators = ProjectTypeGenerator.Instance.GetGenerators(this.Project, subProject.ProjectType, subProject.Folder, subProject.Name, blProjects);
                    this.generators.AddRange(subProjectGenerators.Where(x => this.Project.GeneratorTypes.HasFlag(x.GeneratorType)).ToList());
                }
            }
        }

        private T CreateGenerator<T>()
            where T : Generator
        {
            T generator = Activator.CreateInstance<T>();
            return generator;
        }

        //public GenerationResult Generate<T>(Item item)
        //    where T : Generator
        //{
        //    T generator = CreateGenerator<T>();
        //    GenerationResult result = Generate(item, generator);
        //    return result;
        //}

        private GenerationResult Generate(Item item, Generator generator)
        {
            string code = generator.Generate(item);

            if (string.IsNullOrEmpty(code))
            {
                return null; // TODO
            }

            string baseFolder = this.ProjectMapping == null ? item.Project.Name : this.ProjectMapping.LocalPath;
            List<string> folders = generator.GetFolders(item, baseFolder);
            return new GenerationResult(code, item, generator, folders);
        }

        public List<GenerationResult> Generate(Item item, bool isFirstLevel = true)
        {
            List<GenerationResult> results = new List<GenerationResult>();

            if (generatedItems.Contains(item))
            {
                results.Add(new GenerationResult(null, item.Name + " already exists", OperationResultType.Warning, item, null, null));
                //return new List<GenerationResult>();
            }

            foreach (Generator generator in generators)
            {
                if (isFirstLevel || generator.GenerateInnerItems)
                {
                    GenerationResult result = Generate(item, generator);

                    if (result != null)
                    {
                        results.Add(result);
                    }
                }
            }

            generatedItems.Add(item);

            #region Ara tablolar
            List<ItemProperty> properties = item.Properties.Where(x => x.MappingType == ItemPropertyMappingType.List && x.InnerType != null && x.AssociationType == AssociationType.Aggregation).ToList();

            foreach (ItemProperty property in properties)
            {
                Item temp = new Item()
                {
                    Name = item.Name + property.InnerType.Name,
                    Project = item.Project,
                    IsActive = true
                };

                //if (!generatedItems.Contains(temp))
                {
                    temp.Properties.Add(new ItemProperty()
                    {
                        Name = item.Name,
                        Type = ItemPropertyType.Class,
                        InnerType = item
                    });

                    temp.Properties.Add(new ItemProperty()
                    {
                        Name = property.InnerType.Name,
                        Type = ItemPropertyType.Class,
                        InnerType = property.InnerType
                    });

                    List<GenerationResult> tempResults = Generate(temp, false);
                    results.AddRange(tempResults);
                    generatedItems.Add(temp);
                }
            }
            #endregion

            #region inner items
            //List<Item> innerItems = item.Properties.Where(x => x.InnerType != null).Select(x => x.InnerType).ToList();

            //if (innerItems.Count != 0)
            //{
            //    foreach (Item innerItem in innerItems)
            //    {
            //        if (innerItem.Project == null)
            //        {
            //            innerItem.Project = item.Project;
            //        }
            //    }

            //    List<GenerationResult> innerItemResults = BulkGenerate(innerItems, false);
            //    results.AddRange(innerItemResults);
            //}

            List<ItemProperty> innerProperties = item.Properties.Where(x => x.InnerType != null).ToList();

            if (innerProperties.Count != 0)
            {
                List<ItemProperty> tempProperties = new List<ItemProperty>();

                foreach (ItemProperty innerProperty in innerProperties)
                {
                    if (innerProperty.MappingType == ItemPropertyMappingType.List && innerProperty.Type == ItemPropertyType.Class)
                    {
                        string propertyName = item.Name + "Id";

                        ItemProperty newProperty = new ItemProperty()
                        {
                            Name = propertyName,
                            MappingType = ItemPropertyMappingType.Property,
                            Type = ItemPropertyType.Integer,
                            IsNullable = true,
                            Item = innerProperty.InnerType,
                            //AccessorType = AccessorType.Internal, // internal ui'dan erisimi bozuyor
                            AccessorType = AccessorType.Public,
                            FieldName = "_" + propertyName.ToPascalCase(),
                            MvcIgnore = true,
                            NotSelected = true,
                            NotSaved = true
                        };

                        //if (!innerProperties.Contains(newProperty))
                        //{
                        //    tempProperties.Add(newProperty);
                        //}

                        if (!innerProperty.InnerType.Properties.Contains(newProperty))
                        {
                            innerProperty.InnerType.Properties.Add(newProperty);
                        }
                    }

                    if (innerProperty.InnerType.Project == null)
                    {
                        innerProperty.InnerType.Project = item.Project;
                    }
                }

                innerProperties.AddRange(tempProperties);
                // Listeler olusunca ekran kalabalik oluyor, kaldirdim.
                //List<Item> innerItems = innerProperties.Select(x => x.InnerType).ToList();
                //List<GenerationResult> innerItemResults = BulkGenerate(innerItems, false);
                //results.AddRange(innerItemResults);
            }
            #endregion

            return results;
        }

        public List<GenerationResult> BulkGenerate(List<Item> items, bool isFirstLevel = true)
        {
            List<GenerationResult> results = new List<GenerationResult>();

            foreach (Item item in items)
            {
                if (!generatedItems.Contains(item))
                {
                    List<GenerationResult> itemResults = Generate(item, isFirstLevel);
                    results.AddRange(itemResults);
                }
                else
                {
                    // TODO: ne yapmak gerektigini bilemedim
                }
            }

            return results;
        }

        public List<GenerationResult> Generate(Type type)
        {
            Item item = CodeGeneratorHelper.GetItemFromType(type);
            List<GenerationResult> results = Generate(item);
            return results;
        }

        public List<GenerationResult> Generate(string itemJson)
        {
            //try
            {
                Item item = JsonConvert.DeserializeObject<Item>(itemJson);

                if (item != null)
                {
                    if (item.Project == null)
                    {
                        item.Project = Project;
                    }

                    List<GenerationResult> results = Generate(item);
                    return results;
                }
            }
            //catch (Exception ex)
            //{
            //}

            return new List<GenerationResult>();
        }
    }
}
