using MannFramework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class GarciaMvcExtensions
    {
        public static MvcHtmlString LocalizedActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName)
        {
            return LinkExtensions.ActionLink(htmlHelper, GarciaLocalizationManager.Localize(linkTextKey), actionName);
        }

        public static MvcHtmlString LocalizedActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, RouteValueDictionary routeValues)
        {
            return LinkExtensions.ActionLink(htmlHelper, GarciaLocalizationManager.Localize(linkTextKey), actionName, routeValues);
        }

        public static MvcHtmlString LocalizedActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, RouteValueDictionary routeValues, Dictionary<string, object> htmlAttributes)
        {
            return LinkExtensions.ActionLink(htmlHelper, GarciaLocalizationManager.Localize(linkTextKey), actionName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString LocalizedActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string controllerName)
        {
            return LinkExtensions.ActionLink(htmlHelper, GarciaLocalizationManager.Localize(linkTextKey), actionName, controllerName);
        }

        public static MvcHtmlString LocalizedActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string controllerName, RouteValueDictionary routeValues, object htmlAttributes)
        {
            return LinkExtensions.ActionLink(htmlHelper, GarciaLocalizationManager.Localize(linkTextKey), actionName, controllerName, routeValues, ConvertToDictionary(htmlAttributes));
        }

        public static MvcHtmlString LocalizedStyledActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string controllerName, RouteValueDictionary routeValues, string cssClassName = "Property")
        {
            return LocalizedActionLink(htmlHelper, linkTextKey, actionName, controllerName, routeValues, new { @class = ClassName(htmlHelper, cssClassName) });
        }

        public static MvcHtmlString LocalizedStyledActionLinkForExistingItem(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string controllerName, RouteValueDictionary routeValues, string cssClassName = "Property")
        {
            Dictionary<string, object> parameters = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString.ToDictionary();
            bool disabled = !parameters.ContainsKey(("Id"));

            if (disabled)
            {
                return LocalizedActionLink(htmlHelper, linkTextKey, actionName, controllerName, routeValues, new { @class = ClassName(htmlHelper, cssClassName), @disabled = "disabled", onclick = "javascript:return false;" });
            }
            else
            {
                return LocalizedActionLink(htmlHelper, linkTextKey, actionName, controllerName, routeValues, new { @class = ClassName(htmlHelper, cssClassName) });
            }
        }

        public static MvcHtmlString StyledActionLink(this HtmlHelper htmlHelper, string text, string actionName, string controllerName, RouteValueDictionary routeValues, Dictionary<string, object> htmlAttributes, string cssClassName = "Property")
        {
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            htmlAttributes.Add("class", ClassName(htmlHelper, cssClassName));
            return LinkExtensions.ActionLink(htmlHelper, text, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString StyledActionLink(this HtmlHelper htmlHelper, string text, string actionName, RouteValueDictionary routeValues, string cssClassName = "Property")
        {
            htmlHelper.AppendQueryString(ref routeValues);
            return LinkExtensions.ActionLink(htmlHelper, text, actionName, routeValues, ClassNameDictionary(htmlHelper, cssClassName));
        }

        public static MvcHtmlString StyledActionLinkForModal(this HtmlHelper htmlHelper, string text, string actionName, string controllerName, RouteValueDictionary routeValues, object id, string cssClassName = "Modal")
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.Add("data-target", "#" + controllerName.ToLower() + "-" + actionName.ToLower() + "-" + id);
            htmlAttributes.Add("data-toggle", "modal");
            return StyledActionLink(htmlHelper, text, actionName, controllerName, routeValues, htmlAttributes, cssClassName);
        }

        public static MvcHtmlString Modal(this HtmlHelper htmlHelper, object text, string actionName, string controllerName, RouteValueDictionary routeValues, object id, string cssClassName = "Modal")
        {
            if (text == null)
            {
                return new MvcHtmlString("");
            }

            MvcHtmlString link = StyledActionLinkForModal(htmlHelper, text.ToString(), actionName, controllerName, routeValues, id, cssClassName);
            string div = "<br><div class=\"modal inmodal\" id=\"" + controllerName.ToLower() + "-" + actionName.ToLower() + "-" + id.ToString() + "\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\"><br><div class=\"modal-dialog modal-lg\"><br><div class=\"modal-content\"></div></div></div>";
            return new MvcHtmlString(link.ToHtmlString() + div);
        }

        public static MvcHtmlString LocalizedStyledActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, RouteValueDictionary routeValues, string cssClassName = "Property", bool appendQueryString = true)
        {
            //if (appendQueryString)
            //{
            //    htmlHelper.AppendQueryString(ref routeValues);
            //}

            return LocalizedStyledActionLink(htmlHelper, linkTextKey, actionName, null, cssClassName, appendQueryString, routeValues);
        }

        public static MvcHtmlString LocalizedStyledActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string cssClassName = "Property")
        {
            return LocalizedStyledActionLink(htmlHelper, linkTextKey: linkTextKey, actionName: actionName, htmlAttributes: null, cssClassName: cssClassName);
        }

        public static MvcHtmlString LocalizedStyledActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, Dictionary<string, object> htmlAttributes, string cssClassName = "Property", bool appendQueryString = true, RouteValueDictionary routeValues = null)
        {
            //return LocalizedStyledActionLink(htmlHelper: htmlHelper, linkTextKey: linkTextKey, actionName: actionName, controllerName: null, htmlAttributes: htmlAttributes, cssClassName: cssClassName, appendQueryString: appendQueryString, routeValues: routeValues);
            if (appendQueryString)
            {
                htmlHelper.AppendQueryString(ref routeValues, true);
            }

            Dictionary<string, object> cssClass = ClassNameDictionary(htmlHelper, cssClassName);

            if (htmlAttributes == null)
            {
                htmlAttributes = cssClass;
            }
            else
            {
                htmlAttributes = htmlAttributes.Concat(cssClass).ToDictionary(x => x.Key, x => x.Value);
            }

            return LocalizedActionLink(htmlHelper, linkTextKey, actionName, routeValues, htmlAttributes);
        }

        //public static MvcHtmlString LocalizedStyledActionLink(this HtmlHelper htmlHelper, string linkTextKey, string actionName, string controllerName, Dictionary<string, object> htmlAttributes, string cssClassName = "Property", bool appendQueryString = true, RouteValueDictionary routeValues = null)
        //{
        //    if (appendQueryString)
        //    {
        //        htmlHelper.AppendQueryString(ref routeValues, true);
        //    }

        //    Dictionary<string, object> cssClass = ClassNameDictionary(htmlHelper, cssClassName);

        //    if (htmlAttributes == null)
        //    {
        //        htmlAttributes = cssClass;
        //    }
        //    else
        //    {
        //        htmlAttributes = htmlAttributes.Concat(cssClass).ToDictionary(x => x.Key, x => x.Value);
        //    }

        //    return LocalizedActionLink(htmlHelper, linkTextKey, actionName, controllerName, routeValues, htmlAttributes);
        //}

        public static MvcHtmlString CancelLink(this HtmlHelper htmlHelper, string linkTextKey = "Cancel", string actionName = "List", string cssClassName = "Cancel")
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.Add("data-dismiss", "modal");
            //return LocalizedStyledActionLink(htmlHelper, linkTextKey, actionName, htmlAttributes, cssClassName);
            return StyledActionLink(htmlHelper, linkTextKey, "Back", "Account", null, htmlAttributes, cssClassName);
        }

        public static MvcHtmlString DeleteLink(this HtmlHelper htmlHelper, object id, string linkTextKey = "Delete", string actionName = "Delete", string cssClassName = "Delete")
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>
            {
                {"onclick", "return confirm('" + GarciaLocalizationManager.Localize("DeleteConfirmation") + "');"}
            };

            RouteValueDictionary routeValues = new RouteValueDictionary
            {
                { "Id", id }
            };

            return LocalizedStyledActionLink(htmlHelper, linkTextKey, actionName, htmlAttributes, cssClassName, false, routeValues);
        }

        public static MvcHtmlString EditLink(this HtmlHelper htmlHelper, object id, string linkTextKey = "Edit", string actionName = "Edit", string cssClassName = "Edit")
        {
            RouteValueDictionary routeValues = new RouteValueDictionary
            {
                { "Id", id }
            };

            return LocalizedStyledActionLink(htmlHelper, linkTextKey, actionName, null, cssClassName, false, routeValues);
        }

        public static MvcHtmlString SaveLink(this HtmlHelper htmlHelper, string linkTextKey = "Save", string cssClassName = "Save", Dictionary<string, object> htmlAttributes = null)
        {
            //return new MvcHtmlString("<input type=\"submit\" value=\"" + GarciaLocalizationManager.Localize(linkTextKey) + "\" class=\"" + ClassName(htmlHelper, cssClassName) + "\" />");
            string attributeString = "";

            if (htmlAttributes != null && htmlAttributes.Count != 0)
            {
                attributeString = htmlAttributes.ToString("=", " ");
            }

            return new MvcHtmlString("<input type=\"submit\" value=\"" + GarciaLocalizationManager.Localize(linkTextKey) + "\" class=\"" + ClassName(htmlHelper, cssClassName) + "\" " + attributeString + " />");

            ////return new MvcHtmlString("<input type=\"submit\" value=\"" + GarciaLocalizationManager.Localize(linkTextKey) + "\" class=\"" + ClassName(htmlHelper, cssClassName) + " " + attributeString + " />");
            //return new MvcHtmlString("<input type=\"submit\" value=\"" + GarciaLocalizationManager.Localize(linkTextKey) + "\" class=\"" + ClassName(htmlHelper, cssClassName) + " />");
        }

        public static MvcHtmlString SaveAndCreateLink(this HtmlHelper htmlHelper, string linkTextKey = "SaveAndCreate", string cssClassName = "Save")
        {
            return new MvcHtmlString("<input type=\"submit\" value=\"" + GarciaLocalizationManager.Localize(linkTextKey) + "\" class=\"" + ClassName(htmlHelper, cssClassName) + "\" />");
        }

        public static MvcHtmlString SaveLinks(this HtmlHelper htmlHelper, bool createCancel = true, bool createSave = true, bool createSaveAndCreate = false)
        {
            GarciaStringBuilder builder = new GarciaStringBuilder();

            if (createCancel)
            {
                MvcHtmlString cancel = htmlHelper.CancelLink();
                builder += cancel.ToHtmlString();
                builder += "&nbsp";
            }

            if (createSave)
            {
                MvcHtmlString save = htmlHelper.SaveLink();
                builder += save.ToHtmlString();
            }

            if (createSaveAndCreate)
            {
                MvcHtmlString saveAndCreate = htmlHelper.SaveAndCreateLink();
                builder += saveAndCreate.ToHtmlString();
            }

            MvcHtmlString saveLinks = new MvcHtmlString(builder.ToString());
            return saveLinks;
        }

        public static MvcHtmlString StyledActionLink(this HtmlHelper htmlHelper, string text, string actionName, string cssClassName = "Property")
        {
            RouteValueDictionary routeValues = new RouteValueDictionary();
            htmlHelper.AppendQueryString(ref routeValues, true);
            return LinkExtensions.ActionLink(htmlHelper, text, actionName, routeValues, ClassNameDictionary(htmlHelper, cssClassName));
        }

        public static MvcHtmlString StyledDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object selectedValue = null, bool appendOptionLabel = true, string cssClassName = "DropDownList", bool disabled = false, Dictionary<string, object> htmlAttributes = null)
        {
            if (disabled && selectedValue != null)
            {
                MvcHtmlString label = htmlHelper.Label(name + "Label", selectList.Where(x => x.Value == selectedValue.ToString()).FirstOrDefault()?.Text, new { @class = CssClass.Instance["Label"] });
                MvcHtmlString hidden = htmlHelper.Hidden(name, selectList.Where(x => x.Value == selectedValue.ToString()).FirstOrDefault()?.Value);
                return new MvcHtmlString(label.ToHtmlString() + "<br/>" + hidden.ToHtmlString());
            }

            Dictionary<string, object> attributes = new Dictionary<string, object>();
            attributes.Add("class", ClassName(htmlHelper, cssClassName));

            if (disabled)
            {
                attributes.Add("disabled", "disabled");
            }

            attributes.Add("ng-change", "model." + name + "OnChange()");
            attributes.Add("ng-model", "model." + name);
            attributes.Add("ng-disabled", "model." + name + "Disabled");

            if (selectedValue != null)
            {
                attributes.Add("ng-init", "model." + name + "='" + selectedValue.ToString() + "'");
            }

            if (htmlAttributes != null)
            {
                attributes = attributes.Where(x => !htmlAttributes.ContainsKey(x.Key)).Concat(htmlAttributes).ToDictionary(x => x.Key, x => x.Value);
            }

            return htmlHelper.DropDownList(name, selectList, appendOptionLabel ? GarciaLocalizationManager.Localize("Select") : null, attributes);
            //return htmlHelper.DropDownList(name, selectList, appendOptionLabel ? GarciaLocalizationManager.Localize("Select") : null, new { @class = ClassName(htmlHelper, cssClassName) });
        }

        public static MvcHtmlString StyledDropDownListForEnum(this HtmlHelper htmlHelper, string name, Type enumType, object selectedValue = null, Dictionary<string, object> parameters = null, bool appendOptionLabel = true, string cssClassName = "DropDownList", bool disabled = false, Dictionary<string, object> htmlAttributes = null)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (enumType.IsEnum)
            {
                Array values = Enum.GetValues(enumType);

                foreach (var value in values)
                {
                    //bool selected = selectedValue != null && (int)selectedValue == (int)value;
                    //selectList.Add(new SelectListItem() { Text = value.ToString(), Value = ((int)value).ToString(), Selected = selected });
                    selectList.Add(new SelectListItem() { Text = value.GetDescription().ToString(), Value = ((int)value).ToString() });
                }
            }
            else
            {
                //selectList = StyledDropDownList<T>(htmlHelper, parameters, cssClassName);
            }

            int? val = null;

            if (selectedValue != null)
            {
                val = (int)selectedValue;
            }
            else
            {

            }

            selectList = selectList.OrderBy(x => x.Text).ToList();
            return StyledDropDownList(htmlHelper, name, selectList, val, appendOptionLabel, cssClassName, disabled, htmlAttributes);
        }

        public static MvcHtmlString StyledListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string cssClassName = "ListBox")
        {
            return htmlHelper.ListBox(name, selectList, new { @class = ClassName(htmlHelper, cssClassName) });
        }

        //public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, DateTime? value)
        //{
        //    GarciaStringBuilder builder = new GarciaStringBuilder("<div class=\"input-group date\">");
        //    builder += "<span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>";
        //    MvcHtmlString editor = htmlHelper.Editor(name);
        //    return new MvcHtmlString(builder.ToString() + "<br/>" + editor.ToHtmlString());
        //}

        public static IDictionary<string, object> ConvertToDictionary(object values)
        {
            dynamic expando = new ExpandoObject();
            var result = expando as IDictionary<string, object>;

            foreach (System.Reflection.PropertyInfo fi in values.GetType().GetProperties())
            {
                result[fi.Name] = fi.GetValue(values, null);
            }

            return result;
        }

        public static void AppendQueryString(this HtmlHelper htmlHelper, ref RouteValueDictionary routeValues, bool ignoreId = false)
        {
            //if (appendQueryString)
            {
                Dictionary<string, object> parameters = htmlHelper.ViewContext.RequestContext.HttpContext.Request.QueryString.ToDictionary();

                if (routeValues == null)
                {
                    routeValues = new RouteValueDictionary();
                }

                if (parameters.Count != 0)
                {
                    foreach (var parameter in parameters)
                    {
                        string key = parameter.Key;
                        string keyToLower = key.ToLowerInvariant();

                        if ((ignoreId && keyToLower == "id") || keyToLower == "ref")
                        {
                            continue;
                        }

                        if (!routeValues.ContainsKey(key))
                        {
                            routeValues.Add(key, parameter.Value);
                        }
                    }
                }
            }
        }

        public static string ClassName(this HtmlHelper htmlHelper, string name)
        {
            return CssClass.Instance[name];
        }

        public static Dictionary<string, object> ClassNameDictionary(this HtmlHelper htmlHelper, string name)
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.Add("class", CssClass.Instance[name]);
            return htmlAttributes;
        }

        public static string Localize(this HtmlHelper htmlHelper, string key)
        {
            return GarciaLocalizationManager.Localize(key);
        }

        public static IEnumerable<SelectListItem> ToSelectList<T, L>(this List<T> allItems)
            where T : Entity<L>
            where L : struct
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();

            foreach (T item in allItems)
            {
                selectItems.Add(new SelectListItem()
                {
                    Text = item.ToString(),
                    Value = item.Id.ToString()
                });
            }

            return selectItems;
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this List<T> allItems)
            where T : Entity<int>
        {
            return allItems.ToSelectList<T, int>();
        }

        public static List<string> ToIdList<T, L>(this List<T> selectedItems)
          where T : Entity<L>
          where L : struct
        {
            List<string> ids = selectedItems.Select(x => x.Id.ToString()).ToList();
            return ids;
        }

        public static List<string> ToIdList<T>(this List<T> selectedItems)
          where T : Entity<int>
        {
            return ToIdList<T, int>(selectedItems);
        }

        public static IEnumerable<SelectListItem> GetItems<T, L>(this HtmlHelper htmlHelper)
            where T : Entity<L>
            where L : struct
        {
            List<T> items = EntityManager<L>.Instance.GetItems<T>();
            return items.ToSelectList<T, L>();
        }

        public static IEnumerable<SelectListItem> GetItems<T>(this HtmlHelper htmlHelper)
            where T : Entity<int>
        {
            return GetItems<T, int>(htmlHelper);
        }

        public static MvcHtmlString StyledLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string cssClassName = "Label")
        {
            return html.LabelFor<TModel, TValue>(expression, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString StyledLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, string cssClassName = "Label")
        {
            return html.LabelFor<TModel, TValue>(expression, labelText, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString StyledLabel(this HtmlHelper html,
            string expression, string cssClassName = "Label")
        {
            return html.Label(expression, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString StyledLabel(this HtmlHelper html,
            string expression, string labelText, string cssClassName = "Label")
        {
            return html.Label(expression, labelText, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString LocalizedStyledLabel(this HtmlHelper html,
            string expression, string cssClassName = "Label")
        {
            return StyledLabel(html, html.Localize(expression), cssClassName);
        }

        public static MvcHtmlString LocalizedStyledLabel(this HtmlHelper html,
            string expression, string labelText, string cssClassName = "Label")
        {
            return StyledLabel(html, expression, html.Localize(labelText), cssClassName);
        }

        public static MvcHtmlString StyledTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string cssClassName = "TextBox")
        {
            return html.TextBoxFor<TModel, TValue>(expression, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString StyledTextBox(this HtmlHelper html, string expression = "", string cssClassName = "TextBox")
        {
            return html.TextBox(expression, new { @class = ClassName(html, cssClassName) });
        }

        public static MvcHtmlString StyledRightAlignedTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string cssClassName = "TextBoxRightAligned")
        {
            return html.StyledTextBoxFor<TModel, TValue>(expression, cssClassName);
        }

        public static MvcHtmlString EmptyMessage<T>(this HtmlHelper htmlHelper, IEnumerable<T> model, string message = "")
        {
            if (model.Count() == 0)
            {
                return htmlHelper.EmptyMessage();
            }

            return new MvcHtmlString("");
        }

        public static MvcHtmlString EmptyMessage(this HtmlHelper htmlHelper, string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                message = GarciaLocalizationManager.Localize("NoItemsFound");
            }

            string str = "<tr><td colspan=\"20\"><i class=\"widget yellow-bg fa fa-warning fa-2x\"></i>\n" + message + "</td></tr>";
            return new MvcHtmlString(str);
        }

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
            {
                cssClass = "active";
            }

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }

            if (String.IsNullOrEmpty(action))
            {
                action = currentAction;
            }

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static MvcHtmlString EditPageTitle(this HtmlHelper htmlHelper, string itemName, string title = "Edit")
        {
            GarciaStringBuilder builder = new GarciaStringBuilder();

            switch (GarciaLocalizationManager.CultureCode)
            {
                case "tr-TR":
                    builder += GarciaLocalizationManager.Localize(itemName);
                    builder += " ";
                    builder += GarciaLocalizationManager.Localize(title);
                    break;
                case "en-US":
                default:
                    builder += GarciaLocalizationManager.Localize(title);
                    builder += " ";
                    builder += GarciaLocalizationManager.Localize(itemName);
                    break;
            }

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString IndexPageTitle(this HtmlHelper htmlHelper, string itemName, string title = "List")
        {
            GarciaStringBuilder builder = new GarciaStringBuilder(GarciaLocalizationManager.Localize(itemName), " ", GarciaLocalizationManager.Localize(title));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString CreateLink(this HtmlHelper htmlHelper, string text = "Create", string actionName = "Edit", string cssClassName = "Create", RouteValueDictionary routeValues = null)
        {
            //RouteValueDictionary routeValues = new RouteValueDictionary();

            //if (appendRouteValues)
            //{
            //    routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            //}

            //return LocalizedStyledActionLink(htmlHelper, text, actionName, cssClassName);
            //routeValues = new RouteValueDictionary();
            //routeValues.Add("ProjectId", "1");
            return LocalizedStyledActionLink(htmlHelper, linkTextKey: text, actionName: actionName, cssClassName: cssClassName, routeValues: routeValues, appendQueryString: true);
        }

        public static MvcHtmlString CreateLink(this HtmlHelper htmlHelper, string controllerName, string text = "Create", string actionName = "Edit", string cssClassName = "Create", RouteValueDictionary routeValues = null)
        {
            return LocalizedStyledActionLink(htmlHelper, text, actionName, controllerName, routeValues, cssClassName);
            //return LocalizedStyledActionLink(htmlHelper, controllerName: controllerName, linkTextKey: text, actionName: actionName, cssClassName: cssClassName, routeValues: routeValues, appendQueryString: false);
        }

        public static MvcHtmlString DeleteAllLink(this HtmlHelper htmlHelper, string text = "DeleteAll", string actionName = "DeleteAll", string cssClassName = "DeleteAll", RouteValueDictionary routeValues = null)
        {
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>
            {
                {"onclick", "return confirm('" + GarciaLocalizationManager.Localize("DeleteConfirmation") + "');"}
            };

            return LocalizedStyledActionLink(htmlHelper, linkTextKey: text, actionName: actionName, cssClassName: cssClassName, routeValues: routeValues, appendQueryString: true, htmlAttributes: htmlAttributes);
        }

        //public static MvcHtmlString DeleteAllLink(this HtmlHelper htmlHelper, string controllerName, string text = "DeleteAll", string actionName = "DeleteAll", string cssClassName = "DeleteAll", RouteValueDictionary routeValues = null)
        //{
        //    return LocalizedStyledActionLink(htmlHelper, text, actionName, controllerName, routeValues, cssClassName);
        //}

        #region https://itq.nl/asp-net-mvc-flag-enumeration-model-binder/
        /// <summary>
        /// Convert an enumeration to a Mvc.SelectList for use in dropdowns. Note that the enumeration values must all be added to a resource file.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumObj">The enum obj.</param>
        /// <param name="sortAlphabetically">If set to <c>true</c> [the list is sorted alphabetically].</param>
        /// <returns></returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, bool sortAlphabetically = true)
        {
            IList<SelectListItem> values =
                        (from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new SelectListItem
                         {
                             Text = e.ToString(),
                             Value = e.ToString()
                         }).ToList();

            if (sortAlphabetically)
                values = values.OrderBy(v => v.Text).ToList();

            return new SelectList(values, "Value", "Text", enumObj);
        }

        /// <summary>
        /// Creates a checkbox list for flag enums.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <typeparam name="TValue">The model property type.</typeparam>
        /// <param name="html">The html helper.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="htmlAttributes">Optional html attributes.</param>
        /// <param name="sortAlphabetically">Indicates if the checkboxes should be sorted alfabetically.</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListForEnum<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, bool sortAlphabetically = true)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            // Get all enum values
            IEnumerable<TValue> values = Enum.GetValues(typeof(TValue)).Cast<TValue>();

            // Sort them alphabetically by resource name or default enum name
            if (sortAlphabetically)
                values = values.OrderBy(i => i.ToString());

            // Create checkbox list
            var sb = new StringBuilder();
            foreach (var item in values)
            {
                //string text = item.ToString();
                string text = item.GetDescription();
                string itemName = item.ToString();
                TagBuilder label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox");
                label.MergeAttribute("title", text);

                TagBuilder builder = new TagBuilder("input");
                long targetValue = Convert.ToInt64(item);
                long flagValue = Convert.ToInt64(value);

                if ((targetValue & flagValue) == targetValue)
                    builder.MergeAttribute("checked", "checked");

                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", itemName);
                builder.MergeAttribute("name", fieldId);
                // builder.MergeAttribute("ng-model", "model." + fieldId);
                // builder.MergeAttribute("ng-click", "model." + fieldId + "OnClick()");

                // Add optional html attributes
                if (htmlAttributes != null)
                    builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                // builder.InnerHtml = item.ToString();
                label.InnerHtml = builder.ToString(TagRenderMode.Normal) + " " + text;

                sb.Append(label.ToString(TagRenderMode.Normal));

                // Seperate checkboxes by new line
                // sb.Append("<br/>");
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        public static Uri AddQueryStringValue(this Uri uri, string key, string value)
        {
            UriBuilder builder = new UriBuilder(uri);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

            if (query[key] == null)
            {
                query.Add(key, value);
                builder.Query = query.ToString();
                return builder.Uri;
            }
            else
            {
                return uri;
            }
        }
    }

    public class CssClass : Dictionary<string, string>
    {
        public static CssClass Instance { get; private set; }

        static CssClass()
        {
            Instance = new CssClass();
        }

        private CssClass()
        {
            this.Add("Create", "btn btn-primary btn-sm");
            this.Add("Property", "btn btn-white btn-sm");
            this.Add("PropertyList", "btn btn-white btn-sm");
            this.Add("Details", "btn btn-white btn-sm");
            this.Add("Edit", "btn btn-white btn-sm");
            this.Add("Delete", "btn btn-white btn-sm");
            this.Add("DeleteAll", "btn btn-primary btn-sm");
            //this.Add("DeleteAll", "btn btn-outline btn-w-m btn-primary btn-sm");
            this.Add("List", "btn btn-primary btn-sm");
            this.Add("Save", "btn btn-primary btn-sm");
            this.Add("Cancel", "btn btn-primary btn-sm");
            this.Add("DropDownList", "btn btn-default dropdown-toggle dropdownlist form-control");
            this.Add("ListBox", "listbox");
            this.Add("Label", "control-label");
            this.Add("Modal", "");
            this.Add("TextBox", "text-box single-line form-control");
            this.Add("TextBoxRightAligned", "text-box single-line form-control text-right");
            this.Add("LabelRight", "control-label text-right");
            this.Add("ExtraLinks", "btn btn-white btn-sm");
        }
    }
}
