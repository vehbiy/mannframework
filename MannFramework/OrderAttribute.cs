using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderAttribute : Attribute
    {
        public int Order { get; set; }
        public OrderType OrderType { get; set; }

        public OrderAttribute(int order, OrderType OrderType = OrderType.Ascending)
        {
            this.Order = order;
            this.OrderType = OrderType;
        }

        public OrderAttribute() : this(0)
        {
        }
    }

    public enum OrderType
    {
        Ascending = 0,
        Descending
    }
}
