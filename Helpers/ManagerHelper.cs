using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity;
using System.Globalization;
using Telerik.Sitefinity.RelatedData;
using Telerik.OpenAccess;

namespace SitefinityWebApp.Helpers
{
    public class ManagerHelper
    {
        public static DynamicModuleManager GetModuleManager()
        {
            return DynamicModuleManager.GetManager("IHHDynamicModuleProvider");
        }

        public static DynamicModuleManager GetDefaultManager()
        {
            return DynamicModuleManager.GetManager("OpenAccessProvider");
        }

        public static void SaveDynamicContentChanges()
        {
            GetModuleManager().SaveChanges();
        }

        public static IQueryable<DynamicContent> GetDyanamicContents(string modeluName, string modelName)
        {

            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model." + modeluName + "." + modelName);

            var myCollection = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && w.Visible == true);
            return myCollection;
        }

        public static ILifecycleDataItem GetMasterItem(ILifecycleDataItem lifecycleData)
        {
            var librariesManager = LibrariesManager.GetManager();
            var master = librariesManager.Lifecycle.GetMaster(lifecycleData);

            return master;
        }

        public static ILifecycleDataItem GetDyanamicMasterItem(ILifecycleDataItem lifecycleData)
        {

            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            
            var master = dynamicModuleManager.Lifecycle.GetMaster(lifecycleData);

            return master;
        }

        public static Type GetResolvedType(string typeFullName)
        {
            return TypeResolutionService.ResolveType(typeFullName);
        }

        public static DynamicContent CreateDynamicContent(string typeFullName)
        {
            Type type = TypeResolutionService.ResolveType(typeFullName);
            DynamicContent createdItem = GetModuleManager().CreateDataItem(type);
            return createdItem;
        }

        public static IQueryable<DynamicContent> GetDyanamicContents(string fullTypeName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            
            Type type = TypeResolutionService.ResolveType(fullTypeName);

            var myCollection = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && w.Visible == true);
            return myCollection;
        }

       

        public static IQueryable<DynamicContent> GetMasterDyanamicContents(string fullTypeName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
           
            Type type = TypeResolutionService.ResolveType(fullTypeName);

            var myCollection = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            return myCollection;
        }
        public static IQueryable<DynamicContent> GetTempDyanamicContents(string fullTypeName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
          
            Type type = TypeResolutionService.ResolveType(fullTypeName);

            var myCollection = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Temp);
            return myCollection;
        }



        public static bool IsDynamicContentExist(string typeFullName, string urlName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();

            var myCollection = dynamicModuleManager.GetDataItems(GetResolvedType(typeFullName)).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            return (myCollection != null) ? true : false;
        }

        public static IQueryable<DynamicContent> GetHospitalDynamicContents()
        {

            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = TypeResolutionService.ResolveType(IHHConstants.HospitalTypeFullName);

            var myCollection = dynamicModuleManager.GetDataItems(type).
                Where(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                    w.Visible == true 
                    //&& !w.GetValue<Lstring>("UrlName").ToString().Contains("international")
                    );
            return myCollection;
        }

        public static string GetHospitalTitle()
        {
            string hosp_urlname = FrontendUrlHelper.GetHospitalOrSegmentFromUrl(IHHConstants.HospitalName);
            DynamicContent hosp = GetHospitalDynamicContents().Where(h => h.UrlName.Contains(hosp_urlname)).FirstOrDefault();

            if (hosp != null)
            {
                return hosp.GetString("Title");
            }
            //return HttpContext.GetGlobalResourceObject("_GleneaglesLabels", "GleneaglesHospital").ToString();
            return "IHH Healthcare Malaysia";
        }

        public static DynamicContent GetDynamicContentsByOriginalContentId(string modeluName, string modelName, string originalContentId)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model." + modeluName + "." + modelName);

            var item = dynamicModuleManager.GetDataItems(type).
                FirstOrDefault(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                    w.Visible == true &&
                    w.OriginalContentId.ToString() == originalContentId);
            return item;
        }

        public static DynamicContent GetDynamicContentsByOriginalContentId(string fullTypeName, Guid originalContentId)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            
            Type type = TypeResolutionService.ResolveType(fullTypeName);

            var item = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                w.Visible == true && w.OriginalContentId.Equals(originalContentId)).FirstOrDefault();
            return item;
        }


        public static DynamicContent GetDynamicContentsByUrlName(string typeFullName, string urlName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = GetResolvedType(typeFullName);

            var item = dynamicModuleManager.GetDataItems(type).
                FirstOrDefault(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                    w.Visible == true &&
                    w.UrlName.Contains(urlName));
            return item;
        }

        public static DynamicContent GetDynamicContentsByRandomUrl(string typeFullName, string randomUrl)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            
            Type type = TypeResolutionService.ResolveType(typeFullName);

            var item = dynamicModuleManager.GetDataItems(type).
                FirstOrDefault(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                    w.Visible == true &&
                    randomUrl.Contains(w.UrlName));
            return item;
        }

        public static DynamicContent GetMasterDynamicContentsByUrlName(string typeFullName, string urlName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = GetResolvedType(typeFullName);

            var item = dynamicModuleManager.GetDataItems(type).
                FirstOrDefault(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                    w.UrlName.Contains(urlName));
            return item;
        }

        public static DynamicContent GetMasterDynamicContentsByExactlyUrlName(string typeFullName, string urlName)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            Type type = GetResolvedType(typeFullName);

            var item = dynamicModuleManager.GetDataItems(type).
                FirstOrDefault(w =>
                    w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                    w.UrlName.Equals(urlName));
            return item;
        }

        public static List<ITaxon> GetTaxonomies(DynamicContent item, string fieldName)
        {
            List<ITaxon> taxonomies = new List<ITaxon>();
            if (item.GetValue<TrackedList<Guid>>(fieldName) != null && item.GetValue<TrackedList<Guid>>(fieldName).Count() > 0)
            {
                foreach (Guid id in item.GetValue<TrackedList<Guid>>(fieldName))
                {
                    taxonomies.Add(TaxonomyHelper.GetTaxon(id));
                }
            }
            return taxonomies;
        }

        public static IQueryable<FlatTaxon> GetTaxonomies(string taxonomyName)
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

            var taxonomies = taxonomyManager.GetTaxa<FlatTaxon>().Where(x => x.Taxonomy.Name == taxonomyName);

            return taxonomies;
        }

        public static IQueryable<DynamicContent> GetChildItems(DynamicContent parent, string childModuleName, string childtypeName)
        {
            Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model." + childModuleName + "." + childtypeName);
            IQueryable<DynamicContent> childItems = parent.GetChildItems(type);
            return childItems;
        }

        public static IQueryable<DynamicContent> GetChildItems(DynamicContent parent, string childTypeFullName)
        {
            Type type = TypeResolutionService.ResolveType(childTypeFullName);
            IQueryable<DynamicContent> childItems = parent.GetChildItems(type);
            return childItems;
        }

        public static string GetChildPluralTypeName(string modeluName, string modelName)
        {
            var typeName = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model." + modeluName + "." + modelName).Name;
            var pluralTypeName = PluralsResolver.Instance.ToPlural(typeName);
            return pluralTypeName;
        }

        public static string GetChildPluralTypeName(string typeFullName)
        {
            var typeName = TypeResolutionService.ResolveType(typeFullName).Name;
            var pluralTypeName = PluralsResolver.Instance.ToPlural(typeName);
            return pluralTypeName;
        }

        public static Document GetDocumentByOriginalId(string originalId)
        {
            var libManager = LibrariesManager.GetManager();
            var document = libManager.GetDocuments().Where(d => d.OriginalContentId.ToString() == originalId &&
                d.Visible).FirstOrDefault();

            return document;
        }

        public static IQueryable<Document> GetAllDocuments()
        {
            var libManager = LibrariesManager.GetManager();
            var documents = libManager.GetDocuments().Where(d => d.Visible);

            return documents;
        }


        public static void DeleteItem(string originalId)
        {
            var item = GetModuleManager().GetDataItems(GetResolvedType(IHHConstants.DoctorTypeFullName)).FirstOrDefault(i => i.OriginalContentId.ToString() == originalId);
            GetModuleManager().DeleteDataItem(item);

            // Commit the transaction in order for the items to be actually persisted to data store
            //TransactionManager.CommitTransaction(transactionName);
        }

        public static void DeleteItem(DynamicContent item)
        {
            GetModuleManager().DeleteDataItem(item);

            // Commit the transaction in order for the items to be actually persisted to data store
            TransactionManager.CommitTransaction("Delete Item " + item.GetType().FullName);
        }

        public static IQueryable<DynamicContent> GetAllItemWithId(string originalId)
        {
            return GetModuleManager().GetDataItems(GetResolvedType(IHHConstants.DoctorTypeFullName)).Where(i => i.OriginalContentId.ToString() == originalId);

        }


        public static decimal GetDecimalFromString(string stringNum)
        {
            return decimal.Parse(stringNum);
        }

        public static string Getsf_culture(string url)
        {
            string culturename = FrontendUrlHelper.GetParamsValueFromUrl(url, "sf_culture");
            return (!String.IsNullOrEmpty(culturename)) ? culturename : "en";
        }

        public static void ShowProgress(string taskName, decimal current, int total)
        {
            decimal percent = (current / total) * 100;
            string progress = string.Format("{0}/{1} ({2}%)", current, total, percent);
            Log.Write("[" + taskName + "] Current Progress : " + progress);

        }

        public static bool ItemIsMaster(DynamicContent item)
        {
            return item.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master;
        }

        public static Image GetImageById(string masterImageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Get the master version.
            return librariesManager.GetImages().Where(i => i.Id.ToString() == masterImageId).FirstOrDefault();
        }

        public static List<Image> GetAllImages()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Get the master version.
            return librariesManager.GetImages().ToList();
        }

        public static Image GetImageByTitle(string title)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Get the master version.
            return librariesManager.GetImages().Where(i => i.Title == title).FirstOrDefault();
        }

        public static void PublishItem(DynamicContent item)
        {
            var dynamicModuleManager = GetModuleManager();
            var masterItem = GetDyanamicMasterItem(item);

            item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo("en"));

            // We can now call the following to publish the item
            ILifecycleDataItem publishedAppointmentItem = dynamicModuleManager.Lifecycle.Publish(masterItem);

            // You need to set appropriate workflow status
            item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
        }

        public static List<DynamicContent> GetRelatedItems(DynamicContent item, string fieldName)
        {
            List<DynamicContent> relatedItems = new List<DynamicContent>();
            if (item.GetRelatedItems<DynamicContent>(fieldName) != null && item.GetRelatedItems<DynamicContent>(fieldName).Count() > 0)
            {
                relatedItems = item.GetRelatedItems<DynamicContent>(fieldName).ToList();
            }
            return relatedItems;
        }

        public static DynamicContent GetSpecificCultureItem(Guid originalContentId, string fullTypeName, string culture)
        {
            DynamicModuleManager dynamicModuleManager = GetModuleManager();
            if (fullTypeName.Contains("WhatsOn") || fullTypeName.Contains("Model.Careers"))
            {
                dynamicModuleManager = GetDefaultManager();
            }
            Type type = TypeResolutionService.ResolveType(fullTypeName);

            var item = dynamicModuleManager.GetDataItems(type).
                Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live &&
                w.Visible == true && w.PublishedTranslations.Contains(culture) && w.OriginalContentId.Equals(originalContentId)).FirstOrDefault();
            return item;
        }

        public static string GetHospitalLocation(string typeFullName, string urlName)
        {

            DynamicContent hospital = GetDynamicContentsByUrlName(typeFullName, urlName);

            if(hospital != null)
            {
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                var locations = hospital.GetValue<TrackedList<Guid>>("location");

                foreach (var loc in locations)
                {
                    // get the category name
                    return taxonomyManager.GetTaxon(loc).Title.ToString();
                }
            }
            
            return ""; 
        }

        public static string GetHospitalBrand(string typeFullName, string urlName)
        {

            DynamicContent hospital = GetDynamicContentsByUrlName(typeFullName, urlName);

            if (hospital != null)
            {
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                var brands = hospital.GetValue<TrackedList<Guid>>("brands");

                foreach (var b in brands)
                {
                    // get the category name
                    return taxonomyManager.GetTaxon(b).Title.ToString();
                }
            }

            return "";
        }
    }
}