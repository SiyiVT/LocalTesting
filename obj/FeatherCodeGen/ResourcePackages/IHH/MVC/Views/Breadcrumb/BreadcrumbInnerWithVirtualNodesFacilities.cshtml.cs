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

namespace SitefinityWebApp.ResourcePackages.IHH.MVC.Views.Breadcrumb
{
    using System;
    using System.Collections.Generic;
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
    
    #line 3 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/Breadcrumb/BreadcrumbInnerWithVirtualNodesFacilities.cshtml")]
    public partial class BreadcrumbInnerWithVirtualNodesFacilities : System.Web.Mvc.WebViewPage<Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb.BreadcrumbViewModel>
    {
        public BreadcrumbInnerWithVirtualNodesFacilities()
        {
        }
        public override void Execute()
        {
WriteLiteral("<ul");

WriteLiteral(" class=\"breadcrumb\"");

WriteLiteral(">\r\n");

            
            #line 5 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
    
            
            #line default
            #line hidden
            
            #line 5 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
     for (int i = 0; i < Model.SiteMapNodes.Count; i++)
    {
    var node = Model.SiteMapNodes[i];

    if (i == Model.SiteMapNodes.Count - 1 && Model.ShowCurrentPageInTheEnd)
    {

            
            #line default
            #line hidden
WriteLiteral("    <li");

WriteLiteral(" class=\"breadcrumb-item active\"");

WriteLiteral(" aria-current=\"page\"");

WriteLiteral(">");

            
            #line 11 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
                                                      Write(node.Title);

            
            #line default
            #line hidden
WriteLiteral(" Details</li>\r\n");

            
            #line 12 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
    }
    else if (i > 1 && i < Model.SiteMapNodes.Count - 1)
    {
    continue;
    }
    else
    {

            
            #line default
            #line hidden
WriteLiteral("    <li");

WriteLiteral(" class=\"breadcrumb-item\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 583), Tuple.Create("\"", 599)
            
            #line 19 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
, Tuple.Create(Tuple.Create("", 590), Tuple.Create<System.Object, System.Int32>(node.Url
            
            #line default
            #line hidden
, 590), false)
);

WriteLiteral(" >");

            
            #line 19 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
                                                Write(node.Title);

            
            #line default
            #line hidden
WriteLiteral(" </a></li>\r\n");

            
            #line 20 "..\..MVC\Views\Breadcrumb\BreadcrumbInnerWithVirtualNodesFacilities.cshtml"
    }
    }

            
            #line default
            #line hidden
WriteLiteral("</ul>");

        }
    }
}
#pragma warning restore 1591