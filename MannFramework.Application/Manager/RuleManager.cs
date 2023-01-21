using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public static class RuleManager
    {
        static List<Rule> rules = EntityManager.Instance.GetItems<Rule>();

        public static T GetValue<T>(string code)
        {
            foreach (Rule rule in rules)
            {
                if (rule.Code.Equals(code))
                {
                    return Helpers.GetValueFromObject<T>(rule.Value);
                }
            }

            return default(T);
        }
    }
}