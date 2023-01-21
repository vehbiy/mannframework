using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public abstract class BaseModel
    {
        public BaseModel(Entity Item) : this()
        {
        }

        public BaseModel()
        {
        }

        public static List<K> GetModels<T, K>(List<T> Items)
          where T : Entity
          where K : BaseModel
        {
            List<K> models = new List<K>();

            foreach (T item in Items)
            {
                K model = GetModel<T, K>(item);
                models.Add(model);
            }

            return models;
        }

        public static K GetModel<T, K>(T Item)
         where T : Entity
         where K : BaseModel
        {
            K model = (K)Activator.CreateInstance(typeof(K), new object[] { Item });
            return model;
        }

        public abstract Entity InnerToEntity();
    }

    public abstract class Model<T, L> : BaseModel
        where T : Entity<L>
        where L : struct
    {
        public virtual L Id { get; set; }

        public Model(T Item) : base(Item)
        {
            this.Id = Item.Id;
        }

        public Model()
        {
        }

        public virtual T ToEntity()
        {
            return null;
        }

        public sealed override Entity InnerToEntity()
        {
            return this.ToEntity();
        }
    }

    public abstract class Model<T> : Model<T, int>
       where T : Entity<int>
    {
        public Model(T Item) : base(Item)
        {
        }

        public Model()
        {
        }
    }

    public abstract class Model : BaseModel
    {
        public Model(Entity Item) : base(Item)
        {
        }

        public Model()
        {
        }
    }
}
