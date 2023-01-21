using MannFramework.Application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MannFramework.Application.Filter
{
    public class ClickFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //ClickManager.Instance.SaveClick(filterContext.RequestContext.HttpContext.Request.Url.OriginalString);
            Uri url = filterContext.RequestContext.HttpContext.Request.Url;

            if (url.OriginalString.ToLower().Contains("/account/back")
                || url.OriginalString.ToLower().Contains("/account/forward")
                // || url.Query.Contains("ref=1")
                || url.Segments.Last().ToLower() == "delete"
                || url.Segments.Last().ToLower() == "socketlog")
            {
                return;
            }

            ClickManager.Instance.SaveClick(url);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
