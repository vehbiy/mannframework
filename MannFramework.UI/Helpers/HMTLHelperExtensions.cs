using MannFramework.Application;
using MannFramework.Macondo.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MannFramework.Macondo
{
    public static class HtmlHelperExtensions
    {
        public static IEnumerable<SelectListItem> GetProjectItems(this HtmlHelper html, bool isEnum = false)
        {
            //return UIStateManager.Instance.Project.Items.Where(x => x.IsEnum == isEnum).ToList().ToSelectList<Item, int>();
            return UIStateManager.Instance.Project.Items.ToSelectList<Item, int>();
        }

        public static IEnumerable<SelectListItem> GetProjects(this HtmlHelper html, bool isEnum = false)
        {
            return UIStateManager.Instance.Member.GetProjects().Select(x => x.Project).ToList().ToSelectList<Project, int>();
        }
    }
}
