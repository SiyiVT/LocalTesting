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

namespace SitefinityWebApp.ResourcePackages.IHH.MVC.Views.Navigation
{
    using System;
    using System.Collections.Generic;
    
    #line 8 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 5 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    
    #line 3 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 6 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using SitefinityWebApp.Helpers;
    
    #line default
    #line hidden
    
    #line 7 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using Telerik.Sitefinity.DynamicModules.Model;
    
    #line default
    #line hidden
    
    #line 4 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 9 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using Telerik.Sitefinity.Model;
    
    #line default
    #line hidden
    
    #line 10 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
    using Telerik.Sitefinity.RelatedData;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/MVC/Views/Navigation/NavigationView.RightStickySideBar_GoTop.cshtml")]
    public partial class NavigationView_RightStickySideBar_GoTop : System.Web.Mvc.WebViewPage<Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.INavigationModel>
    {
        public NavigationView_RightStickySideBar_GoTop()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n");

            
            #line 13 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
  
    string cdnHost = FrontendUrlHelper.GetCDNHost();
    var localPrefix = FrontendUrlHelper.GetLocalPrefix();

    var currentUrlPath = HttpContext.Current.Request.Url.AbsolutePath;

    var urlSegment = FrontendUrlHelper.GetHospitalOrSegmentFromUrl(IHHConstants.UrlSegment);
    var hosp = FrontendUrlHelper.GetHospitalOrSegmentFromUrl(IHHConstants.HospitalName);

    var hospital = ManagerHelper.GetHospitalDynamicContents().Where(h => h.UrlName.Contains(hosp)).FirstOrDefault();

    var generalLine = "+622127899788";

    var whatsappNum = "https://wa.me/622127899788";

    //if (FrontendUrlHelper.IsHospitalPage())
    //{
    //    var generalLineItem = ManagerHelper.GetChildItems(hospital, "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Contactinfos").Where(x => x.UrlName.Contains("general-line")).FirstOrDefault();
    //    if (generalLine != null)
    //    {
    //        generalLine = generalLineItem.GetValue("CTALink").ToString();
    //    }

    //    var whatsappItem = ManagerHelper.GetChildItems(hospital, "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Contactinfos").Where(x => x.UrlName.Contains("whatsapp")).FirstOrDefault();
    //    if (whatsappItem != null)
    //    {
    //        whatsappNum = "https://wa.me/" + Regex.Replace(whatsappItem.GetValue("CTALink").ToString(), "[^0-9]", "");
    //    }
    //}

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"sidebar-default \"");

WriteLiteral(">\r\n\r\n    <div");

WriteLiteral(" class=\"d-flex flex-column h-100 align-items-center d-md-flex d-none\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"sidebar-items d-flex flex-column my-lg-auto mt-auto text-center\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2059), Tuple.Create("\"", 2111)
            
            #line 48 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
, Tuple.Create(Tuple.Create("", 2066), Tuple.Create<System.Object, System.Int32>(FrontendUrlHelper.GetSmartUrl("appointment")
            
            #line default
            #line hidden
, 2066), false)
);

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n                    <i");

WriteLiteral(" class=\"fa fa-calendar sidebar-item-icon\"");

WriteLiteral("></i>\r\n                    <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">");

            
            #line 50 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
                                              Write(Html.Resource("Appointment", "_IHHLabel"));

            
            #line default
            #line hidden
WriteLiteral("/ ");

            
            #line 50 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
                                                                                          Write(Html.Resource("Enquiry", "_IHHLabel"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </a>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2452), Tuple.Create("\"", 2471)
            
            #line 54 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
, Tuple.Create(Tuple.Create("", 2459), Tuple.Create<System.Object, System.Int32>(whatsappNum
            
            #line default
            #line hidden
, 2459), false)
);

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(">\r\n                    <i");

WriteLiteral(" class=\"fab fa-whatsapp sidebar-item-icon\"");

WriteLiteral("></i>\r\n                    <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">WhatsApp</div>\r\n                </a>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2755), Tuple.Create("\"", 2778)
, Tuple.Create(Tuple.Create("", 2762), Tuple.Create("tel:", 2762), true)
            
            #line 60 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
, Tuple.Create(Tuple.Create("", 2766), Tuple.Create<System.Object, System.Int32>(generalLine
            
            #line default
            #line hidden
, 2766), false)
);

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n                    <i");

WriteLiteral(" class=\"fas fa-phone sidebar-item-icon\"");

WriteLiteral("></i>\r\n                    <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">");

            
            #line 62 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
                                              Write(Html.Resource("AskADoctor", "_IHHLabel"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </a>\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 3078), Tuple.Create("\"", 3142)
            
            #line 67 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
, Tuple.Create(Tuple.Create("", 3085), Tuple.Create<System.Object, System.Int32>(FrontendUrlHelper.GetLocationHosSmartUrl("travel-guide")
            
            #line default
            #line hidden
, 3085), false)
);

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n                    <i");

WriteLiteral(" class=\"fas fa-plane sidebar-item-icon\"");

WriteLiteral("></i>\r\n                    <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">");

            
            #line 69 "..\..MVC\Views\Navigation\NavigationView.RightStickySideBar_GoTop.cshtml"
                                              Write(Html.Resource("TravelGuide", "_IHHLabel"));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </a>\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"sidebar-expand-button d-flex align-items-center justify-content-center\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fas fa-plus \"");

WriteLiteral("></i>\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n\r\n<!-- mobile sidebar -->\r\n<div");

WriteLiteral(" class=\"mobile-sidebar d-md-none d-flex\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" href=\"/appointment\"");

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fa fa-calendar sidebar-item-icon\"");

WriteLiteral("></i>\r\n            <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">Appointment/ Enquiry</div>\r\n        </a>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" href=\"https://wa.me/622127899788\"");

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fab fa-whatsapp sidebar-item-icon\"");

WriteLiteral("></i>\r\n            <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">WhatsApp</div>\r\n        </a>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" href=\"tel:+622127899788\"");

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fas fa-phone sidebar-item-icon\"");

WriteLiteral("></i>\r\n            <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">Ask A Doctor</div>\r\n        </a>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"sidebar-item\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" href=\"/travel-guide\"");

WriteLiteral(" class=\"sidebar-item-link\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fas fa-plane sidebar-item-icon\"");

WriteLiteral("></i>\r\n            <div");

WriteLiteral(" class=\"sidebar-item-text\"");

WriteLiteral(">Travel Guide</div>\r\n        </a>\r\n    </div>\r\n</div>\r\n\r\n<div");

WriteLiteral(" class=\"gotop-default\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"d-flex flex-column mt-auto align-items-center\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"gotop-button d-flex align-items-center justify-content-center\"");

WriteLiteral(">\r\n            <i");

WriteLiteral(" class=\"fa fa-chevron-up\"");

WriteLiteral("></i>\r\n        </div>\r\n    </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
