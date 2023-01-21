using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    /// <summary>
    /// For internal inheritance, do not use as base class.
    /// </summary>
    public abstract class ProviderBase : SingletonBase
    {
        public abstract OperationResult Save(Entity t);
        public abstract OperationResult Save(Entity t, Guid transactionId);
        public abstract void ClearCache(string name);
        public abstract object GetItem(object Id);
        public abstract Entity[] GetAllItems();
        public abstract void Initialize(object t, Dictionary<string, object> dr, bool useAlias);
        protected internal abstract Entity CreateAndInitialize(Dictionary<string, object> dictionaryItem, Type entityType, bool useAlias);
        protected internal abstract List<Entity<L>> Create<L>(DatabaseResponse dataTableObject, Type entityType, bool useAlias) where L : struct;
        internal ProviderBase()
        {
        }
    }

    /// <summary>
    /// For internal inheritance, do not use as base class.
    /// </summary>
    /// <typeparam name="K">Provider</typeparam>
    /// <typeparam name="L">Id type</typeparam>
    public abstract class ProviderBase<K, L> : ProviderBase
          where K : ProviderBase
          where L : struct
    {
        protected string idName = "Id";
        //protected string EntityTypeName { get; set; }
        protected virtual string InsertSp { get; set; }
        protected virtual string UpdateSp { get; set; }
        protected virtual string DeleteSp { get; set; }
        protected virtual string SelectSp { get; set; }
        protected virtual string SearchSp { get; set; }
        protected virtual string CountSp { get; set; }
        public virtual IDatabaseConnection DatabaseConnection { get; set; }
        //protected Cache Cache { get; set; }
        protected virtual bool GetActiveItemsOnly { get; set; }
        //protected virtual bool EnableCaching { get; set; }
        protected virtual bool UseDeleteTime { get; set; }
        protected IParameterUpdater ParameterUpdater { get; set; }
        protected bool UpdateCacheAfterSave { get; set; }
        public DatabaseConnectionType DatabaseConnectionType { get; set; }

        /// <summary>
        /// Calculated using EnableCaching and GarciaORMConfiguration.DisableCaching properties
        /// </summary>
        protected virtual bool CachingEnabled
        {
            get
            {
                //return this.EnableCaching && !GarciaConfiguration.DisableCaching && this.Cache != null;
                return !GarciaConfiguration.DisableCaching && CacheProvider.Cache != null;
            }
        }

        protected virtual bool CachingEnabledForEntity<T>(T item)
            where T : Entity
        {
            return this.CachingEnabled && item.CachingEnabled;
        }

        protected virtual bool CachingEnabledForType(Type type)
        {
            if (type.IsAbstract)
            {
                return false;
            }

            Entity item = (Entity)Activator.CreateInstance(type, true);
            //return this.CachingEnabled && type.GetPropertyValue<bool>("EnableCaching");
            return this.CachingEnabled && item.CachingEnabled;
        }

        /// <summary>
        /// Default value is Id
        /// </summary>
        protected virtual string IdPropertyName
        {
            get
            {
                return "Id";
            }
        }

        public static K Instance
        {
            get
            {
                return (K)GetInstance(typeof(K));
            }
        }

        public virtual string ConnectionStringName
        {
            get
            {
                return GarciaConfiguration.DefaultConnectionStringName;
            }
        }

        protected internal ProviderBase()
        {
            this.Initialize();
        }

        protected virtual void Initialize()
        {
            this.InitializeCache();
            this.InitializeDatabaseConnection();
            this.UseDeleteTime = GarciaConfiguration.UseDeleteTime;

            switch (this.DatabaseConnectionType)
            {
                case DatabaseConnectionType.StoredProcedure:
                    this.InsertSp = this.GetSpName("Insert");
                    this.UpdateSp = this.GetSpName("Update");
                    this.DeleteSp = this.GetSpName("Delete");
                    this.SelectSp = this.GetSpName("Select");
                    this.CountSp = this.GetSpName("Count");
                    this.SearchSp = this.GetSpName("Search");
                    break;
                case DatabaseConnectionType.DynamicSql:
                    break;
            }
        }

        protected virtual void InitializeCache()
        {
            CacheProvider.Cache = CacheFactory.CreateCache();
            this.UpdateCacheAfterSave = GarciaConfiguration.UpdateCacheAfterSave;
        }

        protected virtual void InitializeDatabaseConnection()
        {
            this.DatabaseConnectionType = GarciaConfiguration.DefaultDatabaseConnectionType;
            this.DatabaseConnection = DatabaseFactory.CreateDatabase(this.ConnectionStringName);
        }

        #region Save Methods

        protected virtual L Insert(Entity<L> item, Guid? transactionId)
        {
            if (item != null)
            {
                item.ValidateBeforeInsert();
            }

            string script = this.GetInsertScript(item);

            if (string.IsNullOrEmpty(script))
            {
                return default(L);
            }

            if (!item.InsertTime.HasValue)
            {
                item.InsertTime = DateTime.Now;
            }

            Dictionary<string, object> parameters = this.GetInsertParameters(item);
            object obj = this.DatabaseConnection.ExecuteScalar(script, parameters, transactionId);
            L returnResult = Helpers.GetValueFromObject<L>(obj);

            if (!returnResult.Equals(default(L)))
            {
                item.AfterInsert();

                if (this.CachingEnabledForEntity(item) && CacheProvider.Cache != null && this.UpdateCacheAfterSave)
                {
                    Type type = item.GetType();
                    string typeName = type.FullName;

                    // add to cache
                    lock (typeof(object))
                    {
                        if (CacheProvider.Cache[typeName] != null)
                        {
                            List<Entity<L>> items = (List<Entity<L>>)CacheProvider.Cache[typeName];
                            items.Add(item);
                        }
                        else
                        {
                            List<Entity<L>> items = new List<Entity<L>>();
                            items.Add(item);
                            CacheProvider.Cache.Add(typeName, items);
                        }
                    }
                }
            }

            return returnResult;
        }

        protected virtual bool Update(Entity<L> item, Guid? transactionId)
        {
            if (item != null)
            {
                item.ValidateBeforeUpdate();
            }

            string script = this.GetUpdateScript(item);

            if (string.IsNullOrEmpty(script))
            {
                return false;
            }

            Dictionary<string, object> parameters = this.GetUpdateParameters(item);
            int result = this.DatabaseConnection.ExecuteNonQuery(script, parameters, transactionId);

            if (result > 0)
            {
                item.AfterUpdate();

                if (this.CachingEnabledForEntity(item) && CacheProvider.Cache != null && this.UpdateCacheAfterSave)
                {
                    Type type = item.GetType();
                    string typeName = type.FullName;

                    // update in cache

                    lock (typeof(object))
                    {
                        if (CacheProvider.Cache[typeName] != null)
                        {
                            List<Entity<L>> items = (List<Entity<L>>)CacheProvider.Cache[typeName];
                            int index = -1;
                            int counter = 0;

                            foreach (var temp in items)
                            {
                                if (!temp.Id.Equals(default(L)) && temp.Id.Equals(item.Id))
                                {
                                    index = counter;
                                    break;
                                }

                                counter++;
                            }

                            if (index >= 0)
                            {
                                items[index] = item;
                            }
                        }
                        else
                        {
                            List<Entity<L>> items = new List<Entity<L>>();
                            items.Add(item);
                            CacheProvider.Cache.Add(typeName, items);
                        }
                    }
                }

                return true;
            }

            return false;
        }

        protected virtual DateTime? Delete(Entity<L> item, Guid? transactionId)
        {
            if (item != null)
            {
                item.ValidateBeforeDelete();
            }

            string script = this.GetDeleteScript(item);

            if (string.IsNullOrEmpty(script))
            {
                return null;
            }

            Dictionary<string, object> parameters = this.GetDeleteParameters(item);
            int result = this.DatabaseConnection.ExecuteNonQuery(script, parameters, transactionId);

            if (result > 0)
            {
                item.AfterDelete();

                if (this.CachingEnabledForEntity(item) && CacheProvider.Cache != null && this.UpdateCacheAfterSave)
                {
                    Type type = item.GetType();
                    string typeName = type.FullName;

                    // add to cache
                    lock (typeof(object))
                    {
                        if (CacheProvider.Cache[typeName] != null)
                        {
                            List<Entity<L>> items = (List<Entity<L>>)CacheProvider.Cache[typeName];
                            int index = -1;
                            int counter = 0;

                            foreach (var temp in items)
                            {
                                if (!temp.Id.Equals(default(L)) && temp.Id.Equals(item.Id))
                                {
                                    index = counter;
                                    break;
                                }

                                counter++;
                            }

                            if (index >= 0)
                            {
                                items.RemoveAt(index);
                            }
                        }
                    }
                }

                return DateTime.Now;
            }

            return null;
        }

        public sealed override OperationResult Save(Entity t)
        {
            return this.Save((Entity<L>)t);
        }

        public sealed override OperationResult Save(Entity t, Guid TransactionId)
        {
            return this.Save((Entity<L>)t, TransactionId);
        }

        protected virtual OperationResult Save(Entity<L> t, Guid? TransactionId)
        {
            OperationResult operationResult = new OperationResult();

            if (t != null)
            {
                // TODO: validation
                //ValidationResults validationResults = ValidationManager<L>.Validate<T>(t);
                //operationResult.ValidationResults = validationResults;

                operationResult.ValidationResults = t.ValidateBeforeSave();

                if (operationResult.ValidationResults != null && operationResult.ValidationResults.Count != 0)
                {
                    operationResult.Success = false;
                    return operationResult;
                }
            }

            else
            {
                throw new ArgumentNullException();
            }

            L result = default(L);

            if (t.Id.Equals(default(L)))
            {
                result = this.Insert(t, TransactionId);
                t.Id = result;

                if (!result.Equals(default(L)))
                {
                    operationResult.Success = true;
                }
            }

            else if (t.IsMarkedForDeletion)
            {
                DateTime? deleteTime = this.Delete(t, TransactionId);

                if (deleteTime.HasValue)
                {
                    operationResult.Success = true;
                }
            }

            else
            {
                bool updateResult = this.Update(t, TransactionId);

                if (updateResult)
                {
                    operationResult.Success = true;
                }
            }

            if (operationResult.Success)
            {
                t.AfterSave();

                if (0 == 1)
                {
                    List<Entity> propertyValues = this.GetSaveablePropertyValues(t);

                    foreach (Entity propertyValue in propertyValues)
                    {
                        //PropertyInfo parentProperty = propertyValue.GetType().GetProperty(t.GetType().Name + "Id");

                        //if (parentProperty != null)
                        //{
                        //    parentProperty.SetValue(propertyValue, t.Id);
                        //}

                        PropertyInfo parentProperty = propertyValue.GetType().GetProperty(t.GetType().Name);

                        if (parentProperty != null)
                        {
                            parentProperty.SetValue(propertyValue, t);
                        }

                        OperationResult propertyValueSaveResult = EntityManager.Instance.Save(propertyValue);

                        if (!propertyValueSaveResult.Success)
                        {
                            // TODO: log and rollback is possible
                        }
                    }
                }
            }

            return operationResult;
        }

        public virtual OperationResult Save(Entity<L> t)
        {
            OperationResult result = this.Save(t, null);
            return result;
        }

        #endregion

        #region Parameters

        //protected abstract Dictionary<string, object> GetCommonParameters(T t);

        protected virtual Dictionary<string, object> GetUpdateParameters(Entity<L> t)
        {
            Dictionary<string, object> parameters = this.GetCommonParameters(t);

            if (!parameters.ContainsKey(this.IdPropertyName))
            {
                parameters.AddParameterValue(this.IdPropertyName, t.Id);
            }

            if (this.ParameterUpdater != null)
            {
                parameters = this.ParameterUpdater.UpdateParameterValues(parameters);
            }

            return parameters;
        }

        protected virtual Dictionary<string, object> GetInsertParameters(Entity<L> t)
        {
            Dictionary<string, object> parameters = this.GetCommonParameters(t);

            if (this.ParameterUpdater != null)
            {
                parameters = this.ParameterUpdater.UpdateParameterValues(parameters);
            }

            return parameters;
        }

        protected virtual Dictionary<string, object> GetDeleteParameters(Entity<L> t)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(this.IdPropertyName, t.Id);
            return parameters;
        }

        #endregion

        #region Get Items

        protected virtual List<Entity<L>> GetItemsFromCache(Type EntityType)
        {
            if (!this.CachingEnabledForType(EntityType))
            {
                return this.GetItemsFromDB(EntityType);
            }

            string typeName = EntityType.FullName;

            if (CacheProvider.Cache[typeName] == null)
            {
                lock (typeof(object))
                {
                    if (CacheProvider.Cache[typeName] == null)
                    {
                        List<Entity<L>> items = this.GetItemsFromDB(EntityType);
                        CacheProvider.Cache.Add(typeName, items);
                    }
                }
            }

            return (List<Entity<L>>)CacheProvider.Cache[typeName];
        }

        public virtual List<Entity<L>> GetItemsFromCache(Dictionary<string, object> Parameters, Type EntityType)
        {
            List<Entity<L>> AllItems = this.GetItemsFromCache(EntityType);

            if (Parameters == null || Parameters.Count == 0)
            {
                return AllItems;
            }

            List<Entity<L>> FilteredItems = new List<Entity<L>>();

            foreach (Entity<L> item in AllItems)
            {
                bool addItem = true;
                IDictionaryEnumerator ienum = Parameters.GetEnumerator();

                while (ienum.MoveNext())
                {
                    PropertyInfo p = item.GetType().GetProperty(ienum.Key.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (p == null)
                    {
                        p = item.GetType().GetProperty(ienum.Key.ToString().Replace("Id", ""), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    }

                    if (p == null)
                    {
                        throw new InvalidOperationException();
                    }

                    object value = p.GetValue(item, null);

                    if (value == null || !value.Equals(ienum.Value))
                    {
                        addItem = false;
                        break;
                    }
                }

                if (addItem)
                {
                    FilteredItems.Add(item);
                }
            }

            return FilteredItems;
        }

        /// <summary>
        /// Selects items from Db using the select sp of the Provider2
        /// </summary>
        /// <returns></returns>
        /// 
        protected virtual List<Entity<L>> GetItemsFromDB(Type EntityType)
        {
            string script = this.GetSelectScript(null, EntityType);

            if (string.IsNullOrEmpty(script))
            {
                return new List<Entity<L>>();
            }

            List<Entity<L>> items = this.GetItemsFromDB(script, EntityType);
            return items;
        }
        /// <summary>
        /// Selects items from Db using the select sp provided in the parameter
        /// </summary>
        /// <param name="SelectScript"></param>
        /// <returns></returns>
        protected virtual List<Entity<L>> GetItemsFromDB(string SelectScript, Type EntityType)
        {
            DatabaseResponse dt = this.DatabaseConnection.ExecuteItems(SelectScript, new Dictionary<string, object>());
            List<Entity<L>> items = this.CreateEntities(dt, EntityType);
            return items;
        }

        /// <summary>
        /// Selects items from Db using the select sp of the Provider2
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        protected virtual List<Entity<L>> GetItemsFromDB(Dictionary<string, object> Parameters, Type EntityType)
        {
            string script = this.GetSelectScript(Parameters, EntityType);
            List<Entity<L>> items = this.GetItemsFromDB(script, Parameters, EntityType);
            return items;
        }

        /// <summary>
        /// Selects items from Db using the select sp provided in the parameter
        /// </summary>
        /// <param name="Script"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<Entity<L>> GetItemsFromDB(string Script, Dictionary<string, object> Parameters, Type EntityType)
        {
            //Dictionary<string, object> dbParameters = new Dictionary<string, object>(Parameters);
            //DatabaseResponse dt = this.DatabaseConnection.ExecuteItems(Script, dbParameters);
            DatabaseResponse dt = this.DatabaseConnection.ExecuteItems(Script, Parameters);
            List<Entity<L>> items = this.CreateEntities(dt, EntityType);

            if (items == null || items.Count == 0)
            {
                return new List<Entity<L>>();
            }

            return items;
        }

        /// <summary>
        /// Selects items from Db using the select sp provided in the parameter
        /// </summary>
        /// <param name="Script"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<Entity<L>> GetItemsFromDB(string Script, string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetItemsFromDB(Script, parameters, EntityType);
        }

        /// <summary>
        /// Selects item from Db using the select sp of the Provider2 and the provided Id value
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected virtual Entity<L> GetItemFromDB(L Id, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(this.IdPropertyName, Id);
            List<Entity<L>> items = this.GetItemsFromDB(parameters, EntityType);
            //DatabaseResponse dt = this.DatabaseConnection.ExecuteItems(this.SelectSp, parameters);
            //List<T> items = this.Initialize(dt);

            return items?.FirstOrDefault();
        }

        protected IList CopyList(List<Entity<L>> Items, Type InnerType)
        {
            Type repositoryType = typeof(List<>).MakeGenericType(InnerType);
            var items = Activator.CreateInstance(repositoryType) as IList;

            foreach (Entity<L> item in Items)
            {
                items.Add(item);
            }

            return items;
        }

        protected List<T> ToList<T>(List<Entity<L>> Items)
            where T : Entity<L>
        {
            //List<T> items = Items.Select(x => x as T).ToList();
            List<T> items = Items.ToList<T, L>(); // extension'a cekildi
            return items;
        }

        #region Sonra yapilacak
        //protected virtual List<T> GetItemsFromDB(int PageNumber, int ValuesCount)
        //{
        //    DatabaseResponse response = this.DatabaseConnection.ExecuteItems(this.SelectSp, new Dictionary<string, object>(), PageNumber, ValuesCount);
        //    List<T> items = this.Initialize(response);
        //    return items;
        //}

        //protected virtual List<T> GetItemsFromDB(Dictionary<string, object> Parameters, int PageNumber, int ValuesCount)
        //{
        //    List<T> items = this.GetItemsFromDB(this.SelectSp, Parameters, PageNumber, ValuesCount);
        //    return items;
        //}

        //protected virtual List<T> GetItemsFromDB(string SelectSp, Dictionary<string, object> Parameters, int PageNumber, int ValuesCount)
        //{
        //    Dictionary<string, object> dbParameters = new Dictionary<string, object>(Parameters);
        //    DatabaseResponse response = this.DatabaseConnection.ExecuteItems(SelectSp, dbParameters, PageNumber, ValuesCount);
        //    List<T> items = this.Initialize(response);
        //    return items;
        //} 
        #endregion

        protected virtual Entity<L> GetItemFromCache(L Id, Type EntityType)
        {
            if (!this.CachingEnabledForType(EntityType))
            {
                return null;
            }

            if (Id.Equals(default(L)))
            {
                return null;
            }

            List<Entity<L>> allItems = this.GetItemsFromCache(EntityType);

            if (allItems == null || allItems.Count == 0)
            {
                return null;
            }

            foreach (Entity<L> item in allItems)
            {
                if (item.Id.Equals(Id))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 14
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        public virtual Entity<L> GetItem(L Id, Type EntityType)
        {
            if (Id.Equals(default(L)))
            {
                return null;
            }

            Entity<L> t = null;

            if (this.CachingEnabledForType(EntityType))
            {
                t = this.GetItemFromCache(Id, EntityType);
            }

            if (t == null)
            {
                return this.GetItemFromDB(Id, EntityType);
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// 14
        /// </summary>
        /// <param name="Id"></param>
        //public virtual Entity<L> GetItem(L? Id)
        //{
        //    if (!Id.HasValue)
        //    {
        //        return null;
        //    }

        //    return this.GetItem(Id.Value);
        //}

        /// <summary>
        /// 14
        /// </summary>
        /// <param name="Id"></param>
        public virtual T GetItem<T>(L? Id)
            where T : Entity<L>
        {
            if (!Id.HasValue)
            {
                return null;
            }

            return this.GetItem(Id.Value, typeof(T)) as T;
        }

        /// <summary>
        /// 1
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual List<Entity<L>> GetItems(Type EntityType)
        {
            if (this.CachingEnabledForType(EntityType))
            {
                return this.GetItemsFromCache(EntityType);
            }

            return this.GetItemsFromDB(EntityType);
        }

        /// <summary>
        /// 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> GetItems<T>()
            where T : Entity<L>
        {
            List<Entity<L>> items = this.GetItems(typeof(T));
            List<T> items2 = this.ToList<T>(items);
            return items2;
        }

        /// <summary>
        /// 2
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual List<Entity<L>> GetItems(Dictionary<string, object> Parameters, Type EntityType)
        {
            if (this.CachingEnabledForType(EntityType))
            {
                return this.GetItemsFromCache(Parameters, EntityType);
            }

            return this.GetItemsFromDB(Parameters, EntityType);
        }

        /// <summary>
        /// 2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<T> GetItems<T>(Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            List<Entity<L>> items = this.GetItems(Parameters, typeof(T));
            List<T> items2 = this.ToList<T>(items);
            return items2;
        }

        /// <summary>
        /// 3
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual List<Entity<L>> GetItems(string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetItems(parameters, EntityType);
        }

        /// <summary>
        /// 3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual List<T> GetItems<T>(string FilterKey, object FilterValue)
            where T : Entity<L>
        {
            List<Entity<L>> items = this.GetItems(FilterKey, FilterValue, typeof(T));
            List<T> items2 = this.ToList<T>(items);
            return items2;
        }

        //public virtual List<T> GetItems(int PageNumber, int ValuesCount)
        //{
        //    // TODO : gokhang : Cache üzerinden de döndürülebilmeli
        //    return this.GetItemsFromDB(PageNumber, ValuesCount);
        //}

        //public virtual List<T> GetItems(Dictionary<string, object> Parameters, int PageNumber, int ValuesCount)
        //{
        //    return this.GetItemsFromDB(Parameters, PageNumber, ValuesCount);
        //}

        //public virtual List<T> GetItems(string FilterKey, object FilterValue, int PageNumber, int ValuesCount)
        //{
        //    Dictionary<string, object> Parameters = new Dictionary<string, object>();
        //    Parameters.Add(FilterKey, FilterValue);
        //    return this.GetItemsFromDB(Parameters, PageNumber, ValuesCount);
        //}

        /// <summary>
        /// 4
        /// </summary>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual int GetItemCount(Type EntityType)
        {
            return this.GetItemCount(null, EntityType);
        }

        /// <summary>
        /// 4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual int GetItemCount<T>()
            where T : Entity<L>
        {
            return this.GetItemCount(typeof(T));
        }

        /// <summary>
        /// 5
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual int GetItemCount(Dictionary<string, object> Parameters, Type EntityType)
        {
            string script = this.GetCountScript(Parameters, EntityType);
            object result = this.DatabaseConnection.ExecuteScalar(script, Parameters);
            int count = Helpers.GetValueFromObject<int>(result);
            return count;
        }

        /// <summary>
        /// 5
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual int GetItemCount<T>(Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            return this.GetItemCount(Parameters, typeof(T));
        }

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual int GetItemCount(string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetItemCount(parameters, EntityType);
        }

        /// <summary>
        /// 6
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual int GetItemCount<T>(string FilterKey, object FilterValue)
            where T : Entity<L>
        {
            return this.GetItemCount(FilterKey, FilterValue, typeof(T));
        }

        /// <summary>
        /// 7
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetOne(Dictionary<string, object> Parameters, Type EntityType)
        {
            List<Entity<L>> items = this.GetItems(Parameters, EntityType);
            return items?.FirstOrDefault();
        }

        /// <summary>
        /// 7
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetOne<T>(Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            Entity<L> item = this.GetOne(Parameters, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 8
        /// </summary>
        /// <param name="SelectSp"></param>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetOneFromDB(string SelectSp, Dictionary<string, object> Parameters, Type EntityType)
        {
            List<Entity<L>> items = this.GetItemsFromDB(SelectSp, Parameters, EntityType);
            return items?.FirstOrDefault();
        }

        /// <summary>
        /// 8
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SelectSp"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetOneFromDB<T>(string SelectSp, Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            Entity<L> item = this.GetOneFromDB(SelectSp, Parameters, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 9
        /// </summary>
        /// <param name="SelectSp"></param>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetOneFromDB(string SelectSp, string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetOneFromDB(SelectSp, parameters, EntityType);
        }

        /// <summary>
        /// 9
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SelectSp"></param>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetOneFromDB<T>(string SelectSp, string FilterKey, object FilterValue)
            where T : Entity<L>
        {
            Entity<L> item = this.GetOneFromDB(SelectSp, FilterKey, FilterValue, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 10
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetOne(string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetOne(parameters, EntityType);
        }

        /// <summary>
        /// 10
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetOne<T>(string FilterKey, object FilterValue)
            where T : Entity<L>
        {
            Entity<L> item = this.GetOne(FilterKey, FilterValue, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 11
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetLast(Dictionary<string, object> Parameters, Type EntityType)
        {
            List<Entity<L>> items = this.GetItems(Parameters, EntityType);
            return items?.LastOrDefault();
        }

        /// <summary>
        /// 11
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetLast<T>(Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            Entity<L> item = this.GetLast(Parameters, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 12
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual Entity<L> GetLast(string FilterKey, object FilterValue, Type EntityType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddParameterValue(FilterKey, FilterValue);
            return this.GetLast(parameters, EntityType);
        }

        /// <summary>
        /// 12
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetLast<T>(string FilterKey, object FilterValue)
            where T : Entity<L>
        {
            Entity<L> item = this.GetLast(FilterKey, FilterValue, typeof(T));
            return item as T;
        }

        /// <summary>
        /// 13
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="EntityType"></param>
        /// <returns></returns>
        internal virtual List<Entity<L>> Search(Dictionary<string, object> Parameters, Type EntityType)
        {
            string script = this.GetSearchScript(Parameters, EntityType);

            if (string.IsNullOrEmpty(script))
            {
                return new List<Entity<L>>();
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            IDictionaryEnumerator ienum = Parameters.GetEnumerator();

            while (ienum.MoveNext())
            {
                parameters.AddParameterValue(ienum.Key.ToString(), ienum.Value);
            }

            DatabaseResponse response = this.DatabaseConnection.ExecuteItems(script, parameters);
            List<Entity<L>> items = this.CreateEntities(response, EntityType);
            return items;
        }

        /// <summary>
        /// 13
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<T> Search<T>(Dictionary<string, object> Parameters)
            where T : Entity<L>
        {
            List<Entity<L>> items = this.Search(Parameters, typeof(T));
            List<T> items2 = this.ToList<T>(items);
            return items2;
        }

        public sealed override object GetItem(object Id)
        {
            return this.GetItem((L)Id);
        }

        public sealed override Entity[] GetAllItems()
        {
            List<Entity<L>> items = this.GetItems(typeof(Entity));
            return items.ToArray();
        }

        #endregion

        #region Initialization

        public override sealed void Initialize(object t, Dictionary<string, object> dr, bool useAlias)
        {
            this.InitializeEntity((Entity<L>)t, dr, useAlias);
        }

        protected internal sealed override Entity CreateAndInitialize(Dictionary<string, object> dictionaryItem, Type EntityType, bool useAlias)
        {
            return this.CreateAndInitializeEntity(dictionaryItem, EntityType, useAlias);
        }

        protected internal virtual Entity<L> CreateAndInitializeEntity(Dictionary<string, object> dictionaryItem, Type EntityType, bool useAlias)
        {
            Entity<L> item = this.CreateEntity(dictionaryItem, EntityType);
            this.InitializeEntity(item, dictionaryItem, useAlias);
            return (this.GetActiveItemsOnly && !item.IsActive) ? null : item;
        }

        protected internal virtual List<Entity<L>> CreateEntities(DatabaseResponse DataTableObject, Type EntityType, bool useAlias = false)
        {
            bool filterInactiveItems = this.GetActiveItemsOnly;
            List<Entity<L>> items = new List<Entity<L>>();

            foreach (Dictionary<string, object> dictionaryItem in DataTableObject)
            {
                //Entity<L> item = this.CreateEntity(dictionaryItem, EntityType);
                //this.InitializeEntity(item, dictionaryItem, useAlias);
                Entity<L> item = this.CreateAndInitializeEntity(dictionaryItem, EntityType, useAlias);

                if (item != null)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        protected internal sealed override List<Entity<A>> Create<A>(DatabaseResponse dataTableObject, Type entityType, bool useAlias)
        {
            return this.CreateEntities(dataTableObject, entityType, useAlias).Cast<Entity<A>>().ToList();
        }

        //protected internal abstract void Initialize(T t, Dictionary<string, object> dr);

        #endregion

        #region Create Methods

        protected internal virtual Entity<L> CreateEntity(Dictionary<string, object> DataItem, Type EntityType)
        {
            return ObjectFactory.CreateObject(EntityType) as Entity<L>;
        }

        #endregion

        #region Caching

        public override void ClearCache(string name)
        {
            if (!this.CachingEnabled || CacheProvider.Cache[name] == null)
            {
                return;
            }

            CacheProvider.Cache.Remove(name);
        }

        //public void ClearCache<T>(T item)
        //    where T : Entity
        //{
        //    this.ClearCache(item.GetType().FullName);
        //}

        //public static bool ClearCache(string Provider2Name)
        //{
        //    Type t = Type.GetType(Provider2Name);

        //    if (t == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        object obj = ProviderBase.GetInstance(t);

        //        if (obj == null)
        //        {
        //            return false;
        //        }

        //        ((ProviderBase)obj).ClearCache();
        //        return true;
        //    }
        //}

        #endregion

        #region Helper Methods

        protected virtual string GetSpName(string Operation)
        {
            return string.Format("{0}_{1}", typeof(Entity<L>).Name, Operation);
        }

        #endregion

        protected virtual PropertyInfo[] GetProperties(Entity<L> Item)
        {
            PropertyInfo[] properties = Item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return properties.Where(x => !Attribute.IsDefined(x, typeof(NotSavedAttribute)) && !x.PropertyType.IsCollection() && !x.PropertyType.IsArray && x.CanRead && x.CanWrite).ToArray();
        }

        protected virtual List<Entity> GetSaveablePropertyValues(Entity<L> Item)
        {
            List<Entity> items = new List<Entity>();
            PropertyInfo[] properties = Item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !Attribute.IsDefined(x, typeof(NotSavedAttribute))).ToArray();

            if (properties.Length != 0)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsCollection())
                    {
                        Type genericType = property.PropertyType.GetGenericArguments()?[0];

                        if (genericType.IsSubclassOf(typeof(Entity)))
                        {
                            ICollection value = property.GetValue(Item) as ICollection;

                            foreach (var val in value)
                            {
                                if (value != null)
                                {
                                    items.Add(val as Entity);
                                }
                            }
                        }
                    }
                    //else if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    //{
                    //    object value = property.GetValue(Item);

                    //    if (value != null)
                    //    {
                    //        items.Add(value as Entity);
                    //    }
                    //}
                }
            }

            return items;
        }

        #region Script Generation
        protected virtual string GetInsertScript(Entity<L> Item)
        {
            string entityTypeName = Item.TypeName;

            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.InsertSp;
            }
            else
            {
                GarciaStringBuilder sb = new GarciaStringBuilder();
                sb += "begin tran;insert into ";
                //sb += this.EntityTypeName;
                sb += entityTypeName;
                sb += " (";
                PropertyInfo[] properties = this.GetProperties(Item);
                int index = 0;
                List<string> propertyNames = new List<string>();

                foreach (PropertyInfo property in properties)
                {
                    GarciaStringBuilder propertyName = new GarciaStringBuilder(property.Name);

                    if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    {
                        propertyName += "Id";
                    }

                    if (propertyNames.Contains(propertyName.ToString()))
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        propertyNames.Add(propertyName.ToString());
                    }

                    sb += "[";
                    sb += propertyName.ToString();
                    sb += "]";

                    if (index != properties.Length - 1)
                    {
                        sb += ", ";
                    }

                    index++;
                }

                sb += ") values (";
                index = 0;
                propertyNames = new List<string>();

                foreach (PropertyInfo property in properties)
                {
                    GarciaStringBuilder propertyName = new GarciaStringBuilder(property.Name);

                    if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    {
                        propertyName += "Id";
                    }

                    if (propertyNames.Contains(propertyName.ToString()))
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        propertyNames.Add(propertyName.ToString());
                    }

                    sb += "@";
                    sb += propertyName.ToString();

                    if (index != properties.Length - 1)
                    {
                        sb += ", ";
                    }

                    index++;
                }

                sb = new GarciaStringBuilder(sb.ToString().TrimEnd(',', ' '));
                sb += ");select @@identity;commit tran";

                return sb.ToString();
            }
        }

        protected virtual string GetUpdateScript(Entity<L> Item)
        {
            string entityTypeName = Item.TypeName;

            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.UpdateSp;
            }
            else
            {
                GarciaStringBuilder sb = new GarciaStringBuilder();
                sb += "update ";
                //sb += this.EntityTypeName;
                sb += entityTypeName;
                sb += " set ";
                PropertyInfo[] properties = this.GetProperties(Item);
                properties = properties.Where(x => !Attribute.IsDefined(x, typeof(NotUpdatedAttribute))).ToArray();
                int index = 0;
                List<string> propertyNames = new List<string>();

                foreach (PropertyInfo property in properties)
                {
                    GarciaStringBuilder propertyName = new GarciaStringBuilder(property.Name);

                    if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    {
                        propertyName += "Id";
                    }

                    if (propertyNames.Contains(propertyName.ToString()))
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        propertyNames.Add(propertyName.ToString());
                    }

                    sb += "[";
                    sb += propertyName.ToString();
                    sb += "]";
                    sb += " = @";
                    sb += propertyName.ToString();

                    //if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    //{
                    //    sb += this.IdPropertyName;
                    //}

                    if (index != properties.Length - 1)
                    {
                        sb += ", ";
                    }

                    index++;
                }

                sb = new GarciaStringBuilder(sb.ToString().TrimEnd(',', ' '));
                sb += " where ";
                sb += this.IdPropertyName;
                sb += " = '";
                sb += Item.Id.ToString();
                sb += "'";

                if (this.UseDeleteTime)
                {
                    sb += " and DeleteTime is null";
                }

                return sb.ToString();
            }
        }

        protected virtual string GetDeleteScript(Entity<L> Item)
        {
            string entityTypeName = Item.TypeName;

            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.DeleteSp;
            }
            else
            {
                GarciaStringBuilder sb = new GarciaStringBuilder();

                if (this.UseDeleteTime)
                {
                    sb += "update ";
                    //sb += this.EntityTypeName;
                    sb += entityTypeName;
                    sb += " set DeleteTime = getdate() where Id = '";
                    sb += Item.Id.ToString();
                    sb += "' and DeleteTime is null";
                }
                else
                {
                    sb += "delete from ";
                    //sb += this.EntityTypeName;
                    sb += entityTypeName;
                    sb += " where Id = '";
                    sb += Item.Id.ToString();
                    sb += "'";
                }

                return sb.ToString();
            }
        }

        protected virtual string GetCountScript(Dictionary<string, object> parameters, Type entityType)
        {
            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.CountSp;
            }
            else
            {
                return this.GetSelectScript(parameters, SelectType.Count, entityType);
            }
        }

        protected virtual string GetSelectScript(Dictionary<string, object> parameters, Type entityType, string orderBy = null)
        {
            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.SelectSp;
            }
            else
            {
                string orderByScript = this.GetOrderByString(entityType);
                return this.GetSelectScript(parameters, SelectType.Select, entityType, orderByScript);
            }
        }

        protected virtual string GetSearchScript(Dictionary<string, object> Parameters, Type entityType, string orderBy = null)
        {
            if (this.DatabaseConnectionType == DatabaseConnectionType.StoredProcedure)
            {
                return this.SearchSp;
            }
            else
            {
                string orderByScript = this.GetOrderByString(entityType);
                return this.GetSelectScript(Parameters, SelectType.Search, entityType, orderByScript);
            }
        }

        // sonradan tekrar protected yapalim
        public virtual string GetSelectScript(Dictionary<string, object> parameters, SelectType selectType, Type entityType, string orderBy = null, List<JoinClause> joinClause = null, bool useAlias = false)
        {
            if (entityType.IsNullable())
            {
                entityType = entityType.GenericTypeArguments?[0];
            }

            //string entityTypeName = entityType.Name;
            string entityTypeName = TypeToNameMapper.Instance.GetEntityNameFromMapper(entityType);

            GarciaStringBuilder sb = new GarciaStringBuilder();
            string select = "";
            string selectProperties = this.GetSelectProperties(entityType, useAlias);

            if (joinClause != null && joinClause.Count != 0)
            {
                foreach (JoinClause clause in joinClause)
                {
                    string joinSelectProperties = this.GetSelectProperties(clause.EntityType, useAlias);

                    if (!string.IsNullOrEmpty(joinSelectProperties))
                    {
                        selectProperties = new GarciaStringBuilder(selectProperties, ", ", joinSelectProperties).ToString();
                    }
                }
            }

            switch (selectType)
            {
                case SelectType.Count:
                    select = "select count(*) from ";
                    break;
                case SelectType.Search:
                case SelectType.Select:
                default:
                    select = "select " + selectProperties + " from ";
                    break;
            }

            sb += select;
            //sb += this.EntityTypeName;
            sb += entityTypeName;
            sb += " (nolock)";

            if (joinClause != null && joinClause.Count != 0)
            {
                foreach (JoinClause clause in joinClause)
                {
                    sb += " join ";
                    //sb += clause.EntityType.Name;
                    sb += TypeToNameMapper.Instance.GetEntityNameFromMapper(clause.EntityType);
                    sb += " (nolock) on ";
                    sb += entityTypeName;
                    sb += ".";
                    sb += clause.LeftColumn;
                    sb += " = ";
                    sb += clause.TableName;
                    sb += ".";
                    sb += clause.RightColumn;
                }
            }

            bool hasParameter = parameters != null && parameters.Count != 0;

            if (hasParameter || this.UseDeleteTime)
            {
                sb += " where";
            }

            if (hasParameter)
            {
                int index = 0;
                IDictionaryEnumerator ienum = parameters.GetEnumerator();

                while (ienum.MoveNext())
                {
                    string key = ienum.Key.ToString();
                    sb += " ";

                    if (useAlias)
                    {
                        sb += entityTypeName;
                        sb += ".";
                    }

                    sb += key;
                    sb += " = @";
                    sb += key;

                    if (index != parameters.Count - 1)
                    {
                        sb += " and";
                    }

                    index++;
                }
            }

            if (this.UseDeleteTime)
            {
                if (hasParameter)
                {
                    sb += " and";
                }

                sb += " ";

                if (useAlias)
                {
                    sb += entityTypeName;
                    sb += ".";
                }

                sb += "DeleteTime is null";

                if (useAlias && joinClause != null && joinClause.Count != 0)
                {
                    foreach (JoinClause clause in joinClause)
                    {
                        sb += " and ";
                        //sb += clause.EntityType.Name;
                        sb += TypeToNameMapper.Instance.GetEntityNameFromMapper(clause.EntityType);
                        sb += " .DeleteTime is null";
                    }
                }
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                sb += " order by ";
                sb += orderBy;
            }

            return sb.ToString();
        }
        #endregion

        internal virtual Dictionary<string, object> GetCommonParameters(Entity<L> t)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            PropertyInfo[] properties = this.GetProperties(t);

            foreach (PropertyInfo propertyInfo in properties)
            {
                object propertyValue = propertyInfo.GetValue(t);
                GarciaStringBuilder builder = new GarciaStringBuilder("@");
                builder += propertyInfo.Name;

                if (propertyInfo.PropertyType.IsSubclassOf(typeof(Entity)))
                {
                    builder += "Id";

                    if (propertyValue != null)
                    {
                        propertyValue = ((Entity<L>)propertyValue).Id;
                    }
                }

                string parameterName = builder.ToString();

                if (!parameters.ContainsKey(parameterName))
                {
                    parameters.AddParameterValue(parameterName, propertyValue);
                }
            }

            return parameters;
        }

        internal virtual void InitializeEntity(Entity<L> t, Dictionary<string, object> dr, bool useAlias)
        {
            if (dr == null)
            {
                return;
            }

            t.Id = dr.GetValue<L>("Id");

            //IDictionaryEnumerator ienum = dr.GetEnumerator();
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;
                object value = null;
                string keyName = property.Name;

                if (useAlias)
                {
                    keyName = type.Name + "." + keyName;
                }

                if (PrimitiveType.IsPrimitive(propertyType) || propertyType.IsValueType)
                {
                    if (dr.ContainsKey(keyName))
                    {
                        value = dr[keyName];
                    }
                }
                //else if (propertyType.IsSubclassOf(typeof(Entity<L>)))
                //{
                //    keyName = keyName + "Id";

                //    if (dr.ContainsKey(keyName))
                //    {
                //        value = EntityManager<L>.Instance.GetItem((Helpers.GetValueFromObject<L>(dr[keyName])), propertyType);
                //    }
                //}
                //else if (propertyType.IsCollection() == true
                //    && propertyType.IsGenericType)
                //{
                //    string entityTypeName = t.TypeName;
                //    Type innerType = propertyType.GenericTypeArguments?[0];

                //    if (innerType != null && typeof(Entity).IsAssignableFrom(innerType)) // vehbi: dikkat
                //    {
                //        value = EntityManager<L>.Instance.GetItems(entityTypeName + "Id", t.Id, innerType);
                //        value = this.CopyList(value as List<Entity<L>>, innerType);
                //    }
                //}

                if (value != null && value != DBNull.Value)
                {
                    property.SetValue(t, value);
                }
            }

            FieldInfo[] fields = t.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in fields)
            {
                if (!field.Name.StartsWith("_"))
                {
                    continue;
                }

                Type fieldType = field.FieldType;
                object value = null;
                string keyName = field.Name.TrimStart('_').ToCamelCase();

                if (useAlias)
                {
                    keyName = type.Name + "." + keyName;
                }

                if (PrimitiveType.IsPrimitive(fieldType) || fieldType.IsValueType)
                {
                    if (dr.ContainsKey(keyName))
                    {
                        value = dr[keyName];
                    }
                }

                if (value != null && value != DBNull.Value)
                {
                    field.SetValue(t, value);
                }
            }
        }

        protected PropertyInfo[] GetSelectableProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite && !x.IsDefined(typeof(NotSelectedAttribute)) && x.PropertyType.IsCollection() == false).ToArray();
            return properties;
        }

        protected PropertyInfo[] GetOrderByProperties(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x =>     x.PropertyType.IsCollection() == false && x.IsDefined(typeof(OrderAttribute))).ToArray();
            return properties;
        }

        protected string GetOrderByString(Type type)
        {
            GarciaStringBuilder orderByString = new GarciaStringBuilder();
            PropertyInfo[] properties = this.GetOrderByProperties(type);

            if (properties.Length != 0)
            {
                IOrderedEnumerable<PropertyInfo> orderedProperties = properties.OrderBy(x => x.GetCustomAttribute<OrderAttribute>().Order);
                int index = 0;

                foreach (var propertyInfo in orderedProperties)
                {
                    orderByString += propertyInfo.Name;

                    if (index != orderedProperties.Count() - 1)
                    {
                        orderByString += ", ";
                    }

                    index++;
                }
            }

            return orderByString.ToString();
        }

        protected virtual internal string GetSelectProperties(Type type, bool useAlias = false)
        {
            GarciaStringBuilder propertyText = new GarciaStringBuilder();
            List<string> selectProperties = new List<string>();

            PropertyInfo[] properties = this.GetSelectableProperties(type);

            if (properties.Length != 0)
            {
                foreach (PropertyInfo property in properties.Where(x => x.CanRead && x.CanWrite))
                {
                    string propertyName = property.Name;

                    if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                    {
                        propertyName += "Id";
                    }

                    if (!selectProperties.Contains(propertyName))
                    {
                        selectProperties.Add(propertyName);
                    }
                }

                foreach (string selectProperty in selectProperties)
                {
                    if (!string.IsNullOrEmpty(selectProperty))
                    {
                        if (useAlias)
                        {
                            propertyText += type.Name;
                            propertyText += ".";
                        }
                        else
                        {
                            propertyText += "[";
                        }

                        propertyText += selectProperty;

                        if (useAlias)
                        {
                            propertyText += " as [";
                            propertyText += type.Name;
                            propertyText += ".";
                            propertyText += selectProperty;
                            propertyText += "]";
                        }
                        else
                        {
                            propertyText += "]";
                        }

                        propertyText += ", ";
                    }
                }
            }

            //PropertyInfo[] properties = Type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //if (properties.Length != 0)
            //{
            //    foreach (PropertyInfo property in properties.Where(x => x.CanRead && x.CanWrite))
            //    {
            //        Type propertyType = property.PropertyType;

            //        //if (PrimitiveType.IsPrimitive(propertyType) && !property.IsDefined(typeof(NotSelectedAttribute)))
            //        // Hepsini cekelim
            //        if (!property.IsDefined(typeof(NotSelectedAttribute))
            //            && propertyType.IsCollection() == false)
            //        {
            //            string propertyName = property.Name;

            //            if (propertyType.IsSubclassOf(typeof(Entity)))
            //            {
            //                propertyName += "Id";
            //            }

            //            if (!selectProperties.Contains(propertyName))
            //            {
            //                selectProperties.Add(propertyName);
            //            }
            //        }
            //    }

            //    foreach (string selectProperty in selectProperties)
            //    {
            //        if (!string.IsNullOrEmpty(selectProperty))
            //        {
            //            propertyText += selectProperty;
            //            propertyText += ", ";
            //        }
            //    }
            //}

            return propertyText.ToString().Trim().TrimEnd(',');
        }
    }

    ///// <summary>
    ///// For internal inheritance of EntityManager, do not use as base class.
    ///// </summary>
    ///// <typeparam name="K">Provider</typeparam>
    //public class Provider<K> : ProviderBase<K, int>
    //    where K : ProviderBase
    //{
    //    internal Provider()
    //    {
    //    }
    //}

    //public class Provider2<T, K> : Provider2<K, int>
    //    where T : Entity<int>
    //    where K : Provider
    //{
    //    protected override Dictionary<string, object> GetCommonParameters(Entity<int> t)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public sealed class Provider2<T> : Provider2<T, Provider2<T>>
    //    where T : Entity<L><int>
    //{
    //}
}