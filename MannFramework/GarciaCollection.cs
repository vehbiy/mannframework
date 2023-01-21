//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MannFramework
//{
//    [Serializable]
//    public class GarciaCollection
//    {
//        internal CustomCollection()
//        {
//        }
//    }

//    [Serializable]
//    public class GarciaCollection<T> : CustomCollection, IList<T>
//        where T : Entity
//    {
//        private List<T> items = new List<T>();

//        public T this[int Index]
//        {
//            get
//            {
//                return this.items[Index];
//            }

//            set
//            {
//                this.items[Index] = value;
//            }
//        }

//        public int Count
//        {
//            get
//            {
//                return this.items.Count;
//            }
//        }

//        public bool IsReadOnly
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public void Add(T Item)
//        {
//            this.items.Add(Item);
//        }

//        public void Clear()
//        {
//            this.items.Clear();
//        }

//        public bool Contains(T Item)
//        {
//            return this.items.Contains(Item);
//        }

//        public void CopyTo(T[] Array, int ArrayIndex)
//        {
//            this.items.CopyTo(Array, ArrayIndex);
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            return this.items.GetEnumerator();
//        }

//        public int IndexOf(T Item)
//        {
//            return this.items.IndexOf(Item);
//        }

//        public void Insert(int Index, T Item)
//        {
//            this.items.Insert(Index, Item);
//        }

//        public bool Remove(T Item)
//        {
//            return this.items.Remove(Item);
//        }

//        public void RemoveAt(int Index)
//        {
//            this.items.RemoveAt(Index);
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.items.GetEnumerator();
//        }
//    }
//}
