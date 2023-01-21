using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class GarciaConfigurationManager
    {
        public static string GetValue(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }

        public static T GetValue<T>(string Key)
        {
            string value = GetValue(Key);
            return Helpers.GetValueFromObject<T>(value);
        }

        public static void SetConfigurationValues(Type configurationManagerType)
        {
            PropertyInfo[] properties = configurationManagerType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                string value = GarciaConfigurationManager.GetValue(property.Name);
                Helpers.SetPropertyValue(property, value);
            }
        }

        public static void SetConfigurationValues(Type configurationManagerType, object valueObject)
        {
            if (configurationManagerType == null || valueObject == null)
            {
                return;
            }

            PropertyInfo[] properties = configurationManagerType.GetProperties(BindingFlags.Static | BindingFlags.Public);
            PropertyInfo[] properties2 = valueObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                PropertyInfo property2 = properties2.Where(x => x.Name == property.Name).FirstOrDefault();

                if (property2 != null)
                {
                    object value = property2.GetValue(valueObject);
                    Helpers.SetPropertyValue(property, value);
                }
            }
        }
    }
}
