using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Workflow;

namespace SitefinityWebApp.Helpers
{
    public class IDataEventHelper
    {
        public static void DataEventHandler(IDataEvent eventInfo)
        {
            var action = eventInfo.Action;
            var contentType = eventInfo.ItemType;
            var itemId = eventInfo.ItemId;
            var providerName = eventInfo.ProviderName;

            Log.Write("contentType : " + contentType + " action : " + action);

            if (contentType.ToString() == IHHConstants.ImageTypeFullName && action == "New")
            {
                //var manager = ManagerBase.GetMappedManager(contentType, providerName);
                //var item = manager.GetItemOrDefault(contentType, itemId);

                //var librariesManager = LibrariesManager.GetManager();
                //Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");
                //librariesManager.Provider.SuppressSecurityChecks = true;
                //var image = librariesManager.GetImages().Where(n => n.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && n.Id == itemId).FirstOrDefault();
                ////var image = ManagerHelper.GetImageById(itemId.ToString());
                //if (image != null && image.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master)
                //{

                //    foreach (var culture in GleneaglesConstants.GleaglesSFCultures)
                //    {
                //        using (new Telerik.Sitefinity.Localization.CultureRegion(new CultureInfo(culture)))
                //        {
                //            if (culture != "en" && !image.PublishedTranslations.Contains(culture))
                //            {

                //                var docTitle = image.Title.ToString();
                //                //fill in image properties
                //                image.Title = docTitle;
                //                image.UrlName = Regex.Replace(docTitle, @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                //                image.Parent = image.Parent;
                //                image.DateCreated = DateTime.UtcNow;
                //                image.PublicationDate = DateTime.UtcNow;
                //                image.LastModified = DateTime.UtcNow;

                //                librariesManager.RecompileAndValidateUrls(image);

                //                librariesManager.Lifecycle.Publish(image, new CultureInfo(culture));
                //                librariesManager.SaveChanges();

                //                //var checkedInImage = librariesManager.Lifecycle.CheckOut(image) as Image;
                //                //var checkInEventItem = librariesManager.Lifecycle.CheckIn(checkedInImage) as Image;

                //                //var temp = librariesManager.Lifecycle.CheckOut(image) as  Telerik.Sitefinity.Libraries.Model.Image;

                //                //var bag = new Dictionary<string, string>();
                //                //bag.Add("ContentType", image.GetType().FullName);
                //                //bag.Add("Language", new CultureInfo(culture).Name);
                //                //WorkflowManager.MessageWorkflow(temp.Id, temp.GetType(), null , "Publish", false, bag);

                //                //librariesManager.SaveChanges();

                //                Log.Write("Image " + culture + " translated");
                //            }
                //        }
                //    }
                //}

            }
            else if (contentType.ToString() == IHHConstants.PageNodeTypeFullName && action == "Updated")
            {
                StandardizePageUrl(contentType, providerName, itemId, action);
            }
        }


        private static void StandardizePageUrl(Type contentType, string providerName, Guid itemId, string action)
        {
            var pageManager = PageManager.GetManager();

            var manager = ManagerBase.GetMappedManager(contentType, providerName);
            var item = manager.GetItemOrDefault(contentType, itemId);

            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");

            // Get the pageNode
            var pageNode = pageManager
           .GetPageNodes()
           .FirstOrDefault(x => x.Id == itemId);

            string en_url = String.Empty;

            if (pageNode != null)
            {
                en_url = pageNode.UrlName;
                foreach (var culture in IHHConstants.IHHSFCultures)
                {
                    if (culture != "en" && pageNode.AvailableCultures.Contains(new CultureInfo(culture)))
                    {

                        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(culture);
                        pageNode.UrlName = en_url;
                        pageManager.SaveChanges();

                        Log.Write("Page url " + culture + " updated");

                    }

                }

            }

        }
    }
}