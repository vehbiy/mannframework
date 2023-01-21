using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Macondo.BL
{
    [Serializable]
    public class MacondoMember : Member
    {
        public new List<Project> Projects { get; set; }
    }
}
