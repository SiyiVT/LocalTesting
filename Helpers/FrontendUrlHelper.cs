using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Helpers
{
    public class FrontendUrlHelper
    {
        public static string GetLocalPrefix()
        {
            return CultureInfo.CurrentUICulture.Name == "en" ? "" : "/" + CultureInfo.CurrentUICulture;
        }

        public static string GetCDNHost()
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["HostCDN"]) ? "/" : ConfigurationManager.AppSettings["HostCDN"];
        }

        public static string GetBaseUri()
        {
            return ConfigurationManager.AppSettings["BaseUri"];
        }

        public static string GetCurrentPageAbsolutePath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

        public static string GetCurrentViewName()
        {
            return GetCleanUrl(SiteMapBase.GetActualCurrentNode().UrlName);
        }

        public static string GetCleanUrl(string url)
        {
            url = url.Replace("/", "");
            url = url.Replace("~", "");
            return url;
        }
        public static string GenerateSiteUrl(string urlname)
        {
            if (urlname.StartsWith("/"))
            {
                urlname = urlname.Substring(1);
            }
            return GetBaseUri() + urlname;
        }

        public static string GetParamsValueFromUrl(string url, string paramName)
        {
            Uri myUri = new Uri(url);
            string paramValue = HttpUtility.ParseQueryString(myUri.Query).Get(paramName);
            return paramValue;
        }


        public static string GetHospNameFromString(string hospUrl)
        {
            hospUrl = hospUrl.Replace(" ", "-");

            if (hospUrl.ToLower().Contains(IHHConstants.GKKItemUrlName))
            {
                return IHHConstants.GKKUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.GKLItemUrlName))
            {
                return IHHConstants.GKLUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.GMHItemUrlName))
            {
                return IHHConstants.GMHUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.GPGItemUrlName))
            {
                return IHHConstants.GPGUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.PHAKItemUrlName))
            {
                return IHHConstants.PHAKUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.PHKLItemUrlName))
            {
                return IHHConstants.PHKLUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.PHPItemUrlName))
            {
                return IHHConstants.PHPUrlName;
            }
            else if (hospUrl.ToLower().Contains(IHHConstants.PCMCItemUrlName))
            {
                return IHHConstants.PCMCUrlName;
            }
      
            return Guid.Empty.ToString();
        }

        public static string GetHospitalUrl(string inputText)
        {
            inputText = GetHospNameFromString(inputText);
            if (inputText != Guid.Empty.ToString())
            {
                return GetUrlWithoutHospitalName(inputText);
            }
            return GetBaseUri();
        }

        public static string GetUrlWithoutHospitalName(string lastSegment)
        {
            return string.Format("{0}/{1}", GetLocalPrefix(), lastSegment);
        }

        public static string GetHospitalOrSegmentFromUrl(string option)
        {
            var localPrefix = GetLocalPrefix();
            var urlSegment = localPrefix;

            var currentUrlPath = HttpContext.Current.Request.Url.AbsolutePath;

            var hosp = Guid.Empty.ToString();
            if (currentUrlPath.ToLower().Contains(IHHConstants.GKKUrlName))
            {
                hosp = IHHConstants.GKKItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.GKKUrlName;

            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.GKLUrlName))
            {
                hosp = IHHConstants.GKLItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.GKLUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.GMHUrlName))
            {
                hosp = IHHConstants.GMHItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.GMHUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.GPGUrlName))
            {
                hosp = IHHConstants.GPGItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.GPGUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.PHAKUrlName))
            {
                hosp = IHHConstants.PHAKItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.PHAKUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.PHKLUrlName))
            {
                hosp = IHHConstants.PHKLItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.PHKLUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.PHPUrlName))
            {
                hosp = IHHConstants.PHPItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.PHPUrlName;
            }
            else if (currentUrlPath.ToLower().Contains(IHHConstants.PCMCUrlName))
            {
                hosp = IHHConstants.PCMCItemUrlName;
                urlSegment = urlSegment + "/" + IHHConstants.PCMCUrlName;
            }

            if (option.Equals(IHHConstants.HospitalName))
            {
                return hosp;
            }
            else
            {
                return urlSegment;
            }
        }

        public static string GetLocationStateOrSegmentFromUrl(string option)
        {
            var localPrefix = GetLocalPrefix();
            var urlSegment = localPrefix;

            var currentUrlPath = HttpContext.Current.Request.Url.AbsolutePath;
            string[] excludedBrands = new string[] { "gleneagles/", "pantai/", "princecourt/" };

            var location = string.Empty;
            var url = currentUrlPath.ToLower();
            if (url.Contains(IHHConstants.SabahUrlName) && !excludedBrands.Any(c => url.Contains(c)))
            {
                location = "Sabah";
                urlSegment = urlSegment + "/" + IHHConstants.SabahUrlName;
            }
            else if (url.Contains(IHHConstants.KualaLumpurUrlName) && !excludedBrands.Any(c => url.Contains(c)))
            {
                location = "Kuala Lumpur";
                urlSegment = urlSegment + "/" + IHHConstants.KualaLumpurUrlName;
            }
            else if (url.Contains(IHHConstants.JohorUrlName) && !excludedBrands.Any(c => url.Contains(c)))
            {
                location = "Johor";
                urlSegment = urlSegment + "/" + IHHConstants.JohorUrlName;
            }
            else if (url.Contains(IHHConstants.PenangUrlName) && !excludedBrands.Any(c => url.Contains(c)))
            {
                location = "Penang";
                urlSegment = urlSegment + "/" + IHHConstants.PenangUrlName;
            }
            else if (url.Contains(IHHConstants.MalaccaUrlName) && !excludedBrands.Any(c => url.Contains(c)))
            {
                location = "Malacca";
                urlSegment = urlSegment + "/" + IHHConstants.MalaccaUrlName;
            }

            if (option.Equals(IHHConstants.Location))
            {
                return location;
            }
            else
            {
                return urlSegment;
            }
        }

        public static string GetBrandFromUrl(string option)
        {
            var localPrefix = GetLocalPrefix();
            var urlSegment = localPrefix;

            var currentUrlPath = HttpContext.Current.Request.Url.AbsolutePath;
            string[] Brands = new string[] { "Gleneagles", "Pantai", "Prince Court" };

            var brand = string.Empty;
            var url = currentUrlPath.ToLower();

            foreach (var b in Brands)
            {
                var urlBrand = b.ToLower().Replace(" ", "");
                if (url.Contains("/"+urlBrand))
                {
                    brand = b;
                    urlSegment = urlSegment +"/" + urlBrand;
                    break;
                }
            }

            if (option.Equals(IHHConstants.UrlSegment))
            {
                return urlSegment;
            }
            else
            {
                return brand;
            }

        }
        public static string GetSmartUrl(string lastSegment)
        {
            return string.Format("{0}/{1}", GetHospitalOrSegmentFromUrl(IHHConstants.UrlSegment), lastSegment);
        }
        public static string GetLocationHosSmartUrl(string lastSegment)
        {
            
            if(IsLocationStatePage())
            {
                return string.Format("{0}/{1}", GetLocationStateOrSegmentFromUrl(IHHConstants.UrlSegment), lastSegment);
            }

            return string.Format("{0}/{1}", GetHospitalOrSegmentFromUrl(IHHConstants.UrlSegment), lastSegment);
        }

        public static string GetLocationorBrandSmartUrl(string lastSegment)
        {
            if (!IsHospitalPage())
            {
                if(GetBrandFromUrl("brand") != string.Empty)
                {
                    return string.Format("{0}/{1}", GetBrandFromUrl(IHHConstants.UrlSegment), lastSegment);
                }
            }

            if (IsLocationStatePage())
            {
                return string.Format("{0}/{1}", GetLocationStateOrSegmentFromUrl(IHHConstants.UrlSegment), lastSegment);
            }
            return string.Format("{0}/{1}", GetHospitalOrSegmentFromUrl(IHHConstants.UrlSegment), lastSegment);
        }

        public static string GetSmartUrl(string hospitalName, string lastSegment)
        {
            return string.Format("{0}/{1}/{2}", GetLocalPrefix(), hospitalName, lastSegment);
        }

        //public static string GetIP()
        //{
        //    string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    var ips = ip.Split(',');
        //    Uri uri = new Uri("http://" + ips[0]);
        //    return uri.Host.ToString();
        //}
        public static string RemoveHTMLTag(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string TruncateText(string input, int max_leng, string pattern)
        {
            string txt = input;
            if (txt.Length > max_leng)
            {
                txt = txt.Substring(0, max_leng) + pattern;
            }
            return txt;
        }
        public static string GetHospitalPageSocialShareUrl()
        {

            string currentPath = GetCurrentPageAbsolutePath();
            //if (currentPath.Contains(GleneaglesConstants.GKKUrlName))
            //{
            //    return GetBaseUri() + "ResourcePackages/Gleneagles/assets/dist/images/social-share/gkk-logo-social-share.jpg";
            //}
            //else if (currentPath.Contains(GleneaglesConstants.GKLUrlName))
            //{
            //    return GetBaseUri() + "ResourcePackages/Gleneagles/assets/dist/images/social-share/gkl-logo-social-share.jpg";
            //}
            //else if (currentPath.Contains(GleneaglesConstants.GMHUrlName))
            //{
            //    return GetBaseUri() + "ResourcePackages/Gleneagles/assets/dist/images/social-share/gmh-logo-social-share.jpg";
            //}
            //else if (currentPath.Contains(GleneaglesConstants.GPGUrlName))
            //{
            //    return GetBaseUri() + "ResourcePackages/Gleneagles/assets/dist/images/social-share/gpg-logo-social-share.jpg";
            //}
            //else
            //{
            //    return GetBaseUri() + "ResourcePackages/Gleneagles/assets/dist/images/social-share/glen-logo-social-share.jpg";
            //}

            return GetBaseUri() + "ResourcePackages/IHH/assets/dist/images/header/ihh-logo.webp";
        }


       
        public static string GetSitefinityItemUrl(string typeFullName, string parentOriginalId, string itemOriginalId)
        {
            switch (typeFullName)
            {
                case IHHConstants.FormAppointmentTypeFullName:
                    return string.Format(
                       "{0}Sitefinity/adminapp/content/hospitals/{1}/appointments/{2}/edit?sf_provider=IHHDynamicModuleProvider&sf_culture=en",
                       GetBaseUri(), parentOriginalId, itemOriginalId);
                case IHHConstants.FormEnquiriesTypeFullName:
                    return string.Format(
                    "{0}Sitefinity/adminapp/content/hospitals/{1}/enquiries/{2}/edit?sf_provider=IHHDynamicModuleProvider&sf_culture=en",
                    GetBaseUri(), parentOriginalId, itemOriginalId);
                default:
                    return "#";
            }

        }

        public static bool IsHospitalPage()
        {
            return GetHospitalOrSegmentFromUrl(IHHConstants.HospitalName) != Guid.Empty.ToString();
        }

        public static bool IsLocationStatePage()
        {
            return GetLocationStateOrSegmentFromUrl(IHHConstants.Location) != string.Empty;
        }

        public static bool IsDetailPage()
        {
            return !GetCurrentPageAbsolutePath().EndsWith("/" + GetCurrentViewName());
        }

        public static string GetRedirectLink(string hosp, string original_url)
        {
            string url_segment = original_url;
            if (original_url.Contains("health-screening-centre"))
            {
                url_segment = String.Format("{0}/{1}", hosp, "health-screening-centre");
                return GenerateSiteUrl(url_segment);
            }
            else if (original_url.Contains("accident-emergency"))
            {
                url_segment = String.Format("{0}/{1}", hosp, "accident-emergency");
                return GenerateSiteUrl(url_segment);

            }
            return String.Empty;
        }

    }
}