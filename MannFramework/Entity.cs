using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [Serializable]
    public abstract class Entity
    {
        public abstract object GetId();
        public abstract string GetPropertyValue(string propertyName);
        [NotSaved]
        [NotSelected]
        [JsonIgnore]
        [MvcIgnore]
        [MvcListIgnore]
        public virtual bool CachingEnabled { get { return false; } }
        //public static bool EnableCaching { get; set; }
    }

    [Serializable]
    public abstract class Entity<L> : Entity
        where L : struct
    {
        [NotSaved]
        [NotUpdated]
        public virtual L Id { get; set; }
        [NotSaved]
        [NotSelected]
        [JsonIgnore]
        public virtual bool IsMarkedForDeletion { get; set; }
        [NotUpdated]
        [JsonIgnore]
        public virtual DateTime? InsertTime { get; set; }
        [NotSaved]
        [NotSelected]
        [NotUpdated]
        [JsonIgnore]
        public virtual DateTime? DeleteTime { get; set; }
        [NotSaved]
        [NotSelected]
        [JsonIgnore]
        public virtual bool IsActive { get; set; }

        private string typeName;
        [NotSaved]
        [NotSelected]
        [NotUpdated]
        [JsonIgnore]
        [MvcIgnore]
        [MvcListIgnore]
        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.typeName))
                {
                    Type type = this.GetType();
                    TableMappingAttribute attribute = type.GetCustomAttribute<TableMappingAttribute>();

                    if (attribute != null)
                    {
                        this.typeName = attribute.TableName;
                    }
                    else
                    {
                        this.typeName = this.GetType().Name;
                    }
                }

                return this.typeName;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (!(obj is Entity<L>))
            {
                return false;
            }
            else
            {
                return this.Id.Equals(((Entity<L>)obj).Id);
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override object GetId()
        {
            return this.Id;
        }

        public override string GetPropertyValue(string propertyName)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName);
            object value = null;

            if (property != null)
            {
                value = property.GetValue(this);
            }

            return value == null ? "" : value.ToString();
        }

        protected T Get<T>(L? id, ref T item)
            where T : Entity<L>
        {
            if (item == null && id.HasValue && !id.Value.Equals(default(L)))
            {
                item = EntityManager<L>.Instance.GetItem<T>(id);
            }

            return item;
        }

        protected List<T> Get<T>(ref List<T> items)
            where T : Entity<L>
        {
            if ((items == null || items.Count == 0) && !this.Id.Equals(default(L)))
            {
                Type type = typeof(T);
                string propertyName = this.GetType().Name + "Id";
                PropertyInfo property = type.GetProperty(propertyName);

                if (property != null)
                {
                    items = EntityManager<L>.Instance.GetItems<T>(propertyName, this.Id);
                }
                // anlamadim
                //else
                //{
                //    Dictionary<string, object> parameters = new Dictionary<string, object>();
                //    parameters.Add(propertyName, this.Id);
                //    string tableName = type.Name + propertyName;
                //    string script = EntityManager<L>.Instance.GetSelectScript(parameters, SelectType.Select, property.PropertyType, joinClause: new List<JoinClause>() { new JoinClause() { TableName = tableName, LeftColumn = "Id", RightColumn = "" } });
                //    items = EntityManager<L>.Instance.GetItemsFromDB(script, parameters, type).ToList<T, L>();
                //}
            }

            return items;
        }

        protected void Set<T>(ref T item, T value)
        {
            item = value;
        }

        protected void Set<T>(ref T item, ref L? id, T value)
            where T : Entity
        {
            item = value;

            if (item != null && id.HasValue && !id.Value.Equals(item.GetId()))
            {
                id = (L?)item.GetId();
            }

            // Vehbi: burasi onemli olabilir, hataya yol acmadigindan emin olmali
            //this.Id = id.HasValue ? id.Value : default(L);

            //if (id.HasValue)
            //{
            //    this.Id = id.Value;
            //}
            //else if (value != null && value is Entity<L>)
            //{
            //    this.Id = (value as Entity<L>).Id;
            //}
            //else
            //{
            //    this.Id = default(L);
            //}
        }

        protected internal virtual void AfterSave()
        {
        }

        protected internal virtual void AfterDelete()
        {
        }

        protected internal virtual void AfterUpdate()
        {
        }

        protected internal virtual void AfterInsert()
        {
        }

        protected internal virtual ValidationResults ValidateBeforeSave()
        {
            return null;
        }

        protected internal virtual ValidationResults ValidateBeforeDelete()
        {
            return null;
        }

        protected internal virtual ValidationResults ValidateBeforeUpdate()
        {
            return null;
        }

        protected internal virtual ValidationResults ValidateBeforeInsert()
        {
            return null;
        }
    }
}
