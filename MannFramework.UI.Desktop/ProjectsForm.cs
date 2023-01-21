using MannFramework.Application;
using MannFramework.CodeGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MannFramework.Macondo.Desktop
{
    public partial class ProjectsForm : BaseForm
    {
        public ProjectsForm()
        {
            UIStateManager.Member = EntityManager.Instance.GetOne<Member>("Email", "vehbiy@gmail.com");
            InitializeComponent();
        }

        private void ProjectsForm_Load(object sender, EventArgs e)
        {
            //cmbProjects.DataSource = EntityManager.Instance.GetItems<Project>();
            //cmbProjects.ValueMember = "Id";
            //cmbProjects.DisplayMember = "Name";
            var projects = UIStateManager.Member.GetProjects();
            cmbProjects.DataSource = projects.Select(x => x.Project).ToList();
            cmbProjects.ValueMember = "Id";
            cmbProjects.DisplayMember = "Name";
            var defaultProject = projects.Where(x => x.IsDefault).FirstOrDefault()?.Project;

            if (defaultProject != null)
            {
                cmbProjects.SelectedValue = defaultProject.Id;
            }
        }

        private void cmbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project project = (Project)cmbProjects.SelectedItem;
            project = EntityManager.Instance.GetItem<Project>(project.Id);
            UIStateManager.Project = project;
            ProjectSetting setting = project.GetProjectSetting();
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaGeneratorConfiguration), setting);
            List<ProjectMapping> mappings = UIStateManager.Member.ProjectMappings.Where(x => x.Project.Id == project.Id).ToList();
            cmbMappings.DataSource = mappings;
            cmbMappings.DisplayMember = "LocalPath";
            ProjectMapping defaultMapping = mappings.Where(x => x.IsDefault).FirstOrDefault();

            if (defaultMapping != null)
            {
                cmbMappings.SelectedItem = defaultMapping;
            }

            List<GeneratorType> generatorTypes = Enum.GetValues(typeof(GeneratorType)).Cast<Enum>().Where(x => project.GeneratorTypes.HasFlag(x)).Cast<GeneratorType>().ToList();
            chkGeneratorTypes.Items.Clear();

            foreach (var type in generatorTypes)
            {
                chkGeneratorTypes.Items.Add(type, false);
            }

            dgItems.AutoGenerateColumns = false;
            dgItems.DataSource = project.Items;

            bool disabled = false;

            if (project.Items.Count == 0)
            {
                lblMessage.Text = "No items found";
                disabled = true;
            }
            else if (mappings.Count == 0)
            {
                lblMessage.Text = "No mappings defined";
                disabled = true;
            }
            else if (generatorTypes.Count == 0)
            {
                lblMessage.Text = "No generators defined";
                disabled = true;
            }
            else
            {
                lblMessage.Text = "";
            }

            btnGenerateAndSave.Enabled = btnGenerateAndSaveSelected.Enabled = !disabled;
            //btnSelectAll.Enabled = btnDeselectAll.Enabled = generatorTypes.Count != 0;
            btnOpenFolder.Enabled = mappings.Count != 0;
            tvProperties.Nodes.Clear();

            foreach (Item item in project.Items)
            {
                TreeNode itemNode = new TreeNode(item.Name);

                foreach (ItemProperty property in item.Properties)
                {
                    string type = "";

                    if (property.MappingType == ItemPropertyMappingType.List)
                    {
                        type = "List<";
                    }

                    if (property.InnerType != null)
                    {
                        type += property.InnerType.Name;
                    }
                    else
                    {
                        type += property.Type.ToString();
                    }

                    if (property.MappingType == ItemPropertyMappingType.List)
                    {
                        type += ">";
                    }
                    else if (property.MappingType == ItemPropertyMappingType.Array)
                    {
                        type += "[]";
                    }

                    itemNode.Nodes.Add(property.Name + " " + type);
                }

                tvProperties.Nodes.Add(itemNode);
            }

            tvProperties.ExpandAll();
        }

        private void btnGenerateAndSave_Click(object sender, EventArgs e)
        {
            Project project = (Project)cmbProjects.SelectedItem;
            this.GenerateAndSave(project.Items);
        }

        private void btnGenerateAndSaveSelected_Click(object sender, EventArgs e)
        {
            List<Item> items = dgItems.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as Item).ToList();
            this.GenerateAndSave(items);
        }

        private void btnRefreshProjects_Click(object sender, EventArgs e)
        {
            this.GetProjects();
        }

        private void GetProjects()
        {
            int index = cmbProjects.SelectedIndex;
            //var projects = EntityManager.Instance.GetItems<Project>();
            var projects = UIStateManager.Member.GetProjects().Select(x => x.Project).ToList();
            cmbProjects.DataSource = projects;
            cmbProjects.ValueMember = "Id";
            cmbProjects.DisplayMember = "Name";
            cmbProjects.SelectedIndex = index;
        }

        private void GenerateAndSave(List<Item> items)
        {
            //try
            {
                #region Generate and save
                Project project = (Project)cmbProjects.SelectedItem;
                ProjectMapping mapping = (ProjectMapping)cmbMappings.SelectedItem;
                GenerationManager manager = new GenerationManager(project, mapping);
                List<GenerationResult> results = manager.BulkGenerate(items);

                foreach (GenerationResult result in results.Where(x => chkGeneratorTypes.CheckedItems.Contains(x.Generator.GeneratorType)))
                {
                    if (result.Success)
                    {
                        string path = mapping.MappingType == ProjectMappingType.Flat ? mapping.LocalPath + "\\" + result.Folders.Last() : result.Folders.Aggregate((x, y) => (x + "\\" + y));
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllText(path, result.Code);
                        lblMessage.Text = "Code generated";
                    }
                    else
                    {
                        lblMessage.Text = "Code could not be generated (" + result.Message + ")";
                    }
                }

                if (results.Count == 0)
                {
                    lblMessage.Text = "Code could not be generated";
                }
                #endregion

                if (mapping.MappingType == ProjectMappingType.Hierarchical)
                {
                    var groups = results.Where(x => chkGeneratorTypes.CheckedItems.Contains(x.Generator.GeneratorType)).GroupBy(x => x.Folders[0] + "\\" + x.Folders[1]);

                    foreach (var group in groups)
                    {
                        XmlDocument xmldoc = new XmlDocument();
                        DirectoryInfo directory = new DirectoryInfo(group.Key);
                        FileInfo[] files = directory.GetFiles("*.*proj");
                        FileInfo file = null;

                        if (files.Length == 0)
                        {
                            lblMessage.Text = "No .csproj files found for " + group.Key;
                        }
                        else if (files.Length == 1)
                        {
                            file = files[0];
                        }
                        else
                        {
                            file = files.Where(x => x.Name == project.Name).FirstOrDefault();
                        }

                        if (file == null)
                        {
                            lblMessage.Text = "No .csproj files found for " + group.Key;
                            continue;
                        }

                        XmlNodeList compileNode = null;
                        XmlNodeList contentNodes = null;
                        XmlNodeList buildNodes = null;

                        if (file != null)
                        {
                            xmldoc.Load(file.FullName);
                            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
                            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");
                            compileNode = xmldoc.SelectNodes("//x:Compile", mgr);
                            contentNodes = xmldoc.SelectNodes("//x:Content", mgr);
                            buildNodes = xmldoc.SelectNodes("//x:Build", mgr);
                        }

                        foreach (GenerationResult result in group)
                        {
                            if (result.Success)
                            {
                                string path = mapping.MappingType == ProjectMappingType.Flat ? mapping.LocalPath + "\\" + result.Folders.Last() : result.Folders.Aggregate((x, y) => (x + "\\" + y));

                                if (mapping.MappingType == ProjectMappingType.Hierarchical && file != null && compileNode != null)
                                {
                                    string fileName = result.Folders.GetRange(2, result.Folders.Count - 2).Aggregate((x, y) => (x + "\\" + y));
                                    bool existing = false;
                                    XmlNode selectedNode = null;
                                    string nodeName = "";
                                    XmlNodeList allNodes = null;

                                    switch (result.Generator.ContentType)
                                    {
                                        case GeneratorContentType.CSharp:
                                            nodeName = "Compile";
                                            allNodes = compileNode;
                                            break;
                                        case GeneratorContentType.Html:
                                        case GeneratorContentType.Javascript:
                                            nodeName = "Content";
                                            allNodes = contentNodes;
                                            break;
                                        case GeneratorContentType.Sql:
                                            nodeName = "Build";
                                            allNodes = buildNodes;
                                            break;
                                    }

                                    foreach (XmlNode item in allNodes)
                                    {
                                        string temp = item.Attributes?["Include"].Value;

                                        if (temp == fileName)
                                        {
                                            existing = true;
                                        }
                                        else
                                        {
                                            selectedNode = item;
                                        }

                                        if (existing && selectedNode != null)
                                        {
                                            break;
                                        }
                                    }

                                    if (!existing && selectedNode != null)
                                    {
                                        XmlNode newFile = xmldoc.CreateElement(nodeName, selectedNode.NamespaceURI);
                                        XmlAttribute include = xmldoc.CreateAttribute("Include");
                                        include.Value = fileName;
                                        newFile.Attributes.Append(include);
                                        selectedNode.ParentNode.AppendChild(newFile);
                                    }
                                }
                            }
                        }

                        string xml = xmldoc.OuterXml;
                        xmldoc.Save(file.FullName);
                    }

                    SourceControlType sourceControlType = project.GetProjectSetting().SourceControlType;
                    switch (sourceControlType)
                    {
                        case SourceControlType.Tfs:
                            System.Diagnostics.Process.Start("Addtotfs.bat", mapping.LocalPath);
                            break;
                        case SourceControlType.Git:
                            System.Diagnostics.Process.Start("Addtogit.bat", mapping.LocalPath);
                            break;
                        default:
                            break;
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (cmbMappings.SelectedValue != null)
            {
                ProjectMapping mapping = cmbMappings.SelectedValue as ProjectMapping;
                System.Diagnostics.Process.Start("explorer.exe", mapping.LocalPath);
            }
        }
    }
}
