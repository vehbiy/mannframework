using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Amazon.Runtime.Internal;

namespace MannFramework.Application.Manager
{
    public static class MenuItemManager
    {
        public static List<MenuItem> GetMenuItems()
        {
            Member member = StateManager.Instance.Member;
            List<MenuItem> allowedMenuItems = new List<MenuItem>();

            if (member == null)
            {
                return allowedMenuItems;
            }

            List<MenuItem> items = EntityManager.Instance.GetItems<MenuItem>();
            string memberRoles = string.Join(",", member.Roles.Select(p => p.Role.Id)) + ",";

            foreach (MenuItem item in items)
            {
                bool allowed = false;

                foreach (MenuItemRole menuItemRole in item.AllowedRoles)
                {
                    if (memberRoles.Contains(menuItemRole.Role.Id + ","))
                    {
                        allowed = true;
                        break;
                    }
                }

                if (!allowed)
                {
                    continue;
                }

                Regex regex = new Regex(@"{[aA-zZ]*[0-9]*}");
                MatchCollection matches = regex.Matches(item.Url);

                foreach (Match m in matches)
                {
                    if (!string.IsNullOrEmpty(m.Value))
                    {
                        string parameterName = m.Value.Replace("{", "").Replace("}", "");

                        if (!string.IsNullOrEmpty(parameterName))
                        {
                            string parameterValue = "";

                            if (HttpContext.Current != null)
                            {
                                if (HttpContext.Current.Request != null)
                                {
                                    parameterValue = HttpContext.Current.Request.QueryString[parameterName];
                                }

                                if (string.IsNullOrEmpty(parameterValue))
                                {
                                    parameterValue = StateManager.Instance.Read<string>(parameterName);
                                }
                            }

                            if (parameterValue == null)
                            {
                                parameterValue = "";
                            }

                            item.ConvertedUrl = item.Url.Replace(m.Value, parameterValue);
                        }
                    }
                }

                if (string.IsNullOrEmpty(item.ConvertedUrl))
                {
                    item.ConvertedUrl = item.Url;
                }

                allowedMenuItems.Add(item);
            }

            return allowedMenuItems.OrderBy(x => x.Order).ToList();
        }
    }
}
