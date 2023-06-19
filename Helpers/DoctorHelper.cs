using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using System.IO;
using System.Text.RegularExpressions;

using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using System.Collections;

namespace SitefinityWebApp.Helpers
{
    public class DoctorHelper
    {
        public static bool IsDoctorExist(string urlName)
        {
            var doctor = GetDoctorByUrlName(urlName);
            return (doctor == null) ? false : true;
        }

        public static DynamicContent GetDoctorByUrlName(string urlName)
        {
            return ManagerHelper.GetMasterDyanamicContents(IHHConstants.DoctorTypeFullName)
              .FirstOrDefault(f => f.UrlName.Equals(urlName));
        }

        public static DynamicContent GetDoctorByItemDefaultUrl(string randomUrl)
        {
            return ManagerHelper.GetDyanamicContents(IHHConstants.DoctorTypeFullName).ToList().Where(f => randomUrl.Contains(f.ItemDefaultUrl.ToString()))
              .FirstOrDefault();
        }

        public static void UpdateMetaData(DynamicContent doctor, string cultureName)
        {
            if (doctor != null)
            {
                string metaTitle = doctor.GetValue("Name").ToString();
                var hosp = doctor.GetRelatedItems<DynamicContent>("Hospital").FirstOrDefault();
                if (hosp != null)
                {
                    metaTitle = string.Format("{0} - {1}", metaTitle, hosp.GetValue("Title").ToString());
                }
                doctor.SetString("MetaTitle", metaTitle, cultureName);
            }
        }

        public static List<string> GetDuplicated()
        {
            var doctors = ManagerHelper.GetMasterDyanamicContents(IHHConstants.DoctorTypeFullName);
            var query = doctors.GroupBy(x => x.GetValue<Lstring>("Name").ToString())
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            return query;
        }

        public static void RemoveDuplicatedDoctor()
        {
            var providerName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Delete Duplicated Doctor";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

            var duplicatedNames = GetDuplicated();

            foreach (var title in duplicatedNames)
            {
                var item = ManagerHelper.GetMasterDyanamicContents(IHHConstants.DoctorTypeFullName)
                    .Where(d => d.GetValue<Lstring>("Title").ToString().Equals(title)).FirstOrDefault();
                // This is how you delete the doctorItem
                dynamicModuleManager.DeleteDataItem(item);

                // Commit the transaction in order for the items to be actually persisted to data store
                TransactionManager.CommitTransaction(transactionName);
            }
        }

        //public static void SetTitle()
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "PublishItems";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
        //    var items = dynamicModuleManager.GetDataItems(ManagerHelper.GetResolvedType(IHHConstants.DoctorOperationHourTypeFullName))
        //        .Where(d => d.Status == ContentLifecycleStatus.Master).ToList();

        //    decimal count = 0;
        //    foreach (var item in items)
        //    {
        //        try
        //        {

        //            //item.SetValue("Title", )

        //            // Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(item, true);

        //            // Commit the transaction in order for the items to be actually persisted to data store
        //            TransactionManager.CommitTransaction(transactionName);

        //            ManagerHelper.ShowProgress("SetTitle", count++, items.Count());
        //        }
        //        catch (Exception err)
        //        {
        //            Log.Write("Publish error " + err);
        //        }

        //    }
        //}


        //public static void PublishItems()
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "PublishItems";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
        //    var items = dynamicModuleManager.GetDataItems(ManagerHelper.GetResolvedType(IHHConstants.DoctorOperationHourTypeFullName))
        //        .Where(d => d.Status == ContentLifecycleStatus.Master).ToList();
        //    foreach (var item in items)
        //    {
        //        try
        //        {
        //            ILifecycleDataItem publishedDoctorItem = dynamicModuleManager.Lifecycle.Publish(item);

        //            // You need to set appropriate workflow status
        //            item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //            // Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(item, true);

        //            // Commit the transaction in order for the items to be actually persisted to data store
        //            TransactionManager.CommitTransaction(transactionName);

        //            Log.Write("Item ID :" + item.Id + " published");
        //        }
        //        catch (Exception err)
        //        {
        //            Log.Write("Publish error " + err);
        //        }

        //    }
        //}

        public static void UpdateMetaSetting(DynamicContent doctor)
        {
            
            String[] cultureName = doctor.AvailableLanguages;

            foreach (var culture in cultureName) {
                if (doctor.GetRelatedItemsCountByField("MainSpecialty") > 0 &&
                    doctor.GetValue("Name") != null &&
                    doctor.GetValue("Salutation") != null)
                {
                    var mainSpecialties = doctor.GetRelatedItems<DynamicContent>("MainSpecialty").FirstOrDefault();
                    string title = doctor.GetString("Name", culture);
                    string salutation = doctor.GetString("Salutation", culture);
                    if (mainSpecialties != null)
                    {
                        string metaTitle = string.Format("{0} {1} | {2} | {3}", salutation, title, mainSpecialties.GetString("Title", culture), "IHH Healthcare");

                        string metaDesciprtion = string.Format("{0} {1} specializes in {2} and works at {3} in Malaysia.", salutation, title, mainSpecialties.GetString("Title", culture), "IHH Healthcare");

                        doctor.SetString("MetaTitle", metaTitle, culture);
                        doctor.SetString("MetaDescription", metaDesciprtion, culture);
                        if (doctor.DoesFieldExist("OpenGraphTitle"))
                        {
                            doctor.SetString("OpenGraphTitle", metaTitle, culture);
                        }
                        if (doctor.DoesFieldExist("OpenGraphDescription"))
                        {
                            doctor.SetString("OpenGraphDescription", metaDesciprtion, culture);
                        }
                    }


                }

                if (doctor.GetRelatedItemsCountByField("DoctorImage") > 0 && doctor.DoesFieldExist("OpenGraphImage"))
                {
                    var image = doctor.GetRelatedItems<Image>("DoctorImage").FirstOrDefault();

                    if (image != null)
                    {
                        var masterImage = ManagerHelper.GetMasterItem(image);
                        if (masterImage != null)
                        {
                            doctor.CreateRelation(masterImage, "OpenGraphImage");

                        }
                    }
                }

                Log.Write("Update Meta Setting");
            }
                
        }

        public static void GenerateDetailPageUrl(DynamicContent doctor)
        {
            if (doctor != null)
            {
                string hosp_url = "";

                if (doctor.GetRelatedItems<DynamicContent>("Hospital") != null &&
                    doctor.GetRelatedItems<DynamicContent>("Hospital").ToList().Count() > 0)
                {
                    hosp_url = doctor.GetRelatedItems<DynamicContent>("Hospital").ToList().First().UrlName.ToString();
                    hosp_url = FrontendUrlHelper.GetHospitalUrl(hosp_url);
                    hosp_url = (hosp_url.StartsWith("/") ? hosp_url.Substring(1) : hosp_url);


                    string fullUrl = FrontendUrlHelper.GetBaseUri() + hosp_url + "/doctors" + doctor.ItemDefaultUrl.ToString();

                    if (doctor.DoesFieldExist("FullUrl"))
                    {
                        doctor.SetValue("FullUrl", fullUrl);
                    }
                }
            }
        }

        public static void SetLeaveTitle(DynamicContent leaveDate)
        {
            if (leaveDate != null)
            {
                if (leaveDate.GetValue<DateTime>("StartDate") != null && leaveDate.GetValue<DateTime>("EndDate") != null)
                {
                    string startDate = leaveDate.GetValue<DateTime>("StartDate").ToString("dd MMMM yyyy");
                    string endDate = leaveDate.GetValue<DateTime>("EndDate").ToString("dd MMMM yyyy");
                    string title = startDate + " " + endDate;
                    string randomUrl = "leave " + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");

                    leaveDate.SetValue("Title", title);
                    leaveDate.SetString("UrlName", randomUrl);
                }
            }
        }
       
        public static string GetOptionsValue(string d)
        {
            string value = "";
            switch (d)
            {
                case "Mon":
                case "1":
                    value = "1";
                    break;
                case "Tue":
                case "2":
                    value = "2";
                    break;
                case "Wed":
                case "4":
                    value = "3";
                    break;
                case "Thu":
                case "8":
                    value = "4";
                    break;
                case "Fri":
                case "16":
                    value = "5";
                    break;
                case "Sat":
                case "32":
                    value = "6";
                    break;
                case "Sun":
                case "64":
                    value = "7";
                    break;
                case "128":
                    value = "8";
                    break;
                case "256":
                    value = "9";
                    break;
                case "":
                    value = "1";
                    break;
                default:
                    value = "1";
                    break;
            }

            return value;
        }


        public static void UpdateDoctorDataRunTask(DynamicContent dynamicContentItem)
        {
            DynamicContent mainSpecialtyItem = dynamicContentItem.GetRelatedItems<DynamicContent>("MainSpecialty").FirstOrDefault();
            List<DynamicContent> subSpecialtyItem = dynamicContentItem.GetRelatedItems<DynamicContent>("SubSpecialty").ToList();
            //DynamicContent hospitalItem = dynamicContentItem.GetRelatedItems<DynamicContent>("Hospital").FirstOrDefault();

            //var en_UrlName = dynamicContentItem.GetString("Name", "en").ToString().ToLower().Replace(" ", "-");

            //var en_mainSpecialty_url = mainSpecialtyItem.GetString("UrlName", "en");
            //var new_en_UrlName = "/" + en_mainSpecialty_url + "/" + en_UrlName;

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

            String[] cultureName = dynamicContentItem.AvailableLanguages;

            foreach (var culture in cultureName)
            {
                var mainSpecialtyName = "";
                var subSpecialtyName = "";
                var HospitalName = "";
                var LocationState = "";
                var hospitalUrl = "";
                var brand = "";

                if (mainSpecialtyItem != null)
                {
                    if (!String.IsNullOrEmpty(mainSpecialtyItem.GetString("Title", culture)))
                    {
                        mainSpecialtyName = mainSpecialtyItem.GetString("Title", culture).ToString();
                    }
                }


                if (subSpecialtyItem != null && subSpecialtyItem.Count > 0)
                {
                    //if (!String.IsNullOrEmpty(subSpecialtyItem.GetString("Title", culture)))
                    //{
                    //    subSpecialtyName = subSpecialtyItem[0].GetString("Title", culture).ToString();
                    //}
                    foreach (var subspec in subSpecialtyItem)
                    {
                        subSpecialtyName = subSpecialtyName + ",, " + subspec.GetString("Title", culture);
                    }
                }

                var count = 0; 
                foreach (var hos in dynamicContentItem.GetValue<TrackedList<Guid>>("HospitalClassification"))
                {
                    hospitalUrl = taxonomyManager.GetTaxon(hos).UrlName;

                    DynamicContent hospitalItem = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);

                    if (hospitalItem != null)
                    {
                        if (!String.IsNullOrEmpty(hospitalItem.GetString("Title", culture)))
                        {
                            if (count == 0 && culture == "en")
                            {
                                var profileUrl = "/" + FrontendUrlHelper.GetHospNameFromString(hospitalUrl)+ "/doctors" + dynamicContentItem.ItemDefaultUrl;
                                var apptUrl = "/" + FrontendUrlHelper.GetHospNameFromString(hospitalUrl) + "/appointment" + dynamicContentItem.ItemDefaultUrl;

                                dynamicContentItem.SetValue("ProfileUrl", profileUrl);
                                dynamicContentItem.SetValue("AppointmentUrl", apptUrl);
                            }

                            count++;

                            HospitalName = HospitalName + "," + hospitalItem.GetString("Title", culture).ToString();

                            var locations = hospitalItem.GetValue<TrackedList<Guid>>("location");

                            foreach (var loc in locations)
                            {
                                // get the category name
                                LocationState = LocationState + "," + taxonomyManager.GetTaxon(loc).Title.ToString();
                            }

                            var brands = hospitalItem.GetValue<TrackedList<Guid>>("brands");

                            foreach (var b in brands)
                            {
                                // get the category name
                                brand = brand + "," + taxonomyManager.GetTaxon(b).Title.ToString();
                            }
                        }
                    }
                }

                char[] charsToTrim = { ' ', ',' };

                dynamicContentItem.SetString("MainSpecialtyName", mainSpecialtyName, culture);
                dynamicContentItem.SetString("SubSpecialtyName", subSpecialtyName.Trim(charsToTrim), culture);
                dynamicContentItem.SetString("HospitalName", HospitalName.Trim(charsToTrim), culture);
                dynamicContentItem.SetString("Location", LocationState.Trim(charsToTrim), culture);
                dynamicContentItem.SetString("Brand", brand.Trim(charsToTrim), culture);
                
            }      
        }

        public static void PatchDoctorDummy()
        {
            var ProviderName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Create Doctor Dummy";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
                
            DynamicContent doctorItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
            
            foreach (var cultureName in IHHConstants.IHHSFCultures)
            {
                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                // This is how values for the properties are set
                doctorItem.SetString("Name", "Yong SI YI", cultureName);
                doctorItem.SetString("Description", "Culture = " + cultureName, cultureName);

                if(cultureName == "en")
                {
                    var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, "pantai-hospital-kuala-lumpur");
                    if (hospitalItem != null)
                    {
                        //This is how we relate an item
                        RelationHelper.RemoveAndCreateRelation(doctorItem, "Hospital", hospitalItem);
                    }

                    // Get related item manager
                    var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, "gastroenterology");
                    if (mainSpecialtiesItem != null)
                    {
                        // This is how we relate an item
                        RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
                    }
                }

                doctorItem.SetString("UrlName", Guid.NewGuid().ToString(), cultureName);
                doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                doctorItem.SetValue("PublicationDate", DateTime.UtcNow);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                //// Create a version and commit the transaction in order changes to be persisted to data store
                versionManager.CreateVersion(doctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(doctorItem);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                // Create a version
                versionManager.CreateVersion(doctorItem, true);
                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                versionManager.CreateVersion(checkInDoctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                versionManager.CreateVersion(checkInDoctorItem, true);

                TransactionManager.CommitTransaction(transactionName);

            }

        }

        public static void PatchDoctor2(JToken jsonObject)
        {
            var ProviderName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Create Doctor Test";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
            DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)jsonObject["UrlName"]);
            if (doctorItem == null)
            {
                doctorItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
            }
            else
            {
                //add hospital name classification
                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);
                if (HospitalNameClassification != null)
                {
                    var childItems = doctorItem.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                    DynamicContent childItem = null;

                    foreach (var child in childItems)
                    {
                        if (child.GetString("UrlName") == HospitalNameClassification.UrlName)
                        {
                            childItem = child;
                        }
                    }

                    if (childItem == null)
                    {
                        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");

                        TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                        var hospitals = doctorItem.GetValue<TrackedList<Guid>>("HospitalClassification");

                        bool hospitalClassifictionIsExist = false;

                        foreach (var hos in hospitals)
                        {
                            if(taxonomyManager.GetTaxon(hos).UrlName == HospitalNameClassification.UrlName)
                            {
                                hospitalClassifictionIsExist = true;
                            }
                        }

                        if(!hospitalClassifictionIsExist)
                        {
                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo("en"));

                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(doctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(doctorItem);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                            // Create a version
                            versionManager.CreateVersion(doctorItem, true);

                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                            ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                            versionManager.CreateVersion(checkInDoctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                            versionManager.CreateVersion(checkInDoctorItem, true);

                            TransactionManager.CommitTransaction(transactionName);
                        }
                       
                    }
                }

                return;
            }

            //multilanguage
            foreach (var cultureName in IHHConstants.IHHSFCultures)
            {
                string url = "https://gleneagles.com.my/api/default/doctors"; // 

                var item = ExternalApiHelper.GetSingleDoctorObjWithCulture(url, (string)jsonObject["Id"], cultureName);

                if ((string)item["Title"] == null || (string)item["Title"] == "")
                {
                    Log.Write("Docotr Dont have " + cultureName + " Translate");
                    continue;
                }

                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                // This is how values for the properties are set
                doctorItem.SetString("Name", (string)item["Title"], cultureName);
                doctorItem.SetString("Description", (string)item["Description"], cultureName);
                doctorItem.SetString("Salutation", (string)item["Salutation"], cultureName);
                //doctorItem.SetValue("PhoneNo", (string)item["PhoneNo"]);
                doctorItem.SetString("LanguageSpoken", (string)item["LanguageSpoken"], cultureName);
                if ((string)item["YearsOfExperience"] != null)
                {
                    doctorItem.SetValue("YearsOfExperience", decimal.Parse((string)item["YearsOfExperience"]));
                }

                if ((string)item["SortNumber"] != null)
                {
                    doctorItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
                }

                doctorItem.SetString("QualificationAndCertifications", (string)item["QualificationAndCertifications"], cultureName);

                if (cultureName == "en")
                {
                    if (jsonObject["Hospital"].Count() > 0)
                    {
                        //add hospital name classification
                        var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);

                        if (HospitalNameClassification != null)
                        {
                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                        }
                    }

                    if (jsonObject["MainSpecialties"].Count() > 0)
                    {
                        // Get related item manager
                        var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["MainSpecialties"][0]["UrlName"]);
                        if (mainSpecialtiesItem != null)
                        {
                            // This is how we relate an item
                            RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
                        }
                        else
                        {
                            Log.Write("Main Spec is null ");
                        }

                    }

                    if (jsonObject["SubSpecialties"].Count() > 0)
                    {
                        RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialties");

                        for (int index = 0; index < jsonObject["SubSpecialties"].Count(); index++)
                        {
                            // Get related item manager
                            var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["SubSpecialties"][index]["UrlName"]);
                            if (subSpecialtiesItem != null)
                            {
                                // This is how we relate an item
                                RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

                            }
                            else
                            {
                                Log.Write("sub Spec is null ");
                            }
                        }

                    }


                    if ((string)item["DoctorImage"][0]["Url"] != null)
                    {

                        LibrariesManager doctorImageManager = LibrariesManager.GetManager();
                        var doctorImgUrlName = (string)item["UrlName"];
                        doctorImgUrlName = Regex.Replace(doctorImgUrlName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                        var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                           i.UrlName.Contains(doctorImgUrlName));

                        if (ImageItem == null)
                        {
                            var doctorImage = ImageHelper.GetImage((string)item["DoctorImage"][0]["Url"]);
                            ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);

                        }
                        //// Get related item manager

                        var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                            i.UrlName.Contains(doctorImgUrlName));

                        if (doctorImageItem != null)
                        {
                            // This is how we relate an item
                            doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
                        }
                        else
                        {
                            Log.Write("Doctor image not found");
                        }
                    }
                }

                doctorItem.SetString("UrlName", (string)jsonObject["UrlName"], cultureName);
                doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                doctorItem.SetValue("PublicationDate", DateTime.UtcNow);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                //// Create a version and commit the transaction in order changes to be persisted to data store
                versionManager.CreateVersion(doctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(doctorItem);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                // Create a version
                versionManager.CreateVersion(doctorItem, true);

                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                versionManager.CreateVersion(checkInDoctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                versionManager.CreateVersion(checkInDoctorItem, true);

                TransactionManager.CommitTransaction(transactionName);
            }
        }

        public static void PatchDoctorHopital(JToken jsonObject, DynamicContent parent)
        {
            var providerName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Patch Doctor Hospital";
            var versionManager = VersionManager.GetManager(null, transactionName);
            
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

            if (parent != null)
            {
                if (jsonObject["Hospital"].Count() == 0)
                {
                    return;
                }

                //add hospital name classification
                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);

                var childItems = parent.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                DynamicContent childItem = null;

                foreach(var child in childItems)
                {
                    if(child.GetString("UrlName") == HospitalNameClassification.UrlName)
                    {
                        childItem = child;
                    }
                }
               
                if (childItem == null)
                {
                    childItem = dynamicModuleManager.CreateDataItem(
                        ManagerHelper.GetResolvedType(IHHConstants.DoctorHospitalTypeFullName));
                }
                else
                {
                    Log.Write("Hospital -- Skip!");

                    string clinicHourUrl = "https://gleneagles.com.my/api/default/clinichours?$filter=contains(ItemDefaultUrl,%20%27" + jsonObject["UrlName"] + "%27)";

                    JArray clinicHourItems = ExternalApiHelper.GetObjectsFromUrl(clinicHourUrl);

                    if (clinicHourItems != null)
                    {
                        foreach (var clinicHour in clinicHourItems)
                        {
                            PatchClinicHourGE_2(clinicHour, childItem);
                            Log.Write("Patch doctor child item : " + (string)clinicHour["ItemDefaultUrl"]);
                        }
                    }

                    return;
                }

                foreach (var cultureName in IHHConstants.IHHSFCultures)
                {
                    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                    childItem.SetString("Title", HospitalNameClassification.Title, cultureName);
                    childItem.SetValue("ContactNumber", (string)jsonObject["PhoneNo"]);
                    if (cultureName == "en")
                    {
                        if (jsonObject["Hospital"].Count() > 0)
                        {
                            //add hospital name classification
                            if (HospitalNameClassification != null)
                            {
                                childItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                            }
                        }

                        if ((string)jsonObject["ResidencyStatus"] != "")
                        {
                            var residencyStatus = "";
                            switch ((string)jsonObject["ResidencyStatus"])
                            {
                                case "1":
                                    residencyStatus = "resident";
                                    break;
                                case "2":
                                    residencyStatus = "sessional";
                                    break;
                                case "4":
                                    residencyStatus = "visiting";
                                    break;
                                default:
                                    residencyStatus = "";
                                    break;
                            }

                            if (residencyStatus != "")
                            {
                                var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", residencyStatus);

                                if (ResidencyStatusClassification != null)
                                {
                                    childItem.Organizer.AddTaxa("residencystatus", ResidencyStatusClassification.Id);
                                }
                            }

                        }
                    }

                    childItem.SetString("UrlName", HospitalNameClassification.UrlName, cultureName);
                    childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                    childItem.SetValue("PublicationDate", DateTime.UtcNow);
                    childItem.SetValue("IncludeInSitemap", false);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                    //set parent
                    Type doctorsType = TypeResolutionService.ResolveType(IHHConstants.DoctorTypeFullName);
                    childItem.SetParent(parent.Id, doctorsType.FullName);

                    //// Create a version and commit the transaction in order changes to be persisted to data store
                    versionManager.CreateVersion(childItem, false);
                    dynamicModuleManager.Lifecycle.Publish(childItem);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                    // Create a version
                    versionManager.CreateVersion(childItem, true);
                    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                    DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                    ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                    versionManager.CreateVersion(checkInClinicHourItem, false);
                    dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                    versionManager.CreateVersion(checkInClinicHourItem, true);

                    TransactionManager.CommitTransaction(transactionName);

                    if(cultureName == "en")
                    {
                        string clinicHourUrl = "https://gleneagles.com.my/api/default/clinichours?$filter=contains(ItemDefaultUrl,%20%27" + jsonObject["UrlName"] + "%27)";

                        JArray clinicHourItems = ExternalApiHelper.GetObjectsFromUrl(clinicHourUrl);

                        if (clinicHourItems != null)
                        {
                            foreach (var clinicHour in clinicHourItems)
                            {
                                PatchClinicHourGE_2(clinicHour, childItem);
                                Log.Write("Patch doctor child item : " + (string)clinicHour["ItemDefaultUrl"]);
                            }
                        }
                    }
                }

            }
            else
            {
                Log.Write("Clinic Hour Parent is null");
            }
        }

        public static void PatchClinicHourGE_2(JToken jsonObjects, DynamicContent parent)
        {
            if (parent != null)
            {
                var url = "https://gleneagles.com.my/api/default/clinichours";
                var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], "en");

                if ((string)item["Title"] == null || (string)item["Title"] == "")
                {
                    return;
                }

                string opdUrl = "https://gleneagles.com.my/api/default/opddays?$filter=ParentID%20eq%20" + (string)jsonObjects["Id"];

                var opdItems = ExternalApiHelper.GetObjectsFromUrl(opdUrl);

                List<string> availableList = new List<string>();
                Dictionary<string, List<string>> dayList = new Dictionary<string, List<string>>();
                Dictionary<string, Dictionary<string, string>> OtherAvailabilities = new Dictionary<string, Dictionary<string, string>>();

                if (opdItems != null)
                {
                    foreach (var opd in opdItems)
                    {
                        var opdurl = "https://gleneagles.com.my/api/default/opddays";
                        var opdItem = ExternalApiHelper.GetSingleObjWithCulture(opdurl, (string)opd["Id"], "en");

                        var availability = GetOptionsValue((string)opdItem["Availability"]);
                        
                        switch (availability)
                        {
                            case "1":
                                availability = "2";
                                break;
                            case "2":
                                availability = "3";
                                break;
                            case "3":
                                availability = "4";
                                break;
                            case "4":
                                availability = "5";
                                break;
                            case "5":
                                availability = "6";
                                break;
                            case "6":
                                availability = "7";
                                break;
                            case "7":
                                availability = "8";
                                break;
                            case "8":
                                availability = "1";
                                break;
                            case "9":
                                availability = "9";
                                break;
                            default:
                                availability = "1";
                                break;
                        }

                        if (!availableList.Contains(availability))
                        {
                            availableList.Add(availability);
                            dayList[availability] = new List<string>();
                            OtherAvailabilities[availability] = new Dictionary<string, string>();
                        }

                        dayList[availability].Add(GetOptionsValue((string)opdItem["Day"]));
                        OtherAvailabilities[availability]["en"] = (string)opdItem["OtherAvailabilities"];

                        var opdItemID = ExternalApiHelper.GetSingleObjWithCulture(opdurl, (string)opd["Id"], "id");
                        if(opdItemID != null)
                        {
                            OtherAvailabilities[availability]["id"] = (string)opdItem["OtherAvailabilities"];
                        }
                    }
                }

                foreach (var available in availableList)
                {
                    var providerName = IHHConstants.IHHDynamicModuleProvider;
                    var transactionName = "Patch Clinic Hour GE";
                    var versionManager = VersionManager.GetManager(null, transactionName);
                    
                    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

                    DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
                       IHHConstants.DoctorClinicHourTypeFullName, (string)jsonObjects["Id"] + "-" + available);

                    if (childItem == null)
                    {
                        childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));
                    }
                    else
                    {
                        return;
                    }

                    foreach (var cultureName in IHHConstants.IHHSFCultures)
                    {
                        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);
                        
                        List<string> days = dayList[available];

                        string[] opdDay = days.ToArray();
                        Log.Write(opdDay.ToString());
                        var title = "";
                        switch(available)
                        {
                            case "1":
                                title = (string)item["Title"];
                                break;
                            case "2":
                                title = (string)item["Title"] + " - By Appointment";
                                break;
                            default:
                                title = (string)item["Title"] + " - Others";
                                break;
                        }

                        childItem.SetString("Title", title, cultureName);
                        childItem.SetValue("StartTime", (string)item["StartTime"]);
                        childItem.SetValue("EndTime", (string)item["EndTime"]);
                        childItem.SetValue("Days", opdDay);
                        childItem.SetValue("Availability", available);
                        childItem.SetString("OtherAvailabilities", OtherAvailabilities[available][cultureName], cultureName);

                        childItem.SetString("UrlName", (string)item["Id"] + "-" + available);
                        childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                        childItem.SetValue("PublicationDate", DateTime.UtcNow);
                        childItem.SetValue("IncludeInSitemap", false);

                        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                        //set parent
                        Type OtherdoctorHospitalType = TypeResolutionService.ResolveType(IHHConstants.DoctorHospitalTypeFullName);
                        childItem.SetParent(parent.Id, OtherdoctorHospitalType.FullName);

                        //// Create a version and commit the transaction in order changes to be persisted to data store
                        versionManager.CreateVersion(childItem, false);
                        dynamicModuleManager.Lifecycle.Publish(childItem);

                        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                        // Create a version
                        versionManager.CreateVersion(childItem, true);
                        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                        DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                        ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                        versionManager.CreateVersion(checkInClinicHourItem, false);
                        dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                        versionManager.CreateVersion(checkInClinicHourItem, true);
                        TransactionManager.CommitTransaction(transactionName);
                    }

                }

            }
            else
            {
                Log.Write("Clinic Hour Parent is null");
            }
        }


        public static void PatchDoctorPantai2(JToken jsonObject, int hosId)
        {
            var ProviderName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Create Doctor";
            var versionManager = VersionManager.GetManager(null, transactionName);

            string docFullUrl = (string)jsonObject["profileUrl"];

            string docUrl = docFullUrl.Split('/').Last();


            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
            DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(
                          IHHConstants.DoctorTypeFullName, docUrl);
            if (doctorItem == null)
            {
                doctorItem = dynamicModuleManager.CreateDataItem(
                    ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
            }
            else
            {
                var hospitalUrl = "";

                switch (hosId)
                {
                    case 2:
                        hospitalUrl = "pantai-hospital-ayer-keroh";
                        break;
                    case 7:
                        hospitalUrl = "pantai-hospital-kuala-lumpur";
                        break;
                    case 9:
                        hospitalUrl = "pantai-hospital-penang";
                        break;
                    default:
                        Log.Write("HospitalId = " + hosId);
                        break;
                }

                var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);
                if (hospitalItem != null)
                {
                    var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", hospitalUrl);
                    
                    if (HospitalNameClassification != null)
                    {
                        var childItems = doctorItem.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                        DynamicContent childItem = null;

                        foreach (var child in childItems)
                        {
                            if (child.GetString("UrlName") == HospitalNameClassification.UrlName)
                            {
                                childItem = child;
                            }
                        }

                        if (childItem == null)
                        {
                            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");

                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo("en"));

                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(doctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(doctorItem);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                            // Create a version
                            versionManager.CreateVersion(doctorItem, true);

                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                            ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                            versionManager.CreateVersion(checkInDoctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                            versionManager.CreateVersion(checkInDoctorItem, true);

                            TransactionManager.CommitTransaction(transactionName);
                        }
                    }
                }

                return;
            }

            //multilanguage
            foreach (var cultureName in IHHConstants.IHHSFCultures)
            {
                var pantaiCultureName = "";

                switch (cultureName)
                {
                    case "en":
                        pantaiCultureName = "en-US";
                        break;
                    case "id":
                        pantaiCultureName = "id-ID";
                        break;
                    default:
                        pantaiCultureName = "en-US";
                        break;
                }

                string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";

                var item = ExternalApiHelper.GetSingleDocObjectsFromPostUrl(url, hosId, (string)jsonObject["full_name"], pantaiCultureName);

                if ((string)item["display_name"] == null || (string)item["display_name"] == "")
                {
                    Log.Write("Docotr Dont have " + cultureName + " Translate");
                    continue;
                }

                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                // This is how values for the properties are set
                doctorItem.SetString("Name", (string)item["full_name"], cultureName);
                //doctorItem.SetString("Description", (string)item["Description"], cultureName);
                //doctorItem.SetValue("DisableRequestForAppointment", false);
                doctorItem.SetString("Salutation", (string)item["salutation"], cultureName);
                //doctorItem.SetString("DoctorStatus", (string)item["DoctorStatus"], cultureName);
                //doctorItem.SetValue("PhoneNo", (string)item["PhoneNo"]);
                doctorItem.SetString("LanguageSpoken", (string)item["spoken_lang"], cultureName);
                doctorItem.SetString("MMC", (string)item["mmc"], cultureName);
                //if ((string)item["YearsOfExperience"] != null)
                //{
                //    doctorItem.SetValue("YearsOfExperience", decimal.Parse((string)item["YearsOfExperience"]));
                //}

                //if ((string)item["SortNumber"] != null)
                //{
                //    doctorItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
                //}

                //doctorItem.SetValue("DisableRequestForAppointment", false);

                doctorItem.SetString("QualificationAndCertifications", (string)item["qualification"], cultureName);
                //doctorItem.SetString("ProceduresPerformed", (string)item["ProceduresPerformed"], cultureName);
                //doctorItem.SetString("ConditionsTreated", (string)item["ConditionsTreated"], cultureName);

                if (cultureName == "en")
                {
                    var hospitalUrl = "";

                    switch (hosId)
                    {
                        case 2:
                            hospitalUrl = "pantai-hospital-ayer-keroh";
                            break;
                        case 7:
                            hospitalUrl = "pantai-hospital-kuala-lumpur";
                            break;
                        case 9:
                            hospitalUrl = "pantai-hospital-penang";
                            break;
                        default:
                            Log.Write("HospitalId = " + hosId);
                            break;
                    }

                    var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);
                    if (hospitalItem != null)
                    {
                        ////This is how we relate an item
                        //RelationHelper.RemoveAndCreateRelation(doctorItem, "Hospital", hospitalItem);

                        //add hospital name classification
                        var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", hospitalUrl);

                        if (HospitalNameClassification != null)
                        {
                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                        }


                    }

                    for (int specIndex = 0; specIndex < item["specialties"].Count(); specIndex++)
                    {
                        string specialtyName = (string)item["specialties"][specIndex]["specialty_name"];
                        string specialtyUrl = specialtyName;
                        Regex reg = new Regex("[^0-9A-Za-z ]");

                        specialtyUrl = reg.Replace(specialtyUrl, string.Empty);
                        var removespace = Regex.Replace(specialtyUrl, " +", " ");

                        specialtyUrl = removespace.Replace(" ", "-").ToLower();

                        if ((bool)item["specialties"][specIndex]["is_subspecialty"] == false)
                        {
                            var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
                            if (mainSpecialtiesItem != null)
                            {
                                // This is how we relate an item
                                RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
                            }
                        }
                        else
                        {
                            // Get related item manager
                            var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
                            if (subSpecialtiesItem != null)
                            {
                                // This is how we relate an item
                                RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);
                            }
                        }
                    }



                    if ((string)item["photo_src"] != null)
                    {

                        var img_src = (string)item["photo_src"];

                        img_src = img_src.Split('/').Last();
                        img_src = img_src.Split('?').First();
                        var img_extension = img_src.Substring(img_src.LastIndexOf('.'));


                        LibrariesManager doctorImageManager = LibrariesManager.GetManager();

                        var doctorImgUrlName = Regex.Replace(docUrl.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                        //// Get related item manager

                        var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                            i.UrlName.Contains(doctorImgUrlName));

                        if (ImageItem == null)
                        {
                            var doctorImage = ImageHelper.GetImage("https://www.pantai.com.my" + (string)item["photo_src"]);
                            ImageHelper.CreateImageWithNativeAPI(Guid.NewGuid(), IHHConstants.DoctorImageLibraryGUID, (string)item["display_name"], doctorImage, docUrl, img_extension);
                        }

                        var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                            i.UrlName.Contains(doctorImgUrlName));

                        if (doctorImageItem != null)
                        {
                            // This is how we relate an item
                            doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
                        }
                        else
                        {
                            Log.Write("Doctor image not found");
                        }
                    }

                }

                doctorItem.SetString("UrlName", docUrl, cultureName);
                doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                doctorItem.SetValue("PublicationDate", DateTime.UtcNow);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                //// Create a version and commit the transaction in order changes to be persisted to data store
                versionManager.CreateVersion(doctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(doctorItem);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                // Create a version
                versionManager.CreateVersion(doctorItem, true);
                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                versionManager.CreateVersion(checkInDoctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                versionManager.CreateVersion(checkInDoctorItem, true);

                TransactionManager.CommitTransaction(transactionName);

            }

            return;
        }

        public static void PatchPantaiDoctorHopital(JToken jsonObjects, DynamicContent parent, int hosId)
        {
            var providerName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Patch Pantai Doctor Hospital";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

            if (parent != null)
            {
                var hospitalUrl = "";

                switch (hosId)
                {
                    case 2:
                        hospitalUrl = "pantai-hospital-ayer-keroh";
                        break;
                    case 7:
                        hospitalUrl = "pantai-hospital-kuala-lumpur";
                        break;
                    case 9:
                        hospitalUrl = "pantai-hospital-penang";
                        break;
                    default:
                        Log.Write("HospitalId = " + hosId);
                        break;
                }

                var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);
                if (hospitalItem != null)
                {
                    var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", hospitalUrl);

                    if (HospitalNameClassification != null)
                    {
                        Type type = ManagerHelper.GetResolvedType(IHHConstants.DoctorHospitalTypeFullName);

                        var childItems = parent.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                        DynamicContent childItem = null;

                        foreach (var child in childItems)
                        {
                            if (child.GetString("UrlName") == HospitalNameClassification.UrlName)
                            {
                                childItem = child;
                            }
                        }

                        if (childItem == null)
                        {
                            childItem = dynamicModuleManager.CreateDataItem(
                                ManagerHelper.GetResolvedType(IHHConstants.DoctorHospitalTypeFullName));
                        }
                        else
                        {
                            Log.Write("Hospital -- Skip!");

                            
                            string PantaiDocUrl = (string)jsonObjects["profileUrl"];

                            PantaiDocUrl = PantaiDocUrl.Split('/').Last();

                            int docId = (int)decimal.Parse((string)jsonObjects["specialties"][0]["doctor_id"]);

                            var clinichouritems = ExternalApiHelper.GetClinicHourObjectsFromPostUrl(docId, hosId);

                            if (clinichouritems != null)
                            {
                                foreach (var clinichouritem in clinichouritems)
                                {
                                    PatchClinicHourPantai2(clinichouritem, PantaiDocUrl, hospitalUrl, childItem);
                                }
                            }
                            

                            return;
                        }

                        foreach (var cultureName in IHHConstants.IHHSFCultures)
                        {
                            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                            childItem.SetString("Title", HospitalNameClassification.Title, cultureName);
                            if (cultureName == "en")
                            {
                                childItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                               
                            }

                         

                            for (int hosIndex = 0; hosIndex < jsonObjects["ehealth"].Count(); hosIndex++)
                            {
                                if (decimal.Parse((string)jsonObjects["ehealth"][hosIndex]["hospital_id"]) == hosId)
                                {
                                    var phone_num = (string)jsonObjects["ehealth"][hosIndex]["phone1_areacode"] + " " + (string)jsonObjects["ehealth"][hosIndex]["phone1"] + (((string)jsonObjects["ehealth"][hosIndex]["phone1_ext"] != "") ? (" / " + (string)jsonObjects["ehealth"][hosIndex]["phone1_ext"]) : "");
                                    var phone_num2 = (string)jsonObjects["ehealth"][hosIndex]["phone2_areacode"] + " " + (string)jsonObjects["ehealth"][hosIndex]["phone2"] + (((string)jsonObjects["ehealth"][hosIndex]["phone2_ext"] != "") ? (" / " + (string)jsonObjects["ehealth"][hosIndex]["phone2_ext"]) : "");
                                    var contact_num = phone_num + " & " + phone_num2;

                                    childItem.SetValue("ContactNumber", contact_num.Trim().Trim('&'));

                                    if (cultureName == "en")
                                    {
                                        var residencyStatus = "";

                                        switch (decimal.Parse((string)jsonObjects["ehealth"][hosIndex]["category_id"]))
                                        {
                                            case 1:
                                                residencyStatus = "resident";
                                                break;
                                            case 2:
                                                residencyStatus = "sessional";
                                                break;
                                            case 3:
                                                residencyStatus = "visiting";
                                                break;
                                            default:
                                                residencyStatus = "";
                                                break;
                                        }

                                        if (residencyStatus != "")
                                        {
                                            var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", residencyStatus);

                                            if (ResidencyStatusClassification != null)
                                            {
                                                childItem.Organizer.AddTaxa("residencystatus", ResidencyStatusClassification.Id);
                                            }
                                        }
                                    }
                                }
                            }

                            childItem.SetString("UrlName", hospitalUrl, cultureName);
                            childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                            childItem.SetValue("PublicationDate", DateTime.UtcNow);
                            childItem.SetValue("IncludeInSitemap", false);

                            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                            //set parent
                            Type doctorsType = TypeResolutionService.ResolveType(IHHConstants.DoctorTypeFullName);
                            childItem.SetParent(parent.Id, doctorsType.FullName);

                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(childItem, false);
                            dynamicModuleManager.Lifecycle.Publish(childItem);

                            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                            // Create a version
                            versionManager.CreateVersion(childItem, true);
                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                            ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                            versionManager.CreateVersion(checkInClinicHourItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                            versionManager.CreateVersion(checkInClinicHourItem, true);

                            TransactionManager.CommitTransaction(transactionName);

                            if (cultureName == "en")
                            {
                                string PantaiDocUrl = (string)jsonObjects["profileUrl"];

                                PantaiDocUrl = PantaiDocUrl.Split('/').Last();

                                int docId = (int)decimal.Parse((string)jsonObjects["specialties"][0]["doctor_id"]);

                                var clinichouritems = ExternalApiHelper.GetClinicHourObjectsFromPostUrl(docId, hosId);

                                if (clinichouritems != null)
                                {
                                    foreach (var clinichouritem in clinichouritems)
                                    {
                                        PatchClinicHourPantai2(clinichouritem, PantaiDocUrl, hospitalUrl, childItem);
                                    }
                                }
                            }
                        }

                        
                    }
                }

            }
            else
            {
                Log.Write("Clinic Hour Parent is null");
            }
        }

        public static void PatchClinicHourPantai2(JToken jsonObjects, string docUrl, string hosUrl, DynamicContent parent)
        {
            var providerName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Patch Clinic Hour Pantai";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

            if (parent != null)
            {
                var itemUrl = (string)jsonObjects["availible_days"] + " - " + (string)jsonObjects["start_time"] + "-" + (string)jsonObjects["end_time"];
                var itemAvailability = (((string)jsonObjects["by_appt_only"] == "No") ? "1" : "2");
                itemUrl = itemUrl.Replace(" ", "-").Replace(",", "-").Replace(":", "").ToLower() + "-" + itemAvailability;

                //DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(
                //      IHHConstants.DoctorClinicHourTypeFullName, itemUrl);

                var childItems = parent.GetChildItems(IHHConstants.DoctorClinicHourTypeFullName);

                DynamicContent childItem = null;

                foreach (var child in childItems)
                {
                    if (child.GetString("UrlName") == itemUrl)
                    {
                        childItem = child;
                    }
                }

                if (childItem == null)
                {
                    childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));
                }
                else
                {
                    Log.Write("Clinic Hour --- Skip!");
                    return;
                }

                //var childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));

                foreach (var cultureName in IHHConstants.GleaglesSFCultures)
                {
                    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                    // This is how values for the properties are set
                    childItem.SetString("Title", (string)jsonObjects["availible_days"] + " - " + (string)jsonObjects["start_time"]+ "-" + (string)jsonObjects["end_time"], cultureName);
                    childItem.SetValue("StartTime", (string)jsonObjects["start_time"]);
                    childItem.SetValue("EndTime", (string)jsonObjects["end_time"]);
                    string availability = "1";

                    if (cultureName == "en")
                    {
                        string days = (string)jsonObjects["availible_days"];

                        string[] availableDays = days.Split(',');

                        List<string> dayList = new List<string>();
                   
                        foreach (var d in availableDays)
                        {
                            dayList.Add(GetOptionsValue(d));
                        }

                        availability = (((string)jsonObjects["by_appt_only"] == "No") ? "1" : "2");

                        childItem.SetValue("Days", dayList.ToArray());
                        childItem.SetValue("Availability", availability);
                        childItem.SetString("OtherAvailabilities", (string)jsonObjects["clinic_remarks"], cultureName);
                    }

                    var title = (string)jsonObjects["availible_days"] + " - " + (string)jsonObjects["start_time"] + "-" + (string)jsonObjects["end_time"];

                    childItem.SetString("UrlName", title.Replace(" ", "-").Replace(",", "-").Replace(":", "").ToLower() + "-" + availability, cultureName);
                    childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                    childItem.SetValue("PublicationDate", DateTime.UtcNow);
                    childItem.SetValue("IncludeInSitemap", false);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                    //set parent
                    Type OtherdoctorHospitalType = TypeResolutionService.ResolveType(IHHConstants.DoctorHospitalTypeFullName);
                    childItem.SetParent(parent.Id, OtherdoctorHospitalType.FullName);


                    //// Create a version and commit the transaction in order changes to be persisted to data store
                    versionManager.CreateVersion(childItem, false);
                    dynamicModuleManager.Lifecycle.Publish(childItem);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                    // Create a version
                    versionManager.CreateVersion(childItem, true);
                    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                    DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                    ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                    versionManager.CreateVersion(checkInClinicHourItem, false);
                    dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                    versionManager.CreateVersion(checkInClinicHourItem, true);

                    TransactionManager.CommitTransaction(transactionName);

                }

            }
            else
            {
                Log.Write("Parent is null");
            }
        }

        public static void PatchDoctorPCMC(JToken jsonObject)
        {
            var ProviderName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Create Doctor Test";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
            DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)jsonObject["UrlName"]);
            if (doctorItem == null)
            {
                doctorItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
            }
            else
            {
                //add hospital name classification
                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);
                if (HospitalNameClassification != null)
                {
                    var childItems = doctorItem.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                    DynamicContent childItem = null;

                    foreach (var child in childItems)
                    {
                        if (child.GetString("UrlName") == HospitalNameClassification.UrlName)
                        {
                            childItem = child;
                        }
                    }

                    if (childItem == null)
                    {
                        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");

                        TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                        var hospitals = doctorItem.GetValue<TrackedList<Guid>>("HospitalClassification");

                        bool hospitalClassifictionIsExist = false;

                        foreach (var hos in hospitals)
                        {
                            if (taxonomyManager.GetTaxon(hos).UrlName == HospitalNameClassification.UrlName)
                            {
                                hospitalClassifictionIsExist = true;
                            }
                        }

                        if (!hospitalClassifictionIsExist)
                        {
                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo("en"));

                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(doctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(doctorItem);

                            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                            // Create a version
                            versionManager.CreateVersion(doctorItem, true);

                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                            ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                            versionManager.CreateVersion(checkInDoctorItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                            versionManager.CreateVersion(checkInDoctorItem, true);

                            TransactionManager.CommitTransaction(transactionName);
                        }

                    }
                }

                return;
            }

            //multilanguage
            foreach (var cultureName in IHHConstants.IHHSFCultures)
            {
                string url = "https://myg03mws013.azurewebsites.net/api/default/doctors"; // 

                var item = ExternalApiHelper.GetSingleDoctorObjWithCulture(url, (string)jsonObject["Id"], cultureName);

                if ((string)item["Name"] == null || (string)item["Name"] == "")
                {
                    Log.Write("Docotr Dont have " + cultureName + " Translate");
                    continue;
                }

                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                var doctorName = (string)item["Name"];
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                // convert the string to title case (i.e. capitalize the first letter of each word)
                doctorName = textInfo.ToTitleCase(doctorName.ToLower());

                // This is how values for the properties are set
                doctorItem.SetString("Name", doctorName, cultureName);
                doctorItem.SetString("Description", (string)item["Description"], cultureName);
                doctorItem.SetString("Salutation", (string)item["Salutation"], cultureName);
                //doctorItem.SetValue("PhoneNo", (string)item["PhoneNo"]);
                doctorItem.SetString("LanguageSpoken", (string)item["LanguageSpoken"], cultureName);
                if ((string)item["YearsOfExperience"] != null)
                {
                    doctorItem.SetValue("YearsOfExperience", decimal.Parse((string)item["YearsOfExperience"]));
                }

                if ((string)item["SortNumber"] != null)
                {
                    doctorItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
                }

                doctorItem.SetString("QualificationAndCertifications", (string)item["QualificationAndCertifications"], cultureName);

                if (cultureName == "en")
                {
                    if (jsonObject["Hospital"].Count() > 0)
                    {
                        //add hospital name classification
                        var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);

                        if (HospitalNameClassification != null)
                        {
                            doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                        }
                    }

                    if (jsonObject["MainSpecialty"].Count() > 0)
                    {
                        // Get related item manager
                        var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["MainSpecialty"][0]["UrlName"]);
                        if (mainSpecialtiesItem != null)
                        {
                            // This is how we relate an item
                            RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
                        }
                        else
                        {
                            Log.Write("Main Spec is null ");
                        }

                    }

                    if (jsonObject["SubSpecialty"].Count() > 0)
                    {
                        RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialty");

                        for (int index = 0; index < jsonObject["SubSpecialty"].Count(); index++)
                        {
                            // Get related item manager
                            var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["SubSpecialty"][index]["UrlName"]);
                            if (subSpecialtiesItem != null)
                            {
                                // This is how we relate an item
                                RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

                            }
                            else
                            {
                                Log.Write("sub Spec is null ");
                            }
                        }

                    }


                    if ((string)item["DoctorImage"][0]["Url"] != null)
                    {

                        LibrariesManager doctorImageManager = LibrariesManager.GetManager();
                        var doctorImgUrlName = (string)item["DoctorImage"][0]["UrlName"];
                       
                        var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                           i.UrlName.Contains(doctorImgUrlName));

                        if (ImageItem == null)
                        {
                            var doctorImage = ImageHelper.GetImage((string)item["DoctorImage"][0]["Url"]);
                            ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);

                        }
                        //// Get related item manager

                        var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                            i.UrlName.Contains(doctorImgUrlName));

                        if (doctorImageItem != null)
                        {
                            // This is how we relate an item
                            doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
                        }
                        else
                        {
                            Log.Write("Doctor image not found");
                        }
                    }
                }

                doctorItem.SetString("UrlName", (string)jsonObject["UrlName"], cultureName);
                doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                doctorItem.SetValue("PublicationDate", DateTime.UtcNow);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                //// Create a version and commit the transaction in order changes to be persisted to data store
                versionManager.CreateVersion(doctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(doctorItem);

                doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                // Create a version
                versionManager.CreateVersion(doctorItem, true);

                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                versionManager.CreateVersion(checkInDoctorItem, false);
                dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                versionManager.CreateVersion(checkInDoctorItem, true);

                TransactionManager.CommitTransaction(transactionName);
            }
        }

        public static void PatchDoctorHopitalPCMC(JToken jsonObject, DynamicContent parent)
        {
            var providerName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Patch Doctor Hospital PCMC";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

            if (parent != null)
            {
                if (jsonObject["Hospital"].Count() == 0)
                {
                    return;
                }

                string url = "https://myg03mws013.azurewebsites.net/api/default/doctors"; // 

                var item = ExternalApiHelper.GetSingleDoctorObjWithCulture(url, (string)jsonObject["Id"], "en");

                //add hospital name classification
                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);

                var childItems = parent.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                DynamicContent childItem = null;

                foreach (var child in childItems)
                {
                    if (child.GetString("UrlName") == HospitalNameClassification.UrlName)
                    {
                        childItem = child;
                    }
                }

                if (childItem == null)
                {
                    childItem = dynamicModuleManager.CreateDataItem(
                        ManagerHelper.GetResolvedType(IHHConstants.DoctorHospitalTypeFullName));
                }
                else
                {
                    Log.Write("Hospital -- Skip!");

                    string clinicHourUrl = "https://myg03mws013.azurewebsites.net/api/default/clinichours?$filter=contains(ItemDefaultUrl,%20%27" + jsonObject["UrlName"] + "%27)";

                    JArray clinicHourItems = ExternalApiHelper.GetObjectsFromUrl(clinicHourUrl);

                    if (clinicHourItems != null)
                    {
                        foreach (var clinicHour in clinicHourItems)
                        {
                            PatchClinicHourPCMC(clinicHour, childItem);
                            Log.Write("Patch doctor child item : " + (string)clinicHour["ItemDefaultUrl"]);
                        }
                    }

                    return;
                }

                foreach (var cultureName in IHHConstants.IHHSFCultures)
                {
                    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                    childItem.SetString("Title", HospitalNameClassification.Title, cultureName);
                    childItem.SetValue("ContactNumber", (string)jsonObject["PhoneNo"]);
                    if (cultureName == "en")
                    {
                        if (jsonObject["Hospital"].Count() > 0)
                        {
                            //add hospital name classification
                            if (HospitalNameClassification != null)
                            {
                                childItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                            }
                        }

                        if ((string)item["residencystatus"][0] != "")
                        {
                            var residencyStatus = "";
                            switch ((string)item["residencystatus"][0])
                            {
                                case "af3a8634-360d-4f98-91eb-ebe0a2b6effb":
                                    residencyStatus = "resident";
                                    break;
                                case "3315b514-d61d-4710-a399-f1d436b8db16":
                                    residencyStatus = "sessional";
                                    break;
                                case "73cfd54c-3e02-4057-8756-e4e91d25519a":
                                    residencyStatus = "visiting";
                                    break;
                                default:
                                    residencyStatus = "";
                                    break;
                            }

                            if (residencyStatus != "")
                            {
                                var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", residencyStatus);

                                if (ResidencyStatusClassification != null)
                                {
                                    childItem.Organizer.AddTaxa("residencystatus", ResidencyStatusClassification.Id);
                                }
                            }

                        }
                    }

                    childItem.SetString("UrlName", HospitalNameClassification.UrlName, cultureName);
                    childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                    childItem.SetValue("PublicationDate", DateTime.UtcNow);
                    childItem.SetValue("IncludeInSitemap", false);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                    //set parent
                    Type doctorsType = TypeResolutionService.ResolveType(IHHConstants.DoctorTypeFullName);
                    childItem.SetParent(parent.Id, doctorsType.FullName);

                    //// Create a version and commit the transaction in order changes to be persisted to data store
                    versionManager.CreateVersion(childItem, false);
                    dynamicModuleManager.Lifecycle.Publish(childItem);

                    childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                    // Create a version
                    versionManager.CreateVersion(childItem, true);
                    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                    DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                    ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                    versionManager.CreateVersion(checkInClinicHourItem, false);
                    dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                    versionManager.CreateVersion(checkInClinicHourItem, true);

                    TransactionManager.CommitTransaction(transactionName);

                    if (cultureName == "en")
                    {
                        string clinicHourUrl = "https://myg03mws013.azurewebsites.net/api/default/clinichours?$filter=contains(ItemDefaultUrl,%20%27" + jsonObject["UrlName"] + "%27)";

                        JArray clinicHourItems = ExternalApiHelper.GetObjectsFromUrl(clinicHourUrl);

                        if (clinicHourItems != null)
                        {
                            foreach (var clinicHour in clinicHourItems)
                            {
                                PatchClinicHourPCMC(clinicHour, childItem);
                                Log.Write("Patch doctor child item : " + (string)clinicHour["ItemDefaultUrl"]);
                            }
                        }
                    }
                }

            }
            else
            {
                Log.Write("Clinic Hour Parent is null");
            }
        }

        public static void PatchClinicHourPCMC(JToken jsonObjects, DynamicContent parent)
        {
            if (parent != null)
            {
                var url = "https://myg03mws013.azurewebsites.net/api/default/clinichours";
                var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], "en");

                if ((string)item["Title"] == null || (string)item["Title"] == "")
                {
                    return;
                }

                string opdUrl = "https://myg03mws013.azurewebsites.net/api/default/opddays?$filter=ParentID%20eq%20" + (string)jsonObjects["Id"];

                var opdItems = ExternalApiHelper.GetObjectsFromUrl(opdUrl);

                List<string> availableList = new List<string>();
                Dictionary<string, List<string>> dayList = new Dictionary<string, List<string>>();
                Dictionary<string, Dictionary<string, string>> OtherAvailabilities = new Dictionary<string, Dictionary<string, string>>();

                if (opdItems != null)
                {
                    foreach (var opd in opdItems)
                    {
                        var opdurl = "https://myg03mws013.azurewebsites.net/api/default/opddays";
                        var opdItem = ExternalApiHelper.GetSingleObjWithCulture(opdurl, (string)opd["Id"], "en");

                        var availability = GetOptionsValue((string)opdItem["Availability"]);

                        switch (availability)
                        {
                            case "1":
                                availability = "2";
                                break;
                            case "2":
                                availability = "3";
                                break;
                            case "3":
                                availability = "4";
                                break;
                            case "4":
                                availability = "5";
                                break;
                            case "5":
                                availability = "6";
                                break;
                            case "6":
                                availability = "7";
                                break;
                            case "7":
                                availability = "8";
                                break;
                            case "8":
                                availability = "1";
                                break;
                            case "9":
                                availability = "9";
                                break;
                            default:
                                availability = "1";
                                break;
                        }

                        if (!availableList.Contains(availability))
                        {
                            availableList.Add(availability);
                            dayList[availability] = new List<string>();
                            OtherAvailabilities[availability] = new Dictionary<string, string>();
                        }

                        dayList[availability].Add(GetOptionsValue((string)opdItem["Day"]));
                        OtherAvailabilities[availability]["en"] = (string)opdItem["OtherAvailabilities"];

                        var opdItemID = ExternalApiHelper.GetSingleObjWithCulture(opdurl, (string)opd["Id"], "id");
                        if (opdItemID != null)
                        {
                            OtherAvailabilities[availability]["id"] = (string)opdItem["OtherAvailabilities"];
                        }
                    }
                }

                foreach (var available in availableList)
                {
                    var providerName = IHHConstants.IHHDynamicModuleProvider;
                    var transactionName = "Patch Clinic Hour GE";
                    var versionManager = VersionManager.GetManager(null, transactionName);

                    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

                    DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
                       IHHConstants.DoctorClinicHourTypeFullName, (string)jsonObjects["Id"] + "-" + available);

                    if (childItem == null)
                    {
                        childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));
                    }
                    else
                    {
                        return;
                    }

                    foreach (var cultureName in IHHConstants.IHHSFCultures)
                    {
                        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                        List<string> days = dayList[available];

                        string[] opdDay = days.ToArray();
                        Log.Write(opdDay.ToString());
                        var title = "";
                        switch (available)
                        {
                            case "1":
                                title = (string)item["Title"];
                                break;
                            case "2":
                                title = (string)item["Title"] + " - By Appointment";
                                break;
                            default:
                                title = (string)item["Title"] + " - Others";
                                break;
                        }

                        childItem.SetString("Title", title, cultureName);
                        childItem.SetString("StartTime", (string)item["StartTime"], cultureName);
                        childItem.SetString("EndTime", (string)item["EndTime"], cultureName);

                        childItem.SetValue("Days", opdDay);
                        childItem.SetValue("Availability", available);
                        childItem.SetString("OtherAvailabilities", OtherAvailabilities[available][cultureName], cultureName);

                        childItem.SetString("UrlName", (string)item["Id"] + "-" + available);
                        childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                        childItem.SetValue("PublicationDate", DateTime.UtcNow);
                        childItem.SetValue("IncludeInSitemap", false);

                        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

                        //set parent
                        Type OtherdoctorHospitalType = TypeResolutionService.ResolveType(IHHConstants.DoctorHospitalTypeFullName);
                        childItem.SetParent(parent.Id, OtherdoctorHospitalType.FullName);

                        //// Create a version and commit the transaction in order changes to be persisted to data store
                        versionManager.CreateVersion(childItem, false);
                        dynamicModuleManager.Lifecycle.Publish(childItem);

                        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                        // Create a version
                        versionManager.CreateVersion(childItem, true);
                        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                        DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
                        ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
                        versionManager.CreateVersion(checkInClinicHourItem, false);
                        dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

                        versionManager.CreateVersion(checkInClinicHourItem, true);
                        TransactionManager.CommitTransaction(transactionName);
                    }

                }

            }
            else
            {
                Log.Write("Clinic Hour Parent is null");
            }
        }


        public static void UpdateDoctorHospitalClassification(DynamicContent dynamicContentItem)
        {
            DynamicContent parent = dynamicContentItem.SystemParentItem;

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

            if (parent != null)
            {
                var HospitalName = "";
                var LocationState = "";
                var hospitalUrl = "";
                var brand = "";

                var childItems = parent.GetChildItems(IHHConstants.DoctorHospitalTypeFullName);

                if (childItems != null)
                {
                    //remove all hospital classification
                    TaxonomyHelper.RemovedAllTaxaInField(parent, "HospitalClassification");

                    //add resident - hos classification
                    foreach (var child in childItems)
                    {
                        var IsResident = false;
                        foreach (var status in child.GetValue<TrackedList<Guid>>("residencystatus"))
                        {
                            var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", taxonomyManager.GetTaxon(status).UrlName);
                            if (ResidencyStatusClassification.Title == "Resident")
                            {
                                IsResident = true;
                            }
                        }
                        if(IsResident)
                        {
                            //get child item url 
                            foreach (var hos in child.GetValue<TrackedList<Guid>>("HospitalClassification"))
                            {
                                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", taxonomyManager.GetTaxon(hos).UrlName);
                                if (HospitalNameClassification != null)
                                {
                                    parent.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                                }
                            }                          
                        }
                    }

                    //add other hos classification
                    foreach (var child in childItems)
                    {
                        var IsResident = false;
                        foreach (var status in child.GetValue<TrackedList<Guid>>("residencystatus"))
                        {
                            var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", taxonomyManager.GetTaxon(status).UrlName);
                            if (ResidencyStatusClassification.Title == "Resident")
                            {
                                IsResident = true;
                            }
                        }
                        if(!IsResident)
                        {
                            //get child item url 
                            foreach (var hos in child.GetValue<TrackedList<Guid>>("HospitalClassification"))
                            {
                                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", taxonomyManager.GetTaxon(hos).UrlName);
                                if (HospitalNameClassification != null)
                                {
                                    parent.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
                                }
                            }
                        }
                        

                    }
                }
                else
                {
                    //remove all hospital classification
                    TaxonomyHelper.RemovedAllTaxaInField(parent, "HospitalClassification");

                    dynamicContentItem.SetString("ProfileUrl", "");
                    dynamicContentItem.SetString("AppointmentUrl", "");

                }

                String[] cultureName = dynamicContentItem.AvailableLanguages;

                foreach (var culture in cultureName)
                {
                    var count = 0;

                    foreach (var hos in parent.GetValue<TrackedList<Guid>>("HospitalClassification"))
                    {
                        hospitalUrl = taxonomyManager.GetTaxon(hos).UrlName;

                        
                        DynamicContent hospitalItem = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);

                        if (hospitalItem != null)
                        {
                            if (!String.IsNullOrEmpty(hospitalItem.GetString("Title", culture)))
                            {
                                if (count == 0 && culture == "en")
                                {
                                    var profileUrl = "/" + FrontendUrlHelper.GetHospNameFromString(hospitalUrl) + "/doctors" + dynamicContentItem.ItemDefaultUrl;
                                    var apptUrl = "/" + FrontendUrlHelper.GetHospNameFromString(hospitalUrl) + "/appointment" + dynamicContentItem.ItemDefaultUrl;

                                    parent.SetValue("ProfileUrl", profileUrl);
                                    parent.SetValue("AppointmentUrl", apptUrl);
                                }

                                count++;

                                HospitalName = HospitalName + "," + hospitalItem.GetString("Title", culture).ToString();

                                var locations = hospitalItem.GetValue<TrackedList<Guid>>("location");

                                foreach (var loc in locations)
                                {
                                    // get the category name
                                    LocationState = LocationState + "," + taxonomyManager.GetTaxon(loc).Title.ToString();
                                }

                                var brands = hospitalItem.GetValue<TrackedList<Guid>>("brands");

                                foreach (var b in brands)
                                {
                                    // get the category name
                                    brand = brand + "," + taxonomyManager.GetTaxon(b).Title.ToString();
                                }
                            }
                        }

                    }

                    char[] charsToTrim = { ' ', ',' };
                    parent.SetString("HospitalName", HospitalName.Trim(charsToTrim), culture);
                    parent.SetString("Location", LocationState.Trim(charsToTrim), culture);
                    parent.SetString("Brand", brand.Trim(charsToTrim), culture);
                }
            }
        }

        public static void UpdateDoctorHospitalTitle(DynamicContent dynamicContentItem)
        {
            if (dynamicContentItem != null)
            {
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

                foreach (var hos in dynamicContentItem.GetValue<TrackedList<Guid>>("HospitalClassification"))
                {
                    dynamicContentItem.SetString("Title", taxonomyManager.GetTaxon(hos).Title.ToString());

                    dynamicContentItem.SetString("UrlName", taxonomyManager.GetTaxon(hos).UrlName);
                }
            }
        }

        public static void PatchDummy(JToken jsonObject)
        {
            var ProviderName = IHHConstants.IHHDynamicModuleProvider;
            var transactionName = "Remove Doctor Test";
            var versionManager = VersionManager.GetManager(null, transactionName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
            DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)jsonObject["UrlName"]);

            if (doctorItem != null)
            {
                foreach(var culture in doctorItem.AvailableLanguages)
                {
                    doctorItem.SetString("MainSpecialtyName", "", culture);
                    doctorItem.SetString("SubSpecialtyName", "", culture);

                    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(culture));

                    //// Create a version and commit the transaction in order changes to be persisted to data store
                    versionManager.CreateVersion(doctorItem, false);
                    dynamicModuleManager.Lifecycle.Publish(doctorItem);

                    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

                    // Create a version
                    versionManager.CreateVersion(doctorItem, true);

                    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                    DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
                    ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
                    versionManager.CreateVersion(checkInDoctorItem, false);
                    dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

                    versionManager.CreateVersion(checkInDoctorItem, true);

                    TransactionManager.CommitTransaction(transactionName);
                }
               

                return;
            }
            return;

        }

        public static void UpdateClinicHour()
        {
            var clinicHour = ManagerHelper.GetDyanamicContents(IHHConstants.DoctorClinicHourTypeFullName);

            foreach (var ch in clinicHour)
            {

                if (ch.GetValue("StartTime").ToString().Count() < ch.GetValue("EndTime").ToString().Count())
                {
                    var startTime = ch.GetValue("StartTime").ToString();
                    var endTime = ch.GetValue("EndTime").ToString();

                    if (ch.GetValue("StartTime").ToString().Contains("am"))
                    {
                        startTime = startTime.Replace("am", "");
                    }

                    if (ch.GetValue("EndTime").ToString().Contains("pm"))
                    {
                        endTime = endTime.Replace("pm", "");
                    }

                    startTime = "0" + startTime;

                    ch.SetValue("StartTime", startTime);
                    ch.SetValue("EndTime", endTime);
                }
            }
        }
    }
}

