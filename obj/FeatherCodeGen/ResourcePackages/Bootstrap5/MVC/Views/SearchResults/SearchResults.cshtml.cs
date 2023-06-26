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

namespace SitefinityWebApp.ResourcePackages.Bootstrap5.MVC.Views.SearchResults
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
    
    #line 3 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
    
    #line default
    #line hidden
    
    #line 7 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Libraries.Model;
    
    #line default
    #line hidden
    
    #line 6 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Modules.Libraries;
    
    #line default
    #line hidden
    
    #line 5 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Modules.Pages;
    
    #line default
    #line hidden
    
    #line 8 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    using Telerik.Sitefinity.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/SearchResults/SearchResults.cshtml")]
    public partial class SearchResults : System.Web.Mvc.WebViewPage<Telerik.Sitefinity.Frontend.Search.Mvc.Models.ISearchResultsModel>
    {
        public SearchResults()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteAttribute("class", Tuple.Create(" class=\"", 350), Tuple.Create("\"", 374)
            
            #line 10 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create(" ", 358), Tuple.Create<System.Object, System.Int32>(Model.CssClass
            
            #line default
            #line hidden
, 359), false)
);

WriteLiteral(" id=\"sf-search-result-container\"");

WriteLiteral(">\r\n\r\n    <div");

WriteLiteral(" class=\"d-flex align-items-center justify-content-between my-3\"");

WriteLiteral(">\r\n");

            
            #line 13 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        
            
            #line default
            #line hidden
            
            #line 13 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
         if (@Model.Results.TotalCount > 0)
        {
            if (ViewBag.IsFilteredbyPermission)
            {

            
            #line default
            #line hidden
WriteLiteral("                <h1");

WriteLiteral(" role=\"alert\"");

WriteLiteral(" aria-live=\"assertive\"");

WriteLiteral(">");

            
            #line 17 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                  Write(Html.HtmlSanitize(Model.ResultText));

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");

            
            #line 18 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <h1");

WriteLiteral(" role=\"alert\"");

WriteLiteral(" aria-live=\"assertive\"");

WriteLiteral(">");

            
            #line 21 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                  Write(Model.Results.TotalCount);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 21 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                            Write(Html.HtmlSanitize(Model.ResultText));

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");

            
            #line 22 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
            }
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1");

WriteLiteral(" role=\"alert\"");

WriteLiteral(" aria-live=\"assertive\"");

WriteLiteral(">");

            
            #line 26 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                              Write(Html.Resource("No"));

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 26 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                   Write(Html.HtmlSanitize(Model.ResultText));

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");

            
            #line 27 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 28 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
         if (Model.AllowSorting && @Model.Results.TotalCount > 0)
        {

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"d-flex align-items-center gap-2\"");

WriteLiteral(" data-sf-hide-while-loading=\"true\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"form-label text-nowrap mb-0\"");

WriteAttribute("for", Tuple.Create(" for=\"", 1282), Tuple.Create("\"", 1316)
            
            #line 31 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1288), Tuple.Create<System.Object, System.Int32>(Html.UniqueId("searchSort")
            
            #line default
            #line hidden
, 1288), false)
);

WriteLiteral(">");

            
            #line 31 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                         Write(Html.Resource("SortBy"));

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n                <select");

WriteLiteral(" class=\"userSortDropdown form-select\"");

WriteAttribute("title", Tuple.Create(" title=\"", 1412), Tuple.Create("\"", 1450)
            
            #line 32 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1420), Tuple.Create<System.Object, System.Int32>(Html.Resource("SortDropdown")
            
            #line default
            #line hidden
, 1420), false)
);

WriteAttribute("id", Tuple.Create(" id=\"", 1451), Tuple.Create("\"", 1484)
            
            #line 32 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                        , Tuple.Create(Tuple.Create("", 1456), Tuple.Create<System.Object, System.Int32>(Html.UniqueId("searchSort")
            
            #line default
            #line hidden
, 1456), false)
);

WriteLiteral(">\r\n                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 1515), Tuple.Create("\"", 1548)
            
            #line 33 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1523), Tuple.Create<System.Object, System.Int32>(OrderByOptions.Relevance
            
            #line default
            #line hidden
, 1523), false)
);

WriteLiteral(" ");

            
            #line 33 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                          Write(Model.OrderBy == OrderByOptions.Relevance ? "selected=selected" : "");

            
            #line default
            #line hidden
WriteLiteral(">");

            
            #line 33 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                                 Write(Html.Resource("Relevance"));

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 1687), Tuple.Create("\"", 1717)
            
            #line 34 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1695), Tuple.Create<System.Object, System.Int32>(OrderByOptions.Newest
            
            #line default
            #line hidden
, 1695), false)
);

WriteLiteral(" ");

            
            #line 34 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                       Write(Model.OrderBy == OrderByOptions.Newest ? "selected=selected" : "");

            
            #line default
            #line hidden
WriteLiteral(">");

            
            #line 34 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                           Write(Html.Resource("NewestFirst"));

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n                    <option");

WriteAttribute("value", Tuple.Create(" value=\"", 1855), Tuple.Create("\"", 1885)
            
            #line 35 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1863), Tuple.Create<System.Object, System.Int32>(OrderByOptions.Oldest
            
            #line default
            #line hidden
, 1863), false)
);

WriteLiteral(" ");

            
            #line 35 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                       Write(Model.OrderBy == OrderByOptions.Oldest ? "selected=selected" : "");

            
            #line default
            #line hidden
WriteLiteral(">");

            
            #line 35 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                           Write(Html.Resource("OldestFirst"));

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n                </select>\r\n            </div>\r\n");

            
            #line 38 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n");

            
            #line 42 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    
            
            #line default
            #line hidden
            
            #line 42 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
     if (Model.Languages.Length > 1)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" data-sf-hide-while-loading=\"true\"");

WriteLiteral(">\r\n            <span>");

            
            #line 45 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
             Write(Html.Resource("ChangeResultsLanguageLabel"));

            
            #line default
            #line hidden
WriteLiteral(" </span>\r\n");

            
            #line 46 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
            
            
            #line default
            #line hidden
            
            #line 46 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
             for (var i = 0; i < Model.Languages.Length; i++)
            {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2332), Tuple.Create("\"", 2415)
            
            #line 48 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 2339), Tuple.Create<System.Object, System.Int32>(String.Format(ViewBag.LanguageSearchUrlTemplate, Model.Languages[i].Name)
            
            #line default
            #line hidden
, 2339), false)
);

WriteLiteral(">");

            
            #line 48 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                  Write(Model.Languages[i].DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 49 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                if (i < Model.Languages.Length - 2)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span>, </span>\r\n");

            
            #line 52 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                }
                else if (i == Model.Languages.Length - 2)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <span> ");

            
            #line 55 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                      Write(Html.Resource("OrLabel"));

            
            #line default
            #line hidden
WriteLiteral(" </span>\r\n");

            
            #line 56 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                }
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n");

            
            #line 59 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div");

WriteLiteral(" data-sf-hide-while-loading=\"true\"");

WriteLiteral(">\r\n\r\n");

            
            #line 63 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        
            
            #line default
            #line hidden
            
            #line 63 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
          
            LibrariesManager librariesManager = LibrariesManager.GetManager();
        
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 67 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        
            
            #line default
            #line hidden
            
            #line 67 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
         foreach (var item in Model.Results.Data)
        {
            var hasLink = item.GetValue("Link") != null && !String.IsNullOrEmpty(item.GetValue("Link").ToString());

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"d-flex gap-3 my-3\"");

WriteLiteral(">\r\n\r\n");

            
            #line 72 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                
            
            #line default
            #line hidden
            
            #line 72 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                 if (((Telerik.Sitefinity.Services.Search.Model.Document)item).ItemType.ToString() == typeof(Telerik.Sitefinity.Libraries.Model.Image).ToString())
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"flex-shrink-0\"");

WriteLiteral(">\r\n                        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 3417), Tuple.Create("\"", 3446)
            
            #line 75 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 3424), Tuple.Create<System.Object, System.Int32>(item.GetValue("Link")
            
            #line default
            #line hidden
, 3424), false)
);

WriteLiteral(">\r\n                            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 3482), Tuple.Create("\"", 3510)
            
            #line 76 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 3488), Tuple.Create<System.Object, System.Int32>(item.GetValue("Link")
            
            #line default
            #line hidden
, 3488), false)
);

WriteAttribute("alt", Tuple.Create(" alt=\"", 3511), Tuple.Create("\"", 3540)
            
            #line 76 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 3517), Tuple.Create<System.Object, System.Int32>(item.GetValue("Title")
            
            #line default
            #line hidden
, 3517), false)
);

WriteLiteral(" width=\"120\"");

WriteLiteral(" />\r\n                        </a>\r\n                    </div>\r\n");

            
            #line 79 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                }
                else if (((Telerik.Sitefinity.Services.Search.Model.Document)item).ItemType.ToString() == typeof(Telerik.Sitefinity.Libraries.Model.Video).ToString())
                {
                    var videoTmbId = new Guid((string)item.GetValue("Id"));
                    string thumbUrl = HyperLinkHelpers.GetVideoThumbnailUrl(librariesManager, videoTmbId);
                    if (!string.IsNullOrEmpty(thumbUrl))
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"flex-shrink-0\"");

WriteLiteral(">\r\n                            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 4171), Tuple.Create("\"", 4200)
            
            #line 87 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 4178), Tuple.Create<System.Object, System.Int32>(item.GetValue("Link")
            
            #line default
            #line hidden
, 4178), false)
);

WriteLiteral(" class=\"position-relative\"");

WriteLiteral(">\r\n                                <img");

WriteAttribute("src", Tuple.Create(" src=\"", 4266), Tuple.Create("\"", 4281)
            
            #line 88 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 4272), Tuple.Create<System.Object, System.Int32>(thumbUrl
            
            #line default
            #line hidden
, 4272), false)
);

WriteAttribute("alt", Tuple.Create(" alt=\"", 4282), Tuple.Create("\"", 4311)
            
            #line 88 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 4288), Tuple.Create<System.Object, System.Int32>(item.GetValue("Title")
            
            #line default
            #line hidden
, 4288), false)
);

WriteLiteral(" width=\"120\"");

WriteLiteral(" />\r\n\r\n                                <div");

WriteLiteral(" class=\"position-absolute top-50 start-50 translate-middle\"");

WriteLiteral(">\r\n                                    <svg");

WriteLiteral(" class=\"svg-inline--fa fa-w-18\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">\r\n                                        <use");

WriteAttribute("xlink:href", Tuple.Create(" xlink:href=\"", 4566), Tuple.Create("\"", 4643)
, Tuple.Create(Tuple.Create("", 4579), Tuple.Create<System.Object, System.Int32>(Href("~/ResourcePackages/Bootstrap5/assets/dist/sprites/solid.svg#play")
, 4579), false)
);

WriteLiteral(" class=\"fa-primary\"");

WriteLiteral("></use>\r\n                                    </svg>\r\n                            " +
"    </div>\r\n                            </a>\r\n                        </div>\r\n");

            
            #line 97 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                    }
                }

                else
                {

                }

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                <div");

WriteLiteral(" class=\"flex-grow-1\"");

WriteLiteral(">\r\n\r\n                    <h3");

WriteLiteral(" data-contenttype=\"");

            
            #line 108 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                     Write(item.GetValue("ContentType"));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n");

            
            #line 109 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 109 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                         if (hasLink)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 5148), Tuple.Create("\"", 5177)
            
            #line 111 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5155), Tuple.Create<System.Object, System.Int32>(item.GetValue("Link")
            
            #line default
            #line hidden
, 5155), false)
);

WriteLiteral(">");

            
            #line 111 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                        Write(item.GetValue("Title"));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 112 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                        }
                        else
                        {
                            
            
            #line default
            #line hidden
            
            #line 115 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                       Write(item.GetValue("Title"));

            
            #line default
            #line hidden
            
            #line 115 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                   
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </h3>\r\n\r\n                    <div>");

            
            #line 119 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                    Write(Html.HtmlSanitize((string)item.GetValue("HighLighterResult")));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 120 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 120 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                     if (hasLink)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <a");

WriteLiteral(" class=\"link-secondary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 5603), Tuple.Create("\"", 5632)
            
            #line 122 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5610), Tuple.Create<System.Object, System.Int32>(item.GetValue("Link")
            
            #line default
            #line hidden
, 5610), false)
);

WriteLiteral(">");

            
            #line 122 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                           Write(item.GetValue("Link"));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 123 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </div>\r\n            </div>\r\n");

            
            #line 126 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n");

            
            #line 130 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
    
            
            #line default
            #line hidden
            
            #line 130 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
     if (Model.DisplayMode == ListDisplayMode.Paging && Model.TotalPagesCount != null && Model.TotalPagesCount > 1)
    {
        if (ViewBag.IsFilteredbyPermission)
        {

            
            #line default
            #line hidden
WriteLiteral("            <ul");

WriteLiteral(" class=\"pagination\"");

WriteLiteral(" data-sf-hide-while-loading=\"true\"");

WriteLiteral(">\r\n");

            
            #line 135 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                
            
            #line default
            #line hidden
            
            #line 135 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                 if (Model.CurrentPage > 1)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <li");

WriteLiteral(" class=\"page-item\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"page-link\"");

WriteAttribute("href", Tuple.Create(" href=\'", 6159), Tuple.Create("\'", 6236)
            
            #line 138 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 6166), Tuple.Create<System.Object, System.Int32>(string.Format(ViewBag.RedirectPageUrlTemplate, Model.CurrentPage - 1)
            
            #line default
            #line hidden
, 6166), false)
);

WriteLiteral(">");

            
            #line 138 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                      Write(Html.Resource("Prev"));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                    </li>\r\n");

            
            #line 140 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                ");

            
            #line 141 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                 if (Model.CurrentPage < @Model.TotalPagesCount)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <li");

WriteLiteral(" class=\"page-item\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"page-link\"");

WriteAttribute("href", Tuple.Create(" href=\'", 6485), Tuple.Create("\'", 6562)
            
            #line 144 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 6492), Tuple.Create<System.Object, System.Int32>(string.Format(ViewBag.RedirectPageUrlTemplate, Model.CurrentPage + 1)
            
            #line default
            #line hidden
, 6492), false)
);

WriteLiteral(">");

            
            #line 144 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                      Write(Html.Resource("Next"));

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n                    </li>\r\n");

            
            #line 146 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </ul>\r\n");

            
            #line 148 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        }
        else
        {
            var numOfItems = (Model.CurrentPage == Model.TotalPagesCount) ? Html.Raw(Model.Results.TotalCount) : Html.Raw(Model.CurrentPage * Model.ItemsPerPage);

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"d-flex align-items-center justify-content-between\"");

WriteLiteral(" data-sf-hide-while-loading=\"true\"");

WriteLiteral(">\r\n                <div>\r\n");

WriteLiteral("                    ");

            
            #line 154 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
               Write(Html.Action("Index", "ContentPager", new
                    {
                    currentPage = Model.CurrentPage,
                    totalPagesCount = Model.TotalPagesCount,
                    redirectUrlTemplate = ViewBag.RedirectPageUrlTemplate
                    }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n\r\n                <div");

WriteLiteral(" class=\"mb-3\"");

WriteLiteral("><em");

WriteLiteral(" class=\"text-muted\"");

WriteLiteral(">");

            
            #line 162 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                     Write(Model.CurrentPage * Model.ItemsPerPage - Model.ItemsPerPage + 1);

            
            #line default
            #line hidden
WriteLiteral(" - ");

            
            #line 162 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                         Write(numOfItems);

            
            #line default
            #line hidden
WriteLiteral(" of ");

            
            #line 162 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
                                                                                                                                        Write(Model.Results.TotalCount);

            
            #line default
            #line hidden
WriteLiteral(" results</em></div>\r\n            </div>\r\n");

            
            #line 164 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
        }
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"searchResOrderBy\"");

WriteAttribute("value", Tuple.Create(" value=\'", 7601), Tuple.Create("\'", 7643)
            
            #line 167 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 7609), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("orderBy")
            
            #line default
            #line hidden
, 7609), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"searchResLanguage\"");

WriteAttribute("value", Tuple.Create(" value=\'", 7706), Tuple.Create("\'", 7749)
            
            #line 168 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 7714), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("language")
            
            #line default
            #line hidden
, 7714), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"searchResIndexCatalogue\"");

WriteAttribute("value", Tuple.Create(" value=\'", 7818), Tuple.Create("\'", 7867)
            
            #line 169 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 7826), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("indexCatalogue")
            
            #line default
            #line hidden
, 7826), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"searchResQuery\"");

WriteAttribute("value", Tuple.Create(" value=\'", 7927), Tuple.Create("\'", 7973)
            
            #line 170 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 7935), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("searchQuery")
            
            #line default
            #line hidden
, 7935), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"scoringInfo\"");

WriteAttribute("value", Tuple.Create(" value=\'", 8030), Tuple.Create("\'", 8076)
            
            #line 171 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 8038), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("scoringInfo")
            
            #line default
            #line hidden
, 8038), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"resultsForAllSites\"");

WriteAttribute("value", Tuple.Create(" value=\'", 8140), Tuple.Create("\'", 8193)
            
            #line 172 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 8148), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("resultsForAllSites")
            
            #line default
            #line hidden
, 8148), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"scoringInfo\"");

WriteAttribute("value", Tuple.Create(" value=\'", 8250), Tuple.Create("\'", 8296)
            
            #line 173 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 8258), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("scoringInfo")
            
            #line default
            #line hidden
, 8258), false)
);

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" data-sf-role=\"filterParameter\"");

WriteAttribute("value", Tuple.Create(" value=\'", 8357), Tuple.Create("\'", 8398)
            
            #line 174 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 8365), Tuple.Create<System.Object, System.Int32>(Request.QueryStringGet("filter")
            
            #line default
            #line hidden
, 8365), false)
);

WriteLiteral(" />\r\n\r\n</div>\r\n\r\n<div");

WriteLiteral(" id=\"sf-search-results-loading-indicator\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"d-flex justify-content-center my-5\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"spinner-border text-primary my-5\"");

WriteLiteral(" role=\"status\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"visually-hidden\"");

WriteLiteral(">Loading...</span>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");

            
            #line 186 "..\..MVC\Views\SearchResults\SearchResults.cshtml"
Write(Html.Script(Url.WidgetContent("Mvc/Scripts/SearchResults/Search-results.js"), "bottom", false));

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
