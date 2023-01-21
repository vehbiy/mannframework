using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class Item : ISearchable
    {
        public string ProjectName
        {
            get
            {
                return this.Project?.Name;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Item)
            {
                return ((Item)obj).Name.Equals(this.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public OperationResult Save()
        {
            foreach (ItemProperty property in this.Properties)
            {
                if (property.InnerType != null && property.InnerType.Id == 0)
                {
                    property.InnerType.Project = this.Project;
                    property.InnerType.Save();
                }
            }

            OperationResult result = EntityManager.Instance.Save(this);
            return result;
        }

        public bool HasProperty(string propertyName)
        {
            return this.Properties.Count(x => x.Name.Equals(propertyName, StringComparison.InvariantCulture)) != 0;
        }

        public OperationResult<Item> Copy(Project oldProject, Project newProject, string name)
        {
            OperationResult<Item> returnResult = new OperationResult<Item>();

            if (!this.Project.Equals(oldProject))
            {
                returnResult.AddValidationResult("ItemDoesNotExistInProject");
                return returnResult;
            }

            if (!this.Project.Equals(oldProject))
            {
                returnResult.AddValidationResult("InvalidProject");
                return returnResult;
            }

            List<ItemProperty> properties = this.Properties;
            this.Name = name;
            this.Project = newProject;
            this.Id = 0;
            this.Properties = properties;
            newProject.Items.Add(this);
            OperationResult result = EntityManager.Instance.Save(this);

            if (result.Success)
            {
                foreach (ItemProperty property in this.Properties)
                {
                    property.Id = 0;
                    property.Item = this;
                    EntityManager.Instance.Save(property);
                }
            }

            returnResult = new OperationResult<Item>(result, this);
            return returnResult;
        }

        protected override ValidationResults ValidateBeforeSave()
        {
            string sql = "select count(*) from Item (nolock) where (@Id=0 or Id<>@Id) and Name=@Name and ProjectId=@ProjectId and DeleteTime is null";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", this.Id);
            parameters.Add("Name", this.Name);
            parameters.Add("ProjectId", this.ProjectId);
            object countResult = EntityManager.Instance.DatabaseConnection.ExecuteScalar(sql, parameters);
            int count = Helpers.GetValueFromObject<int>(countResult);
            ValidationResults results = new ValidationResults();

            if (count != 0)
            {
                results.Add(new ValidationResult("ItemWithNameExists", "Name"));
            }

            return results;
        }

        #region ISearchable
        public string Title { get { return this.Name; } }
        public string Description { get { return this.Name; } }
        public string Icon { get { return ""; } }
        #endregion
    }
}
