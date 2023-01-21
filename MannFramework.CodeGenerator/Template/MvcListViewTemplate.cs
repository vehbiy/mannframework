﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Garcia.CodeGenerator.Template
{
    using MannFramework;
    using MannFramework.Application;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class MvcListViewTemplate : BaseTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            
            #line 8 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"

	string itemName = Item.Name;
	string modelName = itemName;

	if (GenerateMvcModel)
	{
		modelName += "Model";
	}

	string controllerName = itemName + "Controller";
	string viewName = "List";
	List<string> elements = new List<string>();
	List<ItemProperty> properties = Item.Properties.Where(x => !x.MvcIgnore && !x.MvcListIgnore).ToList();

            
            #line default
            #line hidden
            this.Write("@model IEnumerable");
            
            #line 22 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture("<" + modelName + ">"));
            
            #line default
            #line hidden
            this.Write("\r\n\r\n<div class=\"wrapper wrapper-content animated fadeInRight\">\r\n    <div class=\"r" +
                    "ow\">\r\n        <div class=\"col-lg-12\">\r\n            <div class=\"ibox float-e-marg" +
                    "ins\">\r\n                <div class=\"ibox-title\">\r\n\t\t\t\t\t<h5>@Html.IndexPageTitle(\"" +
                    "");
            
            #line 29 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(itemName));
            
            #line default
            #line hidden
            this.Write("\")</h5>\r\n\t\t\t\t\t<div class=\"ibox-tools\">\r\n\t\t\t\t\t\t@Html.CreateLink()\r\n\t\t\t\t\t</div>\r\n  " +
                    "              </div>\r\n                <div class=\"ibox-content\">\r\n\t\t\t\t\t<table cl" +
                    "ass=\"table table-striped\">\r\n\t\t\t\t\t\t<thead>\r\n\t\t\t\t\t\t\t<tr>\r\n");
            
            #line 38 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 foreach (var property in properties) { 
            
            #line default
            #line hidden
            
            #line 39 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (elements.Contains(property.Name)) continue; 
            
            #line default
            #line hidden
            
            #line 40 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 else elements.Add(property.Name); 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t<th>\r\n");
            
            #line 42 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.MappingType == ItemPropertyMappingType.Property) { 
            
            #line default
            #line hidden
            
            #line 43 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.InnerType != null && !elements.Contains(property.Name + "Id")) elements.Add(property.Name + "Id"); 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t@Html.Localize(\"");
            
            #line 44 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(itemName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 44 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("\")\r\n");
            
            #line 45 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t</th>\r\n");
            
            #line 47 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t<th></th>\r\n\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t</thead>\r\n\t\t\t\t\t\t<tbody>\r\n\t\t\t\t\t\t\t@Html.Empt" +
                    "yMessage(Model)\r\n\t\t\t\t\t\t\t@foreach (var item in Model)\r\n\t\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\t<tr>\r\n");
            
            #line 56 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 elements.Clear(); 
            
            #line default
            #line hidden
            
            #line 57 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 foreach (var property in properties) { 
            
            #line default
            #line hidden
            
            #line 58 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (elements.Contains(property.Name)) continue; 
            
            #line default
            #line hidden
            
            #line 59 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 else elements.Add(property.Name); 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t<td>\r\n");
            
            #line 61 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.MappingType == ItemPropertyMappingType.Property) { 
            
            #line default
            #line hidden
            
            #line 62 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.InnerType != null && !elements.Contains(property.Name + "Id")) elements.Add(property.Name + "Id"); 
            
            #line default
            #line hidden
            
            #line 63 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.Type == ItemPropertyType.Class) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t\t@Html.Modal(item.");
            
            #line 64 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(", \"Details\", \"");
            
            #line 64 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.InnerType.Name));
            
            #line default
            #line hidden
            this.Write("\", new RouteValueDictionary { { \"Id\", item.");
            
            #line 64 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("Id } }, item.");
            
            #line 64 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("Id)\r\n");
            
            #line 65 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} else { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t\t@Html.DisplayFor(modelItem => item.");
            
            #line 66 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n");
            
            #line 67 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            
            #line 68 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t</td>\r\n");
            
            #line 70 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t<td>\r\n");
            
            #line 72 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 foreach (var property in Item.Properties) { 
            
            #line default
            #line hidden
            
            #line 73 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
 if (property.MappingType != ItemPropertyMappingType.Property && property.InnerType != null) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t\t@Html.LocalizedStyledActionLink(\"");
            
            #line 74 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            
            #line default
            #line hidden
            this.Write("\", \"List\", \"");
            
            #line 74 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(property.InnerType.Name));
            
            #line default
            #line hidden
            this.Write("\", new RouteValueDictionary { { \"");
            
            #line 74 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(itemName));
            
            #line default
            #line hidden
            this.Write("Id\", item.Id } })\r\n");
            
            #line 75 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            
            #line 76 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"
} 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\t\t\t\t@Html.EditLink(item.Id)\r\n\t\t\t\t\t\t\t\t\t\t@Html.DeleteLink(item.Id)\r\n\t\t\t\t\t\t\t\t\t" +
                    "</td>\r\n\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t}\r\n\t\t\t\t\t\t</tbody>\r\n                    </table>\r\n\t\t" +
                    "\t\t</div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n </div>");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Users\vehbi\source\repos\garcia\Garcia.CodeGenerator\Template\MvcListViewTemplate.tt"

private global::MannFramework.Application.Item _ItemField;

/// <summary>
/// Access the Item parameter of the template.
/// </summary>
private global::MannFramework.Application.Item Item
{
    get
    {
        return this._ItemField;
    }
}

private bool _GenerateMvcModelField;

/// <summary>
/// Access the GenerateMvcModel parameter of the template.
/// </summary>
private bool GenerateMvcModel
{
    get
    {
        return this._GenerateMvcModelField;
    }
}

private string _ViewDataTypeNameField;

/// <summary>
/// Access the ViewDataTypeName parameter of the template.
/// </summary>
private string ViewDataTypeName
{
    get
    {
        return this._ViewDataTypeNameField;
    }
}

private string _ViewDataTypeShortNameField;

/// <summary>
/// Access the ViewDataTypeShortName parameter of the template.
/// </summary>
private string ViewDataTypeShortName
{
    get
    {
        return this._ViewDataTypeShortNameField;
    }
}

private bool _IsPartialViewField;

/// <summary>
/// Access the IsPartialView parameter of the template.
/// </summary>
private bool IsPartialView
{
    get
    {
        return this._IsPartialViewField;
    }
}

private bool _IsLayoutPageSelectedField;

/// <summary>
/// Access the IsLayoutPageSelected parameter of the template.
/// </summary>
private bool IsLayoutPageSelected
{
    get
    {
        return this._IsLayoutPageSelectedField;
    }
}

private bool _ReferenceScriptLibrariesField;

/// <summary>
/// Access the ReferenceScriptLibraries parameter of the template.
/// </summary>
private bool ReferenceScriptLibraries
{
    get
    {
        return this._ReferenceScriptLibrariesField;
    }
}

private bool _IsBundleConfigPresentField;

/// <summary>
/// Access the IsBundleConfigPresent parameter of the template.
/// </summary>
private bool IsBundleConfigPresent
{
    get
    {
        return this._IsBundleConfigPresentField;
    }
}

private string _ViewNameField;

/// <summary>
/// Access the ViewName parameter of the template.
/// </summary>
private string ViewName
{
    get
    {
        return this._ViewNameField;
    }
}

private string _LayoutPageFileField;

/// <summary>
/// Access the LayoutPageFile parameter of the template.
/// </summary>
private string LayoutPageFile
{
    get
    {
        return this._LayoutPageFileField;
    }
}

private string _JQueryVersionField;

/// <summary>
/// Access the JQueryVersion parameter of the template.
/// </summary>
private string JQueryVersion
{
    get
    {
        return this._JQueryVersionField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public override void Initialize()
{
    base.Initialize();
    if ((this.Errors.HasErrors == false))
    {
bool ItemValueAcquired = false;
if (this.Session.ContainsKey("Item"))
{
    this._ItemField = ((global::MannFramework.Application.Item)(this.Session["Item"]));
    ItemValueAcquired = true;
}
if ((ItemValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Item");
    if ((data != null))
    {
        this._ItemField = ((global::MannFramework.Application.Item)(data));
    }
}
bool GenerateMvcModelValueAcquired = false;
if (this.Session.ContainsKey("GenerateMvcModel"))
{
    this._GenerateMvcModelField = ((bool)(this.Session["GenerateMvcModel"]));
    GenerateMvcModelValueAcquired = true;
}
if ((GenerateMvcModelValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("GenerateMvcModel");
    if ((data != null))
    {
        this._GenerateMvcModelField = ((bool)(data));
    }
}
bool ViewDataTypeNameValueAcquired = false;
if (this.Session.ContainsKey("ViewDataTypeName"))
{
    this._ViewDataTypeNameField = ((string)(this.Session["ViewDataTypeName"]));
    ViewDataTypeNameValueAcquired = true;
}
if ((ViewDataTypeNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ViewDataTypeName");
    if ((data != null))
    {
        this._ViewDataTypeNameField = ((string)(data));
    }
}
bool ViewDataTypeShortNameValueAcquired = false;
if (this.Session.ContainsKey("ViewDataTypeShortName"))
{
    this._ViewDataTypeShortNameField = ((string)(this.Session["ViewDataTypeShortName"]));
    ViewDataTypeShortNameValueAcquired = true;
}
if ((ViewDataTypeShortNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ViewDataTypeShortName");
    if ((data != null))
    {
        this._ViewDataTypeShortNameField = ((string)(data));
    }
}
bool IsPartialViewValueAcquired = false;
if (this.Session.ContainsKey("IsPartialView"))
{
    this._IsPartialViewField = ((bool)(this.Session["IsPartialView"]));
    IsPartialViewValueAcquired = true;
}
if ((IsPartialViewValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("IsPartialView");
    if ((data != null))
    {
        this._IsPartialViewField = ((bool)(data));
    }
}
bool IsLayoutPageSelectedValueAcquired = false;
if (this.Session.ContainsKey("IsLayoutPageSelected"))
{
    this._IsLayoutPageSelectedField = ((bool)(this.Session["IsLayoutPageSelected"]));
    IsLayoutPageSelectedValueAcquired = true;
}
if ((IsLayoutPageSelectedValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("IsLayoutPageSelected");
    if ((data != null))
    {
        this._IsLayoutPageSelectedField = ((bool)(data));
    }
}
bool ReferenceScriptLibrariesValueAcquired = false;
if (this.Session.ContainsKey("ReferenceScriptLibraries"))
{
    this._ReferenceScriptLibrariesField = ((bool)(this.Session["ReferenceScriptLibraries"]));
    ReferenceScriptLibrariesValueAcquired = true;
}
if ((ReferenceScriptLibrariesValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ReferenceScriptLibraries");
    if ((data != null))
    {
        this._ReferenceScriptLibrariesField = ((bool)(data));
    }
}
bool IsBundleConfigPresentValueAcquired = false;
if (this.Session.ContainsKey("IsBundleConfigPresent"))
{
    this._IsBundleConfigPresentField = ((bool)(this.Session["IsBundleConfigPresent"]));
    IsBundleConfigPresentValueAcquired = true;
}
if ((IsBundleConfigPresentValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("IsBundleConfigPresent");
    if ((data != null))
    {
        this._IsBundleConfigPresentField = ((bool)(data));
    }
}
bool ViewNameValueAcquired = false;
if (this.Session.ContainsKey("ViewName"))
{
    this._ViewNameField = ((string)(this.Session["ViewName"]));
    ViewNameValueAcquired = true;
}
if ((ViewNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ViewName");
    if ((data != null))
    {
        this._ViewNameField = ((string)(data));
    }
}
bool LayoutPageFileValueAcquired = false;
if (this.Session.ContainsKey("LayoutPageFile"))
{
    this._LayoutPageFileField = ((string)(this.Session["LayoutPageFile"]));
    LayoutPageFileValueAcquired = true;
}
if ((LayoutPageFileValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("LayoutPageFile");
    if ((data != null))
    {
        this._LayoutPageFileField = ((string)(data));
    }
}
bool JQueryVersionValueAcquired = false;
if (this.Session.ContainsKey("JQueryVersion"))
{
    this._JQueryVersionField = ((string)(this.Session["JQueryVersion"]));
    JQueryVersionValueAcquired = true;
}
if ((JQueryVersionValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("JQueryVersion");
    if ((data != null))
    {
        this._JQueryVersionField = ((string)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
}
