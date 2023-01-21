using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Reflection;

namespace MannFramework
{
    public static class Helpers
    {
        public static T GetValueFromObject<T>(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return default(T);
            }

            try
            {
                return (T)GetValueFromObject(typeof(T), Value);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T GetValueFromObject<T>(object Value, T DefaultValue)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return DefaultValue;
            }

            try
            {
                return (T)GetValueFromObject(typeof(T), Value);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private static object GetValueFromObject(Type type, object value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);

            if (converter == null)
            {
                return null;
            }

            try
            {
                object result = null;

                if (type == typeof(bool) || type == typeof(bool?))
                {
                    int? x = null;

                    try
                    {
                        x = Convert.ToInt32(value);
                    }
                    catch (Exception)
                    {

                    }

                    if (x.HasValue)
                    {
                        result = Convert.ToBoolean(x);
                    }
                    else
                    {
                        result = Convert.ToBoolean(value);
                    }
                }

                // TODO: MongoDB destegi
                //else if (type == typeof(ObjectId))
                //{
                //    result = new ObjectId(value.ToString());
                //}

                //else
                {
                    result = converter.ConvertFromString(value.ToString());
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SetPropertyValue(PropertyInfo property, string value)
        {
            SetPropertyValue(null, property, value);
        }

        public static void SetPropertyValue(PropertyInfo property, object value)
        {
            SetPropertyValue(null, property, value);
        }

        public static void SetPropertyValue(object item, PropertyInfo property, object value)
        {
            if (property != null && value != null)
            {
                if (property.PropertyType.IsEnum)
                {
                    property.SetValue(item, Enum.Parse(property.PropertyType, value.ToString()));
                }
                else
                {
                    Type type = property.PropertyType;

                    if (property.PropertyType.IsNullable())
                    {
                        type = property.PropertyType.GenericTypeArguments?[0];
                    }

                    object temp = value;

                    if (type != value.GetType())
                    {
                        temp = Convert.ChangeType(value, type);
                    }

                    property.SetValue(item, temp);
                }
            }
        }

        public static void SetPropertyValue(object item, string propertyName, object value)
        {
            if (item != null && value != null)
            {
                PropertyInfo[] properties = item.GetType().GetProperties();

                if (properties.Length != 0)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            SetPropertyValue(item, property, value);
                            return;
                        }
                    }
                }
            }
        }

        public static string CreateOneWayHash(string inValue, HashAlgorithm HashAlgorithm = HashAlgorithm.SHA1)
        {
            byte[] result = new byte[inValue.Length];

            try
            {
                System.Security.Cryptography.HashAlgorithm hash = null;

                switch (HashAlgorithm)
                {
                    case HashAlgorithm.SHA1:
                        hash = new SHA1CryptoServiceProvider();
                        break;
                    case HashAlgorithm.MD5:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }

                result = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inValue));
                return Convert.ToBase64String(result);
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        public static string CreateKey(int Length)
        {
            string Key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, Length).ToUpper();
            return Key;
        }

        public static string CreateNumberKey(int length)
        {
            string number = RandomNumber.Between(100000000, int.MaxValue).ToString();
            int numberLength = number.Length;
            return length >= numberLength ? number : number.Substring(0, length);
        }

        public static string GetValueFromHtml(string Html, string IdTagName)
        {
            Match rg = Regex.Match(Html, "=\"" + IdTagName + "\">(.*?)</td>");
            string result = rg.Groups[1].ToString();
            return result;
        }

        public static bool IsBasicType(Type Type)
        {
            return Type.IsValueType || Type.Equals(typeof(string));
        }

        public static List<T> ConvertStringToList<T>(string Value, char Seperator = ';')
        {
            List<T> result = null;

            if (!string.IsNullOrEmpty(Value))
            {
                string[] values = Value.Split(Seperator);
                result = ConvertArrayToList<T>(values);
            }

            return result;
        }

        public static List<T> ConvertArrayToList<T>(object[] Values)
        {
            if (Values == null)
            {
                return null;
            }

            List<T> items = new List<T>();

            foreach (object item in Values)
            {
                if (item != null)
                {
                    T value = Helpers.GetValueFromObject<T>(item);

                    if (value != null)
                    {
                        items.Add(value);
                    }
                }
            }

            return items;
        }

        public static string ReadFromFile(string FilePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static XmlDocument ReadXmlFromFile(string FilePath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(FilePath);
            return document;
        }
    }

    public static class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int Between(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];
            _generator.GetBytes(randomNumber);
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
            int range = maximumValue - minimumValue + 1;
            double randomValueInRange = Math.Floor(multiplier * range);
            return (int)(minimumValue + randomValueInRange);
        }
    }
}
