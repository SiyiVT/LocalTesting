using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;


namespace SitefinityWebApp.Helpers
{
    public class HtmlPageHelper
    {
        public static void AddDefaultOGImage(Page page)
        {
            var controls = page.Header.Controls.Cast<Control>();

            if (!controls.Any(c => c is LiteralControl && ((LiteralControl)c).Text.Contains("og:image")))
            {
                var meta = new System.Web.UI.HtmlControls.HtmlMeta();
                meta.Attributes.Add("property", "og:image");
                meta.Content = FrontendUrlHelper.GetHospitalPageSocialShareUrl();
                page.Header.Controls.Add(meta);

            }
        }

        public static void AddHospitalTitle(Page page)
        {
            string appendText = ManagerHelper.GetHospitalTitle();
            if (page != null && page.Title != null)
            {
                if (!page.Title.Contains(appendText))
                {
                    page.Title = String.Format("{0} | {1}", page.Title, ManagerHelper.GetHospitalTitle());
                }
            }
        }

        public static void AddCustomCanonicalTag(Page page)
        {

            var canonical = new System.Web.UI.HtmlControls.HtmlLink();
            canonical.Attributes.Add("rel", "canonical");
            canonical.Attributes.Add("href", FrontendUrlHelper.GenerateSiteUrl(HttpContext.Current.Request.RawUrl));

            page.Header.Controls.Add(canonical);
        }

        public static void UpdateCustomOGUrl(Page page)
        {
            var controls = page.Header.Controls.Cast<Control>();

            foreach (var control in controls)
            {
                if (control is LiteralControl && (((LiteralControl)control).Text.Contains("og:url")))
                {
                    string newUrl = FrontendUrlHelper.GenerateSiteUrl(HttpContext.Current.Request.RawUrl);
                    ((LiteralControl)control).Text = "<meta property=\"og:url\" content=\"" + newUrl + "\" />";
                    break;
                }
            }
        }
        //public static void AddCustomMetaDesc(Page page, string url_to_match, string templateKey, string dynamic_conetnt_type_full_name)
        //{
        //    if (FrontendUrlHelper.GetCurrentPageAbsolutePath().Contains(url_to_match))
        //    {
        //        var controls = page.Header.Controls.Cast<Control>();

        //        RemoveMetaTag(page, "og:description");
        //        RemoveMetaTag(page, "description");

        //        if (!FrontendUrlHelper.GetCurrentPageAbsolutePath().EndsWith(url_to_match))
        //        {
        //            if (!controls.Any(c => c is LiteralControl && ((LiteralControl)c).Text.Contains("description")))
        //            {
        //                string MetaDescTemplate = HttpContext.GetGlobalResourceObject("_GleneaglesLabels", templateKey).ToString();

        //                DynamicContent item = ManagerHelper.GetDynamicContentsByRandomUrl(dynamic_conetnt_type_full_name, FrontendUrlHelper.GetCurrentPageAbsolutePath());

        //                if (item != null)
        //                {
        //                    MetaDescTemplate = MetaDescTemplateTextMapping(MetaDescTemplate, item);
        //                    AddMetaTag(page, "property", "og:description", MetaDescTemplate);
        //                    AddMetaTag(page, "name", "description", MetaDescTemplate);

        //                }


        //            }
        //        }
        //        else
        //        {
        //            // item home page /listing page
        //            string MetaDescTemplate = GetHomeItemMetaDesc(url_to_match);

        //            string hospital_name = HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "GleneaglesHospital").ToString();

        //            if (FrontendUrlHelper.IsHospitalPage())
        //            {
        //                var hosp = FrontendUrlHelper.GetHospitalOrSegmentFromUrl(IHHConstants.HospitalName);
        //                DynamicContent hospital = ManagerHelper.GetDyanamicContents(IHHConstants.HospitalTypeFullName).ToList().Where(h => h.UrlName.Contains(hosp)).FirstOrDefault();

        //                if (hospital != null)
        //                {
        //                    hospital_name = hospital.GetString("Title");
        //                }

        //            }

        //            MetaDescTemplate = MetaDescTemplate.Replace("{|Hospital|}", hospital_name);

        //            AddMetaTag(page, "property", "og:description", MetaDescTemplate);
        //            AddMetaTag(page, "name", "description", MetaDescTemplate);
        //        }



        //    }

        //}

        private static void RemoveMetaTag(Page page, string text)
        {
            var controls = page.Header.Controls.Cast<Control>();

            var toDelete = controls.Where(c => c is LiteralControl && ((LiteralControl)c).Text.Contains(text)).FirstOrDefault();

            if (toDelete != null)
            {
                page.Header.Controls.Remove(toDelete);
            }

        }

        private static void AddMetaTag(Page page, string attribute_name, string attribute_name_value, string content_value)
        {
            var meta = new System.Web.UI.HtmlControls.HtmlMeta();
            meta.Attributes.Add(attribute_name, attribute_name_value);
            meta.Content = content_value;
            page.Header.Controls.Add(meta);
        }

        //private static string GetHomeItemMetaDesc(string url_segment)
        //{
        //    switch (url_segment)
        //    {
        //        case ("appointment"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "ApptHomeMetaDesTemplate").ToString();
        //        case ("doctors"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "DoctorHomeMetaDesTemplate").ToString();
        //        case ("careers"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "CareerHomeMetaDesTemplate").ToString();
        //        case ("packages-promotions"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "PackagesPromoHomeMetaDesc").ToString();
        //        case ("events"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "EventHomeMetaDesTemplate").ToString();
        //        case ("medical-specialties"):
        //            return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "MedicalSpecialtyHomeMetaDesTemplate").ToString();
        //        default:
        //            return String.Empty;
        //    }
        //}

        private static string MetaDescTemplateTextMapping(string template, DynamicContent item)
        {

            //default mapping 
            template = template.Replace("{|Title|}", item.GetString("Title"));

            var hospital = ManagerHelper.GetRelatedItems(item, "Hospital").FirstOrDefault();
            if (hospital != null)
            {
                template = template.Replace("{|Hospital|}", hospital.GetString("Title"));
            }
            else
            {
                var hosp = FrontendUrlHelper.GetHospitalOrSegmentFromUrl(IHHConstants.HospitalName);
                hospital = ManagerHelper.GetDyanamicContents(IHHConstants.HospitalTypeFullName).ToList().Where(h => h.UrlName.Contains(hosp)).FirstOrDefault();

                if (hospital != null)
                {
                    template = template.Replace("{|Hospital|}", hospital.GetString("Title"));
                }

            }

            //custom mapping
            switch (item.GetType().FullName)
            {
               
                case IHHConstants.DoctorTypeFullName:
                    template = template.Replace("{|DoctorName|}", String.Format("{0} {1}", item.GetString("Salutation"), item.GetString("Title")));

                    var specialty = ManagerHelper.GetRelatedItems(item, "MainSpecialties").FirstOrDefault();
                    if (specialty != null)
                    {
                        template = template.Replace("{|Specialty|}", specialty.GetString("Title"));
                    }
                    return template;
                default:

                    return template;

            }
        }
    }
}