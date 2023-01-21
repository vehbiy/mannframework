using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableMappingAttribute : Attribute
    {
        public string TableName { get; set; }

        public TableMappingAttribute()
        {
        }

        public TableMappingAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}
