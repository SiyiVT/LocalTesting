#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SitefinityWebApp.ResourcePackages.IHH.MVC.Views.LanguageSelector
{
    using System;
    using System.Collections.Generic;
    
    #line 3 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
    using System.Configuration;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 7 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
    using SitefinityWebApp.Helpers;
    
    #line default
    #line hidden
    
    #line 5 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 6 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
    using Telerik.Sitefinity.Modules.Pages;
    
    #line default
    #line hidden
    
    #line 4 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
    using Telerik.Sitefinity.Services;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/LanguageSelector/LanguageSelector.IHHDropdown.cshtml")]
    public partial class LanguageSelector_IHHDropdown : System.Web.Mvc.WebViewPage<Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector.LanguageSelectorViewModel>
    {

#line 55 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
public System.Web.WebPages.HelperResult GetClass(string culture)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 56 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
 
    /**/

    if (Model.CurrentLanguage == culture)
    {
        

#line default
#line hidden

#line 61 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("selected"));


#line default
#line hidden

#line 61 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                             ;
    }


#line default
#line hidden
});

#line 63 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
}
#line default
#line hidden

#line 65 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
public System.Web.WebPages.HelperResult GetCustomLanguageName(string culture)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 66 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
 


    switch (culture)
    {
        case "zh-CHS":
            {
                

#line default
#line hidden

#line 73 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("CHI"));


#line default
#line hidden

#line 73 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                                ; break;
            }
        case "en":
            {
                

#line default
#line hidden

#line 77 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("EN"));


#line default
#line hidden

#line 77 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                               ; break;
            }
        case "ms":
            {
                

#line default
#line hidden

#line 81 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("BM"));


#line default
#line hidden

#line 81 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                               ; break;
            }
        default:
            {
                

#line default
#line hidden

#line 85 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw(culture.ToUpper()));


#line default
#line hidden

#line 85 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                                            ; break;
            }
    }


#line default
#line hidden
});

#line 88 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
}
#line default
#line hidden

#line 90 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
public System.Web.WebPages.HelperResult GetCustomUrl(string culture, string rawUrl)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 91 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
 
    string currentlanguage = Model.CurrentLanguage;

    if (currentlanguage != "en")
    {
        switch (culture)
        {
            case "en":
                {
                    string removeStr = "/" + currentlanguage;
                    string newUrl = rawUrl.Substring(removeStr.Length);
                    if (newUrl != "")
                    {
                        

#line default
#line hidden

#line 104 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw(newUrl));


#line default
#line hidden

#line 104 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                                         ;
                    }
                    else
                    {
                        

#line default
#line hidden

#line 108 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("/"));


#line default
#line hidden

#line 108 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                                      ;
                    }
                    break;
                }
        }

    }



#line default
#line hidden
});

#line 116 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
}
#line default
#line hidden

        public LanguageSelector_IHHDropdown()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 9 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
Write(Html.Script(ScriptRef.JQuery, "top", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 11 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
   string CssClass = !string.IsNullOrEmpty(Model.CssClass) ? @Model.CssClass : "js-language";
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 13 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
  
    string cdnHost = string.IsNullOrEmpty(ConfigurationManager.AppSettings["HostCDN"]) ? "~/" : ConfigurationManager.AppSettings["HostCDN"];

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"language-select-div d-lg-block d-none\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"dropdown\"");

WriteLiteral(">\r\n        <button");

WriteAttribute("class", Tuple.Create(" class=\"", 687), Tuple.Create("\"", 760)
, Tuple.Create(Tuple.Create("", 695), Tuple.Create("btn", 695), true)
, Tuple.Create(Tuple.Create(" ", 698), Tuple.Create("btn-secondary", 699), true)
, Tuple.Create(Tuple.Create(" ", 712), Tuple.Create("dropdown", 713), true)
, Tuple.Create(Tuple.Create(" ", 721), Tuple.Create("language-select", 722), true)
, Tuple.Create(Tuple.Create(" ", 737), Tuple.Create("mb-lg-0", 738), true)
, Tuple.Create(Tuple.Create(" ", 745), Tuple.Create("mb-3", 746), true)
            
            #line 18 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
, Tuple.Create(Tuple.Create(" ", 750), Tuple.Create<System.Object, System.Int32>(CssClass
            
            #line default
            #line hidden
, 751), false)
);

WriteLiteral(" type=\"button\"");

WriteLiteral(" data-bs-toggle=\"dropdown\"");

WriteLiteral(" data-culture=\"");

            
            #line 18 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
                                                                                                                                           Write(Model.CurrentLanguage);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 19 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
       Write(GetCustomLanguageName(Model.CurrentLanguage));

            
            #line default
            #line hidden
WriteLiteral("\r\n            ");

WriteLiteral("\r\n        </button>\r\n        ");

WriteLiteral("\r\n\r\n    </div>\r\n</div>\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

            
            #line 118 "..\..MVC\Views\LanguageSelector\LanguageSelector.IHHDropdown.cshtml"
Write(Html.Script(Url.WidgetContent(cdnHost + "Frontend-Assembly/Telerik.Sitefinity.Frontend.Navigation/Mvc/Scripts/LanguageSelector/language-selector.js"), "bottom", false));

            
            #line default
            #line hidden
WriteLiteral(" ");

        }
    }
}
#pragma warning restore 1591
