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
    public class MannFrameworkApiController<L> : ApiController, IMannFrameworkHttpController
        where L : struct
    {
        public IDynamicModelGenerator DynamicModelGenerator { get; protected set; }

        public IHttpResponseFactory HttpResponseMessageFactory { get; set; }

        public MannFrameworkApiController()
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

    public class MannFrameworkApiController<T, K, L> : MannFrameworkApiController<L>
        where T : Entity<L>
        where K : BaseModel
        where L : struct
    {
        public virtual MannFrameworkHttpResponseMessage<K> Get(L id)
        {
            MannFrameworkHttpResponseMessage<K> model = ModelHelper<L>.GetModel<T, K>(id);
            return model;
        }

        public virtual MannFrameworkHttpResponseMessage<List<K>> Get()
        {
            MannFrameworkHttpResponseMessage<List<K>> models = ModelHelper<L>.GetModels<T, K>();
            return models;
        }

        public virtual MannFrameworkHttpResponseMessage Post([FromBody]K model)
        {
            Entity item = model.InnerToEntity();

            if (item == null)
            {
                return this.HttpResponseMessageFactory.GetResponseMessage(HttpStatusCode.NotFound);
            }

            OperationResult saveResult = EntityManager.Instance.Save(item as T);
            return new MannFrameworkHttpResponseMessage(saveResult);
        }

        public virtual MannFrameworkHttpResponseMessage<K> Delete(L id)
        {
            T entity = ModelHelper<L>.GetItem<T>(id);
            entity.IsMarkedForDeletion = true;
            OperationResult result = EntityManager.Instance.Save(entity);
            return new MannFrameworkHttpResponseMessage<K>(result);
        }
    }

    public class MannFrameworkApiController : MannFrameworkApiController<int>
    {
    }

    public class MannFrameworkApiController<T, K> : MannFrameworkApiController<T, K, int>
       where T : Entity<int>
       where K : BaseModel
    {
    }

    public class MannFrameworkEntityApiController<T, L> : MannFrameworkApiController<L>
        where T : Entity<L>
        where L : struct
    {
        public virtual MannFrameworkHttpResponseMessage<T> Get(L id)
        {
            T item = EntityManager<L>.Instance.GetItem<T>(id);
            return new MannFrameworkHttpResponseMessage<T>(item);
        }

        public virtual MannFrameworkHttpResponseMessage<List<T>> Get()
        {
            List<T> items = EntityManager<L>.Instance.GetItems<T>();
            return new MannFrameworkHttpResponseMessage<List<T>>(items);
        }

        public virtual MannFrameworkHttpResponseMessage Post([FromBody]T item)
        {
            if (item == null)
            {
                return new MannFrameworkHttpResponseMessage(HttpStatusCode.NotFound);
            }

            OperationResult saveResult = EntityManager.Instance.Save(item as T);
            return new MannFrameworkHttpResponseMessage(saveResult);
        }

        public virtual MannFrameworkHttpResponseMessage<T> Delete(L id)
        {
            T entity = EntityManager<L>.Instance.GetItem<T>(id);
            entity.IsMarkedForDeletion = true;
            OperationResult result = EntityManager.Instance.Save(entity);
            return new MannFrameworkHttpResponseMessage<T>(result);
        }
    }

    public class MannFrameworkEntityApiController<T> : MannFrameworkEntityApiController<T, int>
        where T : Entity<int>
    {
    }
}