using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MannFramework
{
    public static class MannFrameworkLocalizationManager
    {
        public static string Localize(string cultureCode, string key)
        {
            return DependencyManager.Localizer.Localize(cultureCode, key);
        }

        public static string LocalizeToTurkish(string key)
        {
            return Localize("tr-TR", key);
        }

        public static string Localize(string key)
        {
            return DependencyManager.Localizer.Localize(CultureCode, key);
        }

        public static string CultureCode
        {
            get
            {
                return (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["CultureCode"] == null) ? System.Threading.Thread.CurrentThread.CurrentCulture.Name : HttpContext.Current.Session["CultureCode"].ToString();
            }
        }
    }
}
