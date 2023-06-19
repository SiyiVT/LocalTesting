using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Libraries.Model;
using System.IO;
using Telerik.Sitefinity.Modules.Libraries;
using System.Text;
using System.Net;
using System.Net.Http;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using System.Globalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity;

using Newtonsoft.Json.Linq;


namespace SitefinityWebApp.Helpers
{
    public class ImageHelper
    {
        // NOTE: The imageExtension parameter must contain '.', for example '.jpeg'
        public static void CreateImageWithNativeAPI(Guid masterImageId, Guid parentAlbumId, string imageTitle, Stream imageStream, string imageFileName, string imageExtension)
        {
            //CultureInfo culture = CultureInfo.GetCultureInfo(targetCulture);
            foreach(var cultureName in IHHConstants.GleaglesSFCultures)
            {
                using (new CultureRegion(cultureName))
                {
                    LibrariesManager librariesManager = LibrariesManager.GetManager();
                    Image image = librariesManager.GetImages().FirstOrDefault(i => i.Id == masterImageId);
                    
                    if (image == null)
                    {
                        //The news item is created as a master. The newsId is assigned to the master.
                        image = librariesManager.CreateImage(masterImageId);
                        image.DateCreated = DateTime.UtcNow;

                        //Set the parent album.
                        Album album = librariesManager.GetAlbums().Where(i => i.Id == parentAlbumId).SingleOrDefault();
                        image.Parent = album;
                    }

                    //Set the properties of the news item.
                    image.Title = imageTitle;
                    image.DateCreated = DateTime.UtcNow;
                    image.PublicationDate = DateTime.UtcNow;
                    image.LastModified = DateTime.UtcNow;
                    image.UrlName = Regex.Replace(imageFileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                    image.MediaFileUrlName = Regex.Replace(imageFileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                    //Check for duplicate URLs
                    librariesManager.RecompileAndValidateUrls(image);

                    //Upload the image file.
                    librariesManager.Upload(image, imageStream, imageExtension);

                    //Set the WorkflowStatus and Publish
                    image.ApprovalWorkflowState.SetString(new CultureInfo(cultureName), "Published");
                    librariesManager.Lifecycle.Publish(image, new CultureInfo(cultureName));

                    //Save the changes.
                    librariesManager.SaveChanges();
                }
            }



            //LibrariesManager librariesManager = LibrariesManager.GetManager();
            //Image image = librariesManager.GetImages().Where(i => i.Id == masterImageId).FirstOrDefault();

            //if (image == null)
            //{
            //    foreach (var culture in IHHConstants.GleaglesSFCultures)
            //    {
            //        //The album post is created as master. The masterImageId is assigned to the master version.
            //        image = librariesManager.CreateImage(masterImageId);

            //        //Set the parent album.
            //        Album album = librariesManager.GetAlbums().Where(i => i.Id == parentAlbumId).SingleOrDefault();
            //        image.Parent = album;

            //        //Set the properties of the album post.
            //        image.Title = imageTitle;
            //        image.DateCreated = DateTime.UtcNow;
            //        image.PublicationDate = DateTime.UtcNow;
            //        image.LastModified = DateTime.UtcNow;
            //        image.UrlName = Regex.Replace(imageTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            //        image.MediaFileUrlName = Regex.Replace(imageFileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            //        //Upload the image file.
            //        // The imageExtension parameter must contain '.', for example '.jpeg'
            //        librariesManager.Upload(image, imageStream, imageExtension);

            //        //Save the changes.
            //        librariesManager.SaveChanges();

            //        //Publish the Albums item. The live version acquires new ID.
            //        var bag = new Dictionary<string, string>();
            //        bag.Add("ContentType", typeof(Image).FullName);
            //        bag.Add("Language", culture);
            //        WorkflowManager.MessageWorkflow(masterImageId, typeof(Image), null, "Publish", false, bag);

            //        Log.Write("Doctor Image Create");
            //    }

            //}
            //else
            //{
            //    Log.Write("Doctor Image Exist in Library");
            //}
        }

        public static void translateImage(JToken jsonObject)
        {
            Guid masterImageId = (Guid)jsonObject["ParentId"];
            Guid parentAlbumId = (Guid)jsonObject["ParentId"];
            string imageTitle = (string)jsonObject["Title"];
            Stream imageStream = GetImage(FrontendUrlHelper.GetBaseUri() + (string)jsonObject["ItemDefaultUrl"]);
            string imageFileName = (string)jsonObject["Title"];
            string imageExtension = (string)jsonObject["Extensio"];

            using (new CultureRegion("id"))
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager();
                Image image = librariesManager.GetImages().FirstOrDefault(i => i.Id == masterImageId);

                if (image == null)
                {
                    //The news item is created as a master. The newsId is assigned to the master.
                    image = librariesManager.CreateImage(masterImageId);
                    image.DateCreated = DateTime.UtcNow;

                    //Set the parent album.
                    Album album = librariesManager.GetAlbums().Where(i => i.Id == parentAlbumId).SingleOrDefault();
                    image.Parent = album;
                }

                //Set the properties of the news item.
                image.Title = imageTitle;
                image.DateCreated = DateTime.UtcNow;
                image.PublicationDate = DateTime.UtcNow;
                image.LastModified = DateTime.UtcNow;
                image.UrlName = Regex.Replace(imageFileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                image.MediaFileUrlName = Regex.Replace(imageFileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                //Check for duplicate URLs
                librariesManager.RecompileAndValidateUrls(image);

                //Upload the image file.
                librariesManager.Upload(image, imageStream, imageExtension);

                //Set the WorkflowStatus and Publish
                image.ApprovalWorkflowState.SetString(new CultureInfo("id"), "Published");
                librariesManager.Lifecycle.Publish(image, new CultureInfo("id"));

                //Save the changes.
                librariesManager.SaveChanges();
            }
            

        }
        public static Stream GetImage(string url)
        {
            if(url == null)
            {
                return null;
            }
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                Log.Write("Get Image Error: " + exp);
                buf = null;
            }

            return new MemoryStream(buf);
        }

    }
}