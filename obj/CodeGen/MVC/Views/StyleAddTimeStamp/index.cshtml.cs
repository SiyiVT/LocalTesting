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

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 5 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 3 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 6 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    using Telerik.Sitefinity.Modules.Pages;
    
    #line default
    #line hidden
    
    #line 4 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    using Telerik.Sitefinity.Services;
    
    #line default
    #line hidden
    
    #line 7 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    using Telerik.Sitefinity.UI.MVC;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/StyleAddTimeStamp/index.cshtml")]
    public partial class _MVC_Views_StyleAddTimeStamp_index_cshtml : System.Web.Mvc.WebViewPage<SitefinityWebApp.Mvc.Models.StyleAddTimeStampModel>
    {
        public _MVC_Views_StyleAddTimeStamp_index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n");

            
            #line 10 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
 if (Model.StyleFilePath != string.Empty)
{
    Html.StyleSheet(Url.WidgetContent(Model.StyleFilePath + "?ts=" + DateTime.Now), "head", true);
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 15 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
 if (SystemManager.IsDesignMode)
{

            
            #line default
            #line hidden
WriteLiteral("    <div>");

            
            #line 17 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
    Write(Model.StyleFilePath);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 18 "..\..\MVC\Views\StyleAddTimeStamp\index.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
