using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MannFramework.Factory;
using MannFramework.Interface;

namespace MannFramework
{
    public class GarciaApiController<L> : ApiController, IGarciaHttpController
        where L : struct
    {
        public IDynamicModelGenerator DynamicModelGenerator { get; protected set; }

        public IHttpResponseFactory HttpResponseMessageFactory { get; set; }

        public GarciaApiController()
        {
            this.DynamicModelGenerator = new DynamicModelGenerator();
            this.HttpResponseMessageFactory = new HttpResponseFactory();
        }

        // metod provider'da da var
        private bool IsCollection(Type type)
        {
            //return Type.IsSubclassOf(typeof(CustomCollection));

            Type[] interfaces = type.GetInterfaces();
            return interfaces.Contains(typeof(IList));
        }
    }

    public class GarciaApiController<T, K, L> : GarciaApiController<L>
        where T : Entity<L>
        where K : BaseModel
        where L : struct
    {
        public virtual GarciaHttpResponseMessage<K> Get(L id)
        {
            GarciaHttpResponseMessage<K> model = ModelHelper<L>.GetModel<T, K>(id);
            return model;
        }

        public virtual GarciaHttpResponseMessage<List<K>> Get()
        {
            GarciaHttpResponseMessage<List<K>> models = ModelHelper<L>.GetModels<T, K>();
            return models;
        }

        public virtual GarciaHttpResponseMessage Post([FromBody]K model)
        {
            Entity item = model.InnerToEntity();

            if (item == null)
            {
                return this.HttpResponseMessageFactory.GetResponseMessage(HttpStatusCode.NotFound);
            }

            OperationResult saveResult = EntityManager.Instance.Save(item as T);
            return new GarciaHttpResponseMessage(saveResult);
        }

        public virtual GarciaHttpResponseMessage<K> Delete(L id)
        {
            T entity = ModelHelper<L>.GetItem<T>(id);
            entity.IsMarkedForDeletion = true;
            OperationResult result = EntityManager.Instance.Save(entity);
            return new GarciaHttpResponseMessage<K>(result);
        }
    }

    public class GarciaApiController : GarciaApiController<int>
    {
    }

    public class GarciaApiController<T, K> : GarciaApiController<T, K, int>
       where T : Entity<int>
       where K : BaseModel
    {
    }

    public class GarciaEntityApiController<T, L> : GarciaApiController<L>
        where T : Entity<L>
        where L : struct
    {
        public virtual GarciaHttpResponseMessage<T> Get(L id)
        {
            T item = EntityManager<L>.Instance.GetItem<T>(id);
            return new GarciaHttpResponseMessage<T>(item);
        }

        public virtual GarciaHttpResponseMessage<List<T>> Get()
        {
            List<T> items = EntityManager<L>.Instance.GetItems<T>();
            return new GarciaHttpResponseMessage<List<T>>(items);
        }

        public virtual GarciaHttpResponseMessage Post([FromBody]T item)
        {
            if (item == null)
            {
                return new GarciaHttpResponseMessage(HttpStatusCode.NotFound);
            }

            OperationResult saveResult = EntityManager.Instance.Save(item as T);
            return new GarciaHttpResponseMessage(saveResult);
        }

        public virtual GarciaHttpResponseMessage<T> Delete(L id)
        {
            T entity = EntityManager<L>.Instance.GetItem<T>(id);
            entity.IsMarkedForDeletion = true;
            OperationResult result = EntityManager.Instance.Save(entity);
            return new GarciaHttpResponseMessage<T>(result);
        }
    }

    public class GarciaEntityApiController<T> : GarciaEntityApiController<T, int>
        where T : Entity<int>
    {
    }
}