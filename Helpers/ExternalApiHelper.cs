using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using System.Text;
using Newtonsoft.Json;

namespace SitefinityWebApp.Helpers
{
    public class ExternalApiHelper
    {
        public static JArray GetObjectsFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(url);
                
                    JObject jsonResult = JObject.Parse(json);
                    JArray objects = (JArray)jsonResult["value"];
                    return objects;
                }
                catch (Exception err)
                {
                    Log.Write(err);
                    return new JArray();
                }
               
            }
        }

        public static JObject GetSingleObjWithCulture(string url, string itemId, string culture)
        {
            string uriParamSign = (url.Contains("?")) ? "&" : "?";
            string newUrl = string.Format("{0}({1}){2}sf_culture={3}", url, itemId, uriParamSign, culture);
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(newUrl);
                    JObject jsonResult = JObject.Parse(json);
                    Log.Write("GetSingleObjWithCulture url=" + newUrl + " result =" + jsonResult);
                    return jsonResult;
                }
                catch (Exception err)
                {
                    Log.Write("GetSingleObjWithCulture url=" + newUrl + " Error " + err);
                    return new JObject();
                }

            }
        }

        public static JObject GetSingleDoctorObjWithCulture(string url, string itemId, string culture)
        {
            string newUrl = string.Format("{0}({1})?$expand=DoctorImage&sf_culture={2}", url, itemId, culture);
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(newUrl);
                    JObject jsonResult = JObject.Parse(json);
                    Log.Write("GetSingleDocObjWithCulture url=" + newUrl + " result =" + jsonResult);
                    return jsonResult;
                }
                catch (Exception err)
                {
                    Log.Write("GetSingleDocObjWithCulture url=" + newUrl + " Error " + err);
                    return new JObject();
                }

            }
        }

        public static JArray GetClinicHourObjectsFromPostUrl(int docId, int hosId )
        {
            using (WebClient wc = new WebClient())
            {
                string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/GetDoctorClinicHours";

                wc.BaseAddress = url;

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var newData = new PantaiDoctorClinicHour()
                {
                    DoctorID = docId, 
                    HospitalID = hosId
                };
                string data = JsonConvert.SerializeObject(newData);
                var response = wc.UploadString(url, data);

                JObject result = JObject.Parse(response);

                JArray objects = (JArray)result["d"]["results"];

                return objects;

            }
        }


        public static JArray GetDocObjectsFromPostUrl(string url, int hosId)
        {
            using (WebClient wc = new WebClient())
            {
                wc.BaseAddress = url;

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var newData = new PantaiDoctor()
                {
                    Skip = 0,
                    Take = 999,
                    AlphaKey = "0",
                    Hos = hosId,
                    Spec = 0,
                    docname = "",
                    cultureCode = "en-US",
                    category = 0
                };
                string data = JsonConvert.SerializeObject(newData);
                var response = wc.UploadString(url, data);

                JObject result = JObject.Parse(response);
                JArray objects = (JArray)result["d"]["results"];

                return objects;

            }
        }

        public static JObject GetSingleDocObjectsFromPostUrl(string url, int hosId, string docName , string cultureName)
        {
            using (WebClient wc = new WebClient())
            {
                wc.BaseAddress = url;

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var newData = new PantaiDoctor()
                {
                    Skip = 0,
                    Take = 1,
                    AlphaKey = "0",
                    Hos = hosId,
                    Spec = 0,
                    docname = docName,
                    cultureCode = cultureName,
                    category = 0
                };
                string data = JsonConvert.SerializeObject(newData);
                var response = wc.UploadString(url, data);

                JObject result = JObject.Parse(response);
                JArray objects = (JArray)result["d"]["results"];

                Log.Write((JObject)objects[0]);
                return (JObject)objects[0];

            }
        }

        public static JArray GetSpecialtyObjectsFromPostUrl(string url, int hosId)
        {
            using (WebClient wc = new WebClient())
            {
                wc.BaseAddress = url;

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                var newData = new PantaiSpecialty()
                {
                    hospitalId = hosId,
                    language = "\"en-US\"",
                };
                string data = JsonConvert.SerializeObject(newData);
                var response = wc.UploadString(url, data);

                JObject result = JObject.Parse(response);
                JArray objects = (JArray)result["d"]["results"];
                Log.Write(objects);
                return objects;

            }
        }



    }
}