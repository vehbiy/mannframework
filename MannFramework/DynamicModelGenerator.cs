using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public interface IDynamicModelGenerator
    {
        List<dynamic> GenerateModels<L>(IList Items);
        dynamic GenerateModel<L>(Entity Item);
    }

    public class DynamicModelGenerator : IDynamicModelGenerator
    {
        dynamic IDynamicModelGenerator.GenerateModel<L>(Entity Item)
        {
            throw new NotImplementedException();
        }

        List<dynamic> IDynamicModelGenerator.GenerateModels<L>(IList Items)
        {
            throw new NotImplementedException();
        }
    }
}
