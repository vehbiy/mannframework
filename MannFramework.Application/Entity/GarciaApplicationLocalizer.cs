using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public class GarciaApplicationLocalizer : ILocalizer
    {
        public List<LocalizationItem> Items { get; set; }
        public List<string> Cultures { get; set; }

        public GarciaApplicationLocalizer(List<string> cultures)
        {
            Items = EntityManager.Instance.GetItems<LocalizationItem>();
            Cultures = cultures;
        }

        //public string Localize(string CultureCode, LocalizationKeysEnum Key)
        //{
        //    return this.Localize(CultureCode, Key.ToString());
        //}

        public string Localize(string cultureCode, string key)
        {
            string localizedValue = "";

            foreach (LocalizationItem item in Items)
            {
                if (item.Code.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (item.CultureCode.Equals(cultureCode))
                    {
                        localizedValue = item.Value;
                        break;
                    }
                }
            }

            //if (string.IsNullOrEmpty(localizedValue))
            //{
            //    foreach (LocalizationItem item in Items)
            //    {
            //        if (item.Code.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            localizedValue = item.Value;
            //            AddMissingLocalizationElement(key, cultureCode);
            //            break;
            //        }
            //    }
            //}

            if (string.IsNullOrEmpty(localizedValue))
            {
                localizedValue = key.Split('.').LastOrDefault()?.ToSplittedTitleCase();
                //localizedValue = key;

                foreach (string culture in Cultures)
                {
                    AddMissingLocalizationElement(key, culture);
                }
            }

            return localizedValue;
        }

        public void AddMissingLocalizationElement(string code, string cultureCode)
        {
            //if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(cultureCode))
            //{
            //    return;
            //}

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Code", code);
            parameters.Add("CultureCode", cultureCode);
            parameters.Add("Value", "");
            EntityManager.Instance.DatabaseConnection.ExecuteNonQuery("AddMissingLocalizationItem", parameters, CommandType.StoredProcedure);

            //LocalizationItem item = new LocalizationItem()
            //{
            //    Code = code,
            //    CultureCode = cultureCode,
            //    Value = ""
            //};

            //EntityManager.Instance.Save(item);
        }
    }
}
