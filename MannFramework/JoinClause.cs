using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class JoinClause
    {
        public string TableName { get; set; }
        public string LeftColumn { get; set; }
        public string RightColumn { get; set; }
        public Type EntityType { get; internal set; }
        public bool IncludeColumns { get; internal set; }
    }
}
