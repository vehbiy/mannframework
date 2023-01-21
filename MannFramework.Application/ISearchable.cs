using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public interface ISearchable
    {
        string Title { get; }
        string Description { get; }
        string Icon { get; }
        int Id { get; }
    }
}
