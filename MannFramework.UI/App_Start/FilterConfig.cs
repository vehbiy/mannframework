using MannFramework.Application.Filter;
using System.Web;
using System.Web.Mvc;

namespace MannFramework.Macondo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ClickFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
