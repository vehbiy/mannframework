using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class ModelHelper<L>
        where L : struct
    {
        public static T GetItem<T>(L Id)
            where T : Entity<L>
        {
            T item = EntityManager<L>.Instance.GetItem<T>(Id);
            return item;
        }

        public static MannFrameworkHttpResponseMessage<K> GetModel<T, K>(L Id)
            where T : Entity<L>
            where K : BaseModel
        {
            T item = GetItem<T>(Id);
            K model = Model.GetModel<T, K>(item);
            MannFrameworkHttpResponseMessage<K> response = new MannFrameworkHttpResponseMessage<K>(model);
            return response;
        }

        public static MannFrameworkHttpResponseMessage<List<K>> GetModels<T, K>()
            where T : Entity<L>
            where K : BaseModel
        {
            List<T> items = EntityManager<L>.Instance.GetItems<T>();
            List<K> models = Model.GetModels<T, K>(items);
            return new MannFrameworkHttpResponseMessage<List<K>>(models);
        }

        //public static CustomHttpResponseMessage<K> GetModel<T, K>(L Id)
        //    where T : Entity<L>
        //    where K : BaseModel
        //{
        //    CustomHttpResponseMessage<K> model = GetModel<T, K>(Id);
        //    return model;
        //}

        //public static CustomHttpResponseMessage<List<K>> GetModels<T, K>()
        //    where T : Entity<L>
        //    where K : BaseModel
        //{
        //    CustomHttpResponseMessage<List<K>> models = GetModels<T, K>();
        //    return models;
        //}
    }
}
