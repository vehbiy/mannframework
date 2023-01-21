using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class EntityManagerBase<K, L> : ProviderBase<K, L>
        where K : ProviderBase
        where L : struct
    {
        internal EntityManagerBase()
        {
        }

        public static IEnumerable<PropertyInfo> GetPropertiesForMvcDetails(Type type)
        {
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => !x.HasAttribute<NotSelectedAttribute>() && !x.HasAttribute<MvcIgnoreAttribute>() && !x.PropertyType.IsCollection() && x.Name != "Id");
            return properties.ToList();
        }

        public static IEnumerable<PropertyInfo> GetPropertiesForMvcIndex(Type type)
        {
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !x.HasAttribute<NotSelectedAttribute>() && !x.HasAttribute<MvcIgnoreAttribute>() && !x.HasAttribute<MvcListIgnoreAttribute>());
            return properties;
        }

        public List<T> GetItemsFromDBJoin<T>(Dictionary<string, object> parameters = null)
            where T : Entity<L>
        {
            List<T> items = new List<T>();
            Type type = typeof(T);
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => !x.HasAttribute<NotSelectedAttribute>() && x.Name != "Id");
            List<JoinClause> joins = new List<JoinClause>();

            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType.IsGenericType ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;

                if (!propertyType.IsSubclassOf(typeof(Entity)) || this.CachingEnabledForType(propertyType))
                {
                    continue;
                }

                JoinClause join = new JoinClause()
                {
                    TableName = propertyType.Name,
                    EntityType = propertyType,
                    IncludeColumns = true
                };

                if (property.PropertyType.IsGenericType)
                {
                    join.LeftColumn = "Id";
                    join.RightColumn = type.Name + "Id";
                }
                else
                {
                    join.LeftColumn = property.PropertyType.Name + "Id";
                    join.RightColumn = "Id";
                }

                joins.Add(join);
            }

            string script = this.GetSelectScript(parameters, SelectType.Select, typeof(T), null, joins, true);
            DatabaseResponse response = this.DatabaseConnection.ExecuteItems(script, parameters);
            var groups = response.GroupBy(x => x[type.Name + ".Id"]);

            foreach (var group in groups)
            {
                DatabaseResponse temp = new DatabaseResponse(group);
                Entity<L> item = null;
                //ProviderBase<K, L> provider = (ProviderBase<K, L>)ProviderRepository.Providers[typeof(T)];
                ProviderBase provider = ProviderRepository.GetProvider(typeof(T));

                if (provider != null)
                {
                    item = (Entity<L>)provider.CreateAndInitialize(temp[0], type, true);
                }
                else
                {
                    item = this.CreateAndInitializeEntity(temp[0], type, true);
                }

                foreach (PropertyInfo property in properties)
                {
                    Type propertyType = property.PropertyType.IsGenericType ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;

                    if (!propertyType.IsSubclassOf(typeof(Entity)) || this.CachingEnabledForType(propertyType))
                    {
                        continue;
                    }

                    if (property.PropertyType.IsCollection())
                    {
                        //List<Entity<L>> subItems = this.CreateEntities(temp, propertyType, true);
                        ProviderBase propertyProvider = ProviderRepository.GetProvider(propertyType);
                        List<Entity<L>> subItems = null;

                        if (propertyProvider != null)
                        {
                            subItems = propertyProvider.Create<L>(temp, propertyType, true);
                        }
                        else
                        {
                            subItems = this.CreateEntities(temp, propertyType, true);
                        }

                        var copy = this.CopyList(subItems, propertyType);
                        property.SetValue(item, copy);
                    }
                    else
                    {
                        Entity<L> subItem = this.CreateAndInitializeEntity(temp[0], propertyType, true);

                        if (subItem != null)
                        {
                            property.SetValue(item, subItem);
                        }
                    }
                }

                items.Add(item as T);
            }

            return items;
        }
    }

    /// <summary>
    /// For internal inheritance of EntityManager, do not use as base class.
    /// </summary>
    /// <typeparam name="K">Provider</typeparam>
    public class EntityManagerBase<K> : EntityManagerBase<K, int>
        where K : ProviderBase
    {
        protected internal EntityManagerBase()
        {
        }
    }

    /// <summary>
    /// Used for entity operations
    /// </summary>
    public sealed class EntityManager : EntityManagerBase<EntityManager>
    {
        internal EntityManager()
        {
        }
    }

    /// <summary>
    /// Used for entity operations
    /// </summary>
    public sealed class EntityManager<L> : EntityManagerBase<EntityManager<L>, L>
        where L : struct
    {
        internal EntityManager()
        {
        }
    }

    #region Commented
    //public class JoinEntityManager<L> : EntityManager<L>
    //    where L : struct
    //{
    //    public List<T> GetItemsFromDBJoin<T>(Dictionary<string, object> parameters = null)
    //        where T : Entity<L>
    //    {
    //        return this.GetItemsFromDBJoin<T, JoinEntityManager>(null, parameters);
    //    }

    //    public List<T> GetItemsFromDBJoin<T, K>(ProviderBase<K, L> provider, Dictionary<string, object> parameters = null)
    //    where K : ProviderBase
    //    where T : Entity<L>
    //    {
    //        List<T> items = new List<T>();
    //        Type type = typeof(T);
    //        //List<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => !x.HasAttribute<NotSelectedAttribute>() && !x.PropertyType.IsCollection() && x.Name != "Id" && x.PropertyType.IsSubclassOf(typeof(Entity))).ToList();
    //        //IEnumerable<PropertyInfo> properties2 = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => !x.HasAttribute<NotSelectedAttribute>() && x.PropertyType.IsCollection() && x.Name != "Id" && x.PropertyType.GetGenericArguments()[0].IsSubclassOf(typeof(Entity)));
    //        //properties.AddRange(properties2);

    //        IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => !x.HasAttribute<NotSelectedAttribute>() && x.Name != "Id");

    //        List<JoinClause> joins = new List<JoinClause>();

    //        foreach (PropertyInfo property in properties)
    //        {
    //            Type propertyType = property.PropertyType.IsGenericType ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;

    //            if (!propertyType.IsSubclassOf(typeof(Entity)))
    //            {
    //                continue;
    //            }

    //            JoinClause join = new JoinClause()
    //            {
    //                TableName = propertyType.Name,
    //                EntityType = propertyType,
    //                IncludeColumns = true
    //            };

    //            if (property.PropertyType.IsGenericType)
    //            {
    //                join.LeftColumn = "Id";
    //                join.RightColumn = type.Name + "Id";
    //            }
    //            else
    //            {
    //                join.LeftColumn = property.PropertyType.Name + "Id";
    //                join.RightColumn = "Id";
    //            }

    //            joins.Add(join);
    //        }

    //        string script = this.GetSelectScript(parameters, SelectType.Select, typeof(T), null, joins, true);
    //        DatabaseResponse response = this.DatabaseConnection.ExecuteItems(script, null);
    //        var groups = response.GroupBy(x => x[type.Name + ".Id"]);

    //        foreach (var group in groups)
    //        {
    //            DatabaseResponse temp = new DatabaseResponse(group);
    //            Entity<L> item = null;

    //            if (provider != null)
    //            {
    //                item = provider.CreateAndInitializeEntity(temp[0], type, true);
    //            }
    //            else
    //            {
    //                item = this.CreateAndInitializeEntity(temp[0], type, true);
    //            }

    //            foreach (PropertyInfo property in properties)
    //            {
    //                Type propertyType = property.PropertyType.IsGenericType ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;

    //                if (!propertyType.IsSubclassOf(typeof(Entity)))
    //                {
    //                    continue;
    //                }

    //                if (property.PropertyType.IsCollection())
    //                {
    //                    //List<Entity<L>> subItems = this.CreateEntities(temp, propertyType, true);
    //                    List<Entity<L>> subItems = null;

    //                    if (provider != null)
    //                    {
    //                        subItems = provider.CreateEntities(temp, propertyType, true);
    //                    }
    //                    else
    //                    {
    //                        subItems = this.CreateEntities(temp, propertyType, true);
    //                    }

    //                    var copy = this.CopyList(subItems, propertyType);
    //                    property.SetValue(item, copy);
    //                }
    //                else
    //                {
    //                    Entity<L> subItem = this.CreateAndInitializeEntity(temp[0], propertyType, true);

    //                    if (subItem != null)
    //                    {
    //                        property.SetValue(item, subItem);
    //                    }
    //                }
    //            }

    //            items.Add(item as T);
    //        }

    //        return items;
    //    }
    //}

    //public class JoinEntityManager : JoinEntityManager<int>
    //{
    //} 
    #endregion
}
