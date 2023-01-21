using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    /// <summary>
    /// Base class for custom providers
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="K">Provider</typeparam>
    /// <typeparam name="L">Id type</typeparam>
    public abstract class Provider<T, K, L> : ProviderBase<K, L>
        where T : Entity<L>
        where K : ProviderBase
        where L : struct
    {
        /// <summary>
        /// 1
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetItems()
        {
            return this.GetItems<T>();
        }

        /// <summary>
        /// 2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<T> GetItems(Dictionary<string, object> Parameters)
        {
            return this.GetItems<T>(Parameters);
        }

        /// <summary>
        /// 3
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual List<T> GetItems(string FilterKey, object FilterValue)
        {
            return this.GetItems(FilterKey, FilterValue);
        }

        /// <summary>
        /// 4
        /// </summary>
        /// <returns></returns>
        public virtual int GetItemCount()
        {
            return this.GetItemCount<T>();
        }

        /// <summary>
        /// 5
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual int GetItemCount(Dictionary<string, object> Parameters)
        {
            return this.GetItemCount<T>(Parameters);
        }

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual int GetItemCount(string FilterKey, object FilterValue)
        {
            return this.GetItemCount<T>(FilterKey, FilterValue);
        }

        /// <summary>
        /// 7
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetOne(Dictionary<string, object> Parameters)
        {
            return this.GetOne<T>(Parameters);
        }

        /// <summary>
        /// 8
        /// </summary>
        /// <param name="SelectSp"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetOneFromDB(string SelectSp, Dictionary<string, object> Parameters)
        {
            return this.GetOneFromDB<T>(SelectSp, Parameters);
        }

        /// <summary>
        /// 9
        /// </summary>
        /// <param name="SelectSp"></param>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetOneFromDB(string SelectSp, string FilterKey, object FilterValue)
        {
            return this.GetOneFromDB<T>(SelectSp, FilterKey, FilterValue);
        }

        /// <summary>
        /// 10
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetOne(string FilterKey, object FilterValue)
        {
            return this.GetOne<T>(FilterKey, FilterValue);
        }

        /// <summary>
        /// 11
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual T GetLast(Dictionary<string, object> Parameters)
        {
            return this.GetLast<T>(Parameters);
        }

        /// <summary>
        /// 12
        /// </summary>
        /// <param name="FilterKey"></param>
        /// <param name="FilterValue"></param>
        /// <returns></returns>
        public virtual T GetLast(string FilterKey, object FilterValue)
        {
            return this.GetLast<T>(FilterKey, FilterValue);
        }

        /// <summary>
        /// 13
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public virtual List<T> Search(Dictionary<string, object> Parameters)
        {
            return this.Search<T>(Parameters);
        }

        /// <summary>
        /// 14
        /// </summary>
        /// <param name="Id"></param>
        public virtual T GetItem(L? Id)
        {
            return this.GetItem<T>(Id);
        }

        protected internal virtual Dictionary<string, object> GetCommonParameters(T Item)
        {
            return this.GetCommonParameters(Item as Entity<L>);
        }

        protected internal virtual void InitializeEntity(T t, Dictionary<string, object> dr, bool useAlias)
        {
            base.InitializeEntity(t as Entity<L>, dr, useAlias);
        }

        // TODO: problem olabilir
        protected internal override Entity<L> CreateAndInitializeEntity(Dictionary<string, object> dictionaryItem, Type entityType, bool useAlias)
        {
            Entity<L> item = this.CreateEntity(dictionaryItem, entityType);

            if (entityType.Equals(typeof(T)))
            {
                this.InitializeEntity(item as T, dictionaryItem, useAlias);
            }
            else
            {
                base.InitializeEntity(item, dictionaryItem, useAlias);
            }

            return item;
        }

        public virtual OperationResult Delete(L Id)
        {
            T entity = this.GetItem(Id);

            if (entity == null)
            {
                return new OperationResult("EntityNotFound");
            }

            entity.IsMarkedForDeletion = true;
            OperationResult result = this.Save(entity);
            return result;
        }
    }

    /// <summary>
    /// Base class for custom providers. Default Id type is integer.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="K">Provider</typeparam>
    public abstract class Provider<T, K> : Provider<T, K, int>
        where T : Entity<int>
        where K : ProviderBase
    {

    }
}
