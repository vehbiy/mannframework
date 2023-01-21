using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;

namespace MannFramework
{
    // https://itq.nl/asp-net-mvc-flag-enumeration-model-binder/
    public class FlagEnumModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Fix for the default model binder's failure to decode flag enum types from chexbox list.
        /// </summary>
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            var propertyType = propertyDescriptor.PropertyType;

            // Check if the property type is an enum with the flag attribute
            if (propertyType.IsEnum && propertyType.GetCustomAttributes<FlagsAttribute>().Any())
            {
                var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (providerValue != null)
                {
                    var value = providerValue.RawValue;
                    if (value != null)
                    {
                        // In case it is a checkbox list/dropdownlist/radio button list
                        if (value is string[])
                        {
                            // Create flag value from posted values
                            var flagValue = ((string[])value).Aggregate(0, (current, v) => current | (int)Enum.Parse(propertyType, v));

                            return Enum.ToObject(propertyType, flagValue);
                        }
                        // In case it is a single value
                        if (value.GetType().IsEnum)
                        {
                            return Enum.ToObject(propertyType, value);
                        }
                    }
                }
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }
}
