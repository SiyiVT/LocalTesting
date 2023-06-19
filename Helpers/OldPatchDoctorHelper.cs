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
    //OLD CODE - FOR REFERENCE ONLY
    public class OldPatchDoctorHelper
    {
        //public static void PatchDoctor(JToken jsonObject)
        //{
        //    var ProviderName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Create Doctor";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
        //    DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(
        //                  IHHConstants.DoctorTypeFullName, (string)jsonObject["UrlName"]);
        //    if (doctorItem == null)
        //    {
        //        doctorItem = dynamicModuleManager.CreateDataItem(
        //            ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
        //    }
        //    else
        //    {
        //        if ((string)jsonObject["HospitalName"] == "Gleneagles Hospital Kota Kinabalu")
        //        {
        //            var cultureName = "en";
        //            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //            doctorItem.SetString("Location", "Sabah", cultureName);

        //            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //            //// Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(doctorItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //            doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //            // Create a version
        //            versionManager.CreateVersion(doctorItem, true);
        //            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //            ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //            versionManager.CreateVersion(checkInDoctorItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //            versionManager.CreateVersion(checkInDoctorItem, true);

        //            TransactionManager.CommitTransaction(transactionName);

        //        }

        //        Log.Write("Existing Doctor Decteced -- Skip!");
        //        //String[] cultureName = doctorItem.AvailableLanguages;

        //        //foreach (var culture in cultureName)
        //        //{
        //        //    if (doctorItem.GetRelatedItemsCountByField("MainSpecialty") > 0 &&
        //        //        doctorItem.GetRelatedItemsCountByField("Hospital") > 0 &&
        //        //        doctorItem.GetValue("Name") != null &&
        //        //        doctorItem.GetValue("Salutation") != null)
        //        //    {
        //        //        var mainSpecialties = doctorItem.GetRelatedItems<DynamicContent>("MainSpecialty").FirstOrDefault();
        //        //        var hospital = doctorItem.GetRelatedItems<DynamicContent>("Hospital").FirstOrDefault();
        //        //        string title = doctorItem.GetString("Name", culture);
        //        //        string salutation = doctorItem.GetString("Salutation", culture);
        //        //        if (mainSpecialties != null && hospital != null)
        //        //        {
        //        //            string metaTitle = string.Format("{0} {1} | {2} | {3}", salutation, title, mainSpecialties.GetString("Title", culture), hospital.GetString("Title", culture));

        //        //            string metaDesciprtion = string.Format("{0} {1} specializes in {2} and works at {3} in Malaysia.", salutation, title, mainSpecialties.GetString("Title", culture), hospital.GetString("Title", culture));

        //        //            doctorItem.SetString("MetaTitle", metaTitle, culture);
        //        //            doctorItem.SetString("MetaDescription", metaDesciprtion, culture);
        //        //            if (doctorItem.DoesFieldExist("OpenGraphTitle"))
        //        //            {
        //        //                doctorItem.SetString("OpenGraphTitle", metaTitle, culture);
        //        //            }
        //        //            if (doctorItem.DoesFieldExist("OpenGraphDescription"))
        //        //            {
        //        //                doctorItem.SetString("OpenGraphDescription", metaDesciprtion, culture);
        //        //            }
        //        //        }


        //        //    }

        //        //    if (doctorItem.GetRelatedItemsCountByField("DoctorImage") > 0 && doctorItem.DoesFieldExist("OpenGraphImage"))
        //        //    {
        //        //        var image = doctorItem.GetRelatedItems<Image>("DoctorImage").FirstOrDefault();

        //        //        if (image != null)
        //        //        {
        //        //            var masterImage = ManagerHelper.GetMasterItem(image);
        //        //            if (masterImage != null)
        //        //            {
        //        //                doctorItem.CreateRelation(masterImage, "OpenGraphImage");

        //        //            }
        //        //        }
        //        //    }

        //        //    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(culture);

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(culture));

        //        //    //// Create a version and commit the transaction in order changes to be persisted to data store
        //        //    versionManager.CreateVersion(doctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        //    // Create a version
        //        //    versionManager.CreateVersion(doctorItem, true);
        //        //    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //    DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        //    ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        //    versionManager.CreateVersion(checkInDoctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        //    versionManager.CreateVersion(checkInDoctorItem, true);

        //        //    TransactionManager.CommitTransaction(transactionName);

        //        //    Log.Write("Update Meta Setting");
        //        //}


        //        //check doc main specialty n sub specialty
        //        //var specialty = doctorItem.GetRelatedItems("MainSpecialty").FirstOrDefault();


        //        //DynamicContent spec = ManagerHelper.GetDynamicContentsByOriginalContentId(IHHConstants.SpecialtyTypeFullName, specialty.Id);

        //        //if (spec != null && jsonObject["MainSpecialties"].Count() > 0)
        //        //{
        //        //    var specialtyUrl = (string)jsonObject["MainSpecialties"][0]["UrlName"];
        //        //    var mainSpecUrl = spec.GetValue("UrlName").ToString();
        //        //    if (mainSpecUrl != specialtyUrl)
        //        //    {
        //        //        var cultureName = "en";

        //        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        //        var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
        //        //        if (mainSpecialtiesItem != null)
        //        //        {
        //        //            // This is how we relate an item
        //        //            RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
        //        //        }

        //        //        if (jsonObject["SubSpecialties"].Count() > 0)
        //        //        {

        //        //            RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialties");

        //        //            for (int index = 0; index < jsonObject["SubSpecialties"].Count(); index++)
        //        //            {
        //        //                // Get related item manager
        //        //                var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["SubSpecialties"][index]["UrlName"]);
        //        //                if (subSpecialtiesItem != null)
        //        //                {
        //        //                    // This is how we relate an item
        //        //                    RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

        //        //                }
        //        //                else
        //        //                {
        //        //                    Log.Write("sub Spec is null ");
        //        //                }
        //        //            }

        //        //        }


        //        //if (jsonObject["SubSpecialties"].Count() > 0)
        //        //{
        //        //    var cultureName = "en";
        //        //    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        //    RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialties");

        //        //    for (int index = 0; index < jsonObject["SubSpecialties"].Count(); index++)
        //        //    {
        //        //        // Get related item manager
        //        //        var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["SubSpecialties"][index]["UrlName"]);
        //        //        if (subSpecialtiesItem != null)
        //        //        {
        //        //            // This is how we relate an item
        //        //            RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

        //        //        }

        //        //    }

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //        //    //// Create a version and commit the transaction in order changes to be persisted to data store
        //        //    versionManager.CreateVersion(doctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        //    // Create a version
        //        //    versionManager.CreateVersion(doctorItem, true);
        //        //    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //    DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        //    ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        //    versionManager.CreateVersion(checkInDoctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        //    versionManager.CreateVersion(checkInDoctorItem, true);

        //        //    TransactionManager.CommitTransaction(transactionName);
        //        //}


        //        //    }
        //        //}

        //        //check doc image 
        //        //IDataItem DoctorImageItem = doctorItem.GetRelatedItems("DoctorImage").FirstOrDefault();
        //        //if(DoctorImageItem == null)
        //        //{
        //        //    var cultureName = "en";

        //        //    string url = "https://gleneagles.com.my/api/default/doctors"; // 

        //        //    var item = ExternalApiHelper.GetSingleDoctorObjWithCulture(url, (string)jsonObject["Id"], cultureName);

        //        //    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        //    if ((string)item["DoctorImage"][0]["Url"] != null)
        //        //    {

        //        //        LibrariesManager doctorImageManager = LibrariesManager.GetManager();
        //        //        var doctorImgUrlName = (string)item["UrlName"];
        //        //        doctorImgUrlName = Regex.Replace(doctorImgUrlName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

        //        //        var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //        //            i.UrlName.Contains(doctorImgUrlName));

        //        //        if (ImageItem == null)
        //        //        {
        //        //            var doctorImage = ImageHelper.GetImage((string)item["DoctorImage"][0]["Url"]);
        //        //            ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);

        //        //        }
        //        //        //// Get related item manager

        //        //        var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //        //            i.UrlName.Contains(doctorImgUrlName));

        //        //        if (doctorImageItem != null)
        //        //        {
        //        //            // This is how we relate an item
        //        //            doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
        //        //        }
        //        //        else
        //        //        {
        //        //            Log.Write("Doctor image not found");
        //        //        }
        //        //    }

        //        //    ////// Create a version and commit the transaction in order changes to be persisted to data store
        //        //    //versionManager.CreateVersion(doctorItem, false);
        //        //    //TransactionManager.CommitTransaction(transactionName);

        //        //    ////// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //    //DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        //    //DynamicContent checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem) as DynamicContent;
        //        //    //versionManager.CreateVersion(checkInDoctorItem, false);
        //        //    //TransactionManager.CommitTransaction(transactionName);

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //        //    //// Create a version and commit the transaction in order changes to be persisted to data store
        //        //    versionManager.CreateVersion(doctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        //    doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        //    // Create a version
        //        //    versionManager.CreateVersion(doctorItem, true);
        //        //    //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //    DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        //    ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        //    versionManager.CreateVersion(checkInDoctorItem, false);
        //        //    dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        //    versionManager.CreateVersion(checkInDoctorItem, true);

        //        //    TransactionManager.CommitTransaction(transactionName);

        //        //}

        //        return;

        //    }

        //    //multilanguage
        //    foreach (var cultureName in IHHConstants.IHHSFCultures)
        //    {
        //        string url = "https://gleneagles.com.my/api/default/doctors"; // 

        //        var item = ExternalApiHelper.GetSingleDoctorObjWithCulture(url, (string)jsonObject["Id"], cultureName);

        //        if ((string)item["Title"] == null || (string)item["Title"] == "")
        //        {
        //            Log.Write("Docotr Dont have " + cultureName + " Translate");
        //            continue;
        //        }
        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);


        //        // This is how values for the properties are set
        //        doctorItem.SetString("Name", (string)item["Title"], cultureName);
        //        doctorItem.SetString("Description", (string)item["Description"], cultureName);
        //        doctorItem.SetValue("DisableRequestForAppointment", false);
        //        doctorItem.SetString("Salutation", (string)item["Salutation"], cultureName);
        //        doctorItem.SetString("DoctorStatus", (string)item["DoctorStatus"], cultureName);
        //        doctorItem.SetValue("PhoneNo", (string)item["PhoneNo"]);
        //        doctorItem.SetString("LanguageSpoken", (string)item["LanguageSpoken"], cultureName);
        //        if ((string)item["YearsOfExperience"] != null)
        //        {
        //            doctorItem.SetValue("YearsOfExperience", decimal.Parse((string)item["YearsOfExperience"]));
        //        }

        //        if ((string)item["SortNumber"] != null)
        //        {
        //            doctorItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //        }
        //        doctorItem.SetString("QualificationAndCertifications", (string)item["QualificationAndCertifications"], cultureName);
        //        doctorItem.SetString("ProceduresPerformed", (string)item["ProceduresPerformed"], cultureName);
        //        doctorItem.SetString("ConditionsTreated", (string)item["ConditionsTreated"], cultureName);

        //        if (cultureName == "en")
        //        {
        //            if (jsonObject["Hospital"].Count() > 0)
        //            {
        //                var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, (string)jsonObject["Hospital"][0]["UrlName"]);
        //                if (hospitalItem != null)
        //                {
        //                    //This is how we relate an item
        //                    RelationHelper.RemoveAndCreateRelation(doctorItem, "Hospital", hospitalItem);

        //                    //add hospital name classification
        //                    var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", (string)jsonObject["Hospital"][0]["UrlName"]);

        //                    if (HospitalNameClassification != null)
        //                    {
        //                        doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
        //                    }

        //                    if ((string)item["ResidencyStatus"] != "")
        //                    {
        //                        var residencyStatus = "";
        //                        switch ((string)item["ResidencyStatus"])
        //                        {
        //                            case "1":
        //                                residencyStatus = "resident";
        //                                break;
        //                            case "2":
        //                                residencyStatus = "sessional";
        //                                break;
        //                            case "4":
        //                                residencyStatus = "visiting";
        //                                break;
        //                            default:
        //                                residencyStatus = "";
        //                                break;
        //                        }
        //                        if (residencyStatus != "")
        //                        {
        //                            var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", residencyStatus);

        //                            if (ResidencyStatusClassification != null)
        //                            {
        //                                doctorItem.Organizer.AddTaxa("residencystatus", ResidencyStatusClassification.Id);
        //                            }
        //                        }

        //                    }


        //                }
        //            }

        //            if (jsonObject["MainSpecialties"].Count() > 0)
        //            {
        //                // Get related item manager
        //                var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["MainSpecialties"][0]["UrlName"]);
        //                if (mainSpecialtiesItem != null)
        //                {
        //                    // This is how we relate an item
        //                    RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
        //                }
        //                else
        //                {
        //                    Log.Write("Main Spec is null ");
        //                }

        //            }

        //            if (jsonObject["SubSpecialties"].Count() > 0)
        //            {
        //                RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialties");

        //                for (int index = 0; index < jsonObject["SubSpecialties"].Count(); index++)
        //                {
        //                    // Get related item manager
        //                    var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, (string)jsonObject["SubSpecialties"][index]["UrlName"]);
        //                    if (subSpecialtiesItem != null)
        //                    {
        //                        // This is how we relate an item
        //                        RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

        //                    }
        //                    else
        //                    {
        //                        Log.Write("sub Spec is null ");
        //                    }
        //                }

        //            }


        //            if ((string)item["DoctorImage"][0]["Url"] != null)
        //            {

        //                LibrariesManager doctorImageManager = LibrariesManager.GetManager();
        //                var doctorImgUrlName = (string)item["UrlName"];
        //                doctorImgUrlName = Regex.Replace(doctorImgUrlName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

        //                var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //                   i.UrlName.Contains(doctorImgUrlName));

        //                if (ImageItem == null)
        //                {
        //                    var doctorImage = ImageHelper.GetImage((string)item["DoctorImage"][0]["Url"]);
        //                    ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);

        //                }
        //                //// Get related item manager

        //                var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //                    i.UrlName.Contains(doctorImgUrlName));

        //                if (doctorImageItem != null)
        //                {
        //                    // This is how we relate an item
        //                    doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
        //                }
        //                else
        //                {
        //                    Log.Write("Doctor image not found");
        //                }
        //            }

        //            Log.Write("Hospital: " + jsonObject["Hospital"]);
        //            Log.Write("Main Spec: " + jsonObject["MainSpecialties"]);
        //            Log.Write("Sub Spec: " + jsonObject["SubSpecialties"]);
        //        }

        //        //UpdateMetaData(doctorItem, cultureName);


        //        doctorItem.SetString("UrlName", (string)jsonObject["UrlName"], cultureName);
        //        doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //        doctorItem.SetValue("PublicationDate", DateTime.UtcNow);


        //        //doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published", new CultureInfo(cultureName));

        //        ////// Create a version and commit the transaction in order changes to be persisted to data store
        //        //versionManager.CreateVersion(doctorItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        ////// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        //DynamicContent checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem) as DynamicContent;
        //        //versionManager.CreateVersion(checkInDoctorItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(doctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version
        //        versionManager.CreateVersion(doctorItem, true);
        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        versionManager.CreateVersion(checkInDoctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        versionManager.CreateVersion(checkInDoctorItem, true);

        //        TransactionManager.CommitTransaction(transactionName);
        //    }
        //}

        //public static void PatchClinicHour(JToken jsonObjects)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Clinic Hour";
        //    var versionManager = VersionManager.GetManager(null, transactionName);


        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    var parentObj = ExternalApiHelper.GetSingleObjWithCulture(
        //               "https://gleneagles.com.my/api/default/doctors", (string)jsonObjects["ParentId"], "en");



        //    DynamicContent parent = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //        IHHConstants.DoctorTypeFullName, (string)parentObj["UrlName"]
        //    );

        //    if (parent != null)
        //    {
        //        DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //            IHHConstants.DoctorClinicHourTypeFullName, (string)jsonObjects["Id"]);
        //        if (childItem == null)
        //        {
        //            childItem = dynamicModuleManager.CreateDataItem(
        //                ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));
        //        }
        //        else
        //        {

        //            Log.Write("Existing Clinic Hour Decteced -- Skip!");
        //            return;
        //        }
        //        //DynamicContent childItem = dynamicModuleManager.CreateDataItem(
        //        //        ManagerHelper.GetResolvedType(GleneaglesConstants.DoctorClinicHourTypeFullName));

        //        //DynamicContent childItem = dynamicModuleManager.CreateDataItem(
        //        //       ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));

        //        foreach (var cultureName in IHHConstants.GleaglesSFCultures)
        //        {
        //            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //            var url = "https://gleneagles.com.my/api/default/clinichours";
        //            var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], cultureName);

        //            // This is how values for the properties are set
        //            childItem.SetString("Title", (string)item["Title"], cultureName);
        //            childItem.SetString("StartTime", (string)item["StartTime"], cultureName);
        //            childItem.SetString("EndTime", (string)item["EndTime"], cultureName);
        //            if ((string)item["SortNumber"] != null)
        //            {
        //                childItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //            }


        //            childItem.SetString("UrlName", (string)item["Id"], cultureName);
        //            childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //            childItem.SetValue("PublicationDate", DateTime.UtcNow);



        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));


        //            //set parent
        //            Type doctorsType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Doctors");
        //            childItem.SetParent(parent.Id, doctorsType.FullName);


        //            // Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(childItem, false);
        //            TransactionManager.CommitTransaction(transactionName);

        //            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            DynamicContent checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem) as DynamicContent;
        //            versionManager.CreateVersion(checkInClinicHourItem, false);
        //            TransactionManager.CommitTransaction(transactionName);
        //            Log.Write("Create Clinic Hour");
        //        }
        //    }
        //    else
        //    {
        //        Log.Write("Clinic Hour Parent is null");
        //    }
        //}

        //public static void PatchClinicHourGE(JToken jsonObjects, DynamicContent parent)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Clinic Hour GE";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    if (parent != null)
        //    {
        //        DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //           IHHConstants.DoctorClinicHourTypeFullName, (string)jsonObjects["Id"]);

        //        if (childItem == null)
        //        {
        //            childItem = dynamicModuleManager.CreateDataItem(
        //                ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));
        //        }
        //        else
        //        {
        //            Log.Write("Existing Clinic Hour Decteced -- Skip!");

        //            string opdUrl = "https://gleneagles.com.my/api/default/opddays?$filter=ParentID%20eq%20" + (string)jsonObjects["Id"];

        //            var opdItems = ExternalApiHelper.GetObjectsFromUrl(opdUrl);

        //            if (opdItems != null)
        //            {
        //                foreach (var opd in opdItems)
        //                {
        //                    PatchOperationHourGE(opd, childItem);
        //                }
        //            }

        //            return;
        //        }

        //        foreach (var cultureName in IHHConstants.IHHSFCultures)
        //        {
        //            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //            var url = "https://gleneagles.com.my/api/default/clinichours";
        //            var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], cultureName);

        //            if ((string)item["Title"] == null || (string)item["Title"] == "")
        //            {
        //                Log.Write("CLinic hour Dont have " + cultureName + " Translate");
        //                continue;
        //            }

        //            childItem.SetString("Title", (string)item["Title"], cultureName);
        //            childItem.SetString("StartTime", (string)item["StartTime"], cultureName);
        //            childItem.SetString("EndTime", (string)item["EndTime"], cultureName);
        //            if ((string)item["SortNumber"] != null)
        //            {
        //                childItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //            }


        //            childItem.SetString("UrlName", (string)item["Id"], cultureName);
        //            childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //            childItem.SetValue("PublicationDate", DateTime.UtcNow);



        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));


        //            //set parent
        //            Type doctorsType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Doctors");
        //            childItem.SetParent(parent.Id, doctorsType.FullName);


        //            //// Create a version and commit the transaction in order changes to be persisted to data store
        //            //versionManager.CreateVersion(childItem, false);
        //            //TransactionManager.CommitTransaction(transactionName);

        //            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            //DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            //DynamicContent checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem) as DynamicContent;
        //            //versionManager.CreateVersion(checkInClinicHourItem, false);
        //            //TransactionManager.CommitTransaction(transactionName);


        //            //// Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(childItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(childItem);

        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //            // Create a version
        //            versionManager.CreateVersion(childItem, true);
        //            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
        //            versionManager.CreateVersion(checkInClinicHourItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

        //            versionManager.CreateVersion(checkInClinicHourItem, true);

        //            TransactionManager.CommitTransaction(transactionName);

        //            if (cultureName == "en")
        //            {
        //                string opdUrl = "https://gleneagles.com.my/api/default/opddays?$filter=ParentID%20eq%20" + (string)jsonObjects["Id"];

        //                var opdItems = ExternalApiHelper.GetObjectsFromUrl(opdUrl);

        //                if (opdItems != null)
        //                {
        //                    foreach (var opd in opdItems)
        //                    {
        //                        PatchOperationHourGE(opd, childItem);
        //                    }
        //                }

        //            }
        //        }

        //    }
        //    else
        //    {
        //        Log.Write("Clinic Hour Parent is null");
        //    }
        //}
        //public static void PatchOperationHour(JToken jsonObjects)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Operation Hour";
        //    var versionManager = VersionManager.GetManager(null, transactionName);


        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    var parentObj = ExternalApiHelper.GetSingleObjWithCulture(
        //              "https://gleneagles.com.my/api/default/clinichours", (string)jsonObjects["ParentId"], "en");
        //    DynamicContent parent = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //        IHHConstants.DoctorClinicHourTypeFullName, (string)parentObj["Id"]
        //        );

        //    if (parent != null)
        //    {
        //        DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //            IHHConstants.DoctorOperationHourTypeFullName, (string)jsonObjects["Id"]);
        //        if (childItem == null)
        //        {
        //            childItem = dynamicModuleManager.CreateDataItem(
        //                ManagerHelper.GetResolvedType(IHHConstants.DoctorOperationHourTypeFullName));
        //        }
        //        else
        //        {
        //            Log.Write("Existing OPD Day Decteced -- Update , ID=" + childItem.OriginalContentId);
        //            //return;
        //        }
        //        //DynamicContent childItem = dynamicModuleManager.CreateDataItem(
        //        //        ManagerHelper.GetResolvedType(GleneaglesConstants.DoctorOperationHourTypeFullName));

        //        foreach (var cultureName in IHHConstants.GleaglesSFCultures)
        //        {
        //            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //            var url = "https://gleneagles.com.my/api/default/opddays";
        //            var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], cultureName);

        //            // This is how values for the properties are set
        //            childItem.SetString("Title", (string)item["Title"], cultureName);
        //            // Set the selected value 

        //            string days = DoctorHelper.GetOptionsValue((string)item["Day"]);
        //            childItem.SetValue("Day", days);
        //            // Set the selected value 

        //            string availability = DoctorHelper.GetOptionsValue((string)item["Availability"]);
        //            childItem.SetValue("Availability", availability);
        //            childItem.SetString("OtherAvailabilities", (string)item["OtherAvailabilities"], cultureName);
        //            if ((string)item["SortNumber"] != null)
        //            {
        //                childItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //            }


        //            childItem.SetString("UrlName", (string)jsonObjects["Id"], cultureName);
        //            childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //            childItem.SetValue("PublicationDate", DateTime.UtcNow);


        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published", new CultureInfo(cultureName));


        //            // Set item parent
        //            childItem.SetParent(parent.Id, parent.GetType().FullName);

        //            // Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(childItem, false);
        //            TransactionManager.CommitTransaction(transactionName);

        //            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            DynamicContent checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem) as DynamicContent;
        //            versionManager.CreateVersion(checkInClinicHourItem, false);
        //            TransactionManager.CommitTransaction(transactionName);

        //        }
        //    }
        //    else
        //    {
        //        Log.Write("Parent is null");
        //    }
        //}

        //public static void PatchOperationHourGE(JToken jsonObjects, DynamicContent ParentItem)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Operation Hour GE";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    DynamicContent childItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //        IHHConstants.DoctorOperationHourTypeFullName, (string)jsonObjects["Id"]);
        //    if (childItem == null)
        //    {
        //        childItem = dynamicModuleManager.CreateDataItem(
        //            ManagerHelper.GetResolvedType(IHHConstants.DoctorOperationHourTypeFullName));
        //    }
        //    else
        //    {
        //        Log.Write("Existing OPD Day Decteced -- Update , ID=" + childItem.OriginalContentId);
        //        return;
        //    }
        //    //DynamicContent childItem = dynamicModuleManager.CreateDataItem(
        //    //        ManagerHelper.GetResolvedType(GleneaglesConstants.DoctorOperationHourTypeFullName));

        //    foreach (var cultureName in IHHConstants.IHHSFCultures)
        //    {
        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        var url = "https://gleneagles.com.my/api/default/opddays";
        //        var item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)jsonObjects["Id"], cultureName);

        //        // This is how values for the properties are set
        //        childItem.SetString("Title", (string)item["Title"], cultureName);
        //        // Set the selected value 

        //        string days = DoctorHelper.GetOptionsValue((string)item["Day"]);
        //        childItem.SetValue("Day", days);
        //        // Set the selected value 

        //        string availability = DoctorHelper.GetOptionsValue((string)item["Availability"]);
        //        childItem.SetValue("Availability", availability);
        //        childItem.SetString("OtherAvailabilities", (string)item["OtherAvailabilities"], cultureName);
        //        if ((string)item["SortNumber"] != null)
        //        {
        //            childItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //        }


        //        childItem.SetString("UrlName", (string)jsonObjects["Id"], cultureName);
        //        childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //        childItem.SetValue("PublicationDate", DateTime.UtcNow);

        //        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //        // Set item parent
        //        childItem.SetParent(ParentItem.Id, ParentItem.GetType().FullName);

        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        //versionManager.CreateVersion(childItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //        //DynamicContent checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem) as DynamicContent;
        //        //versionManager.CreateVersion(checkInClinicHourItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        versionManager.CreateVersion(childItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(childItem);

        //        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version
        //        versionManager.CreateVersion(childItem, true);
        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutOPDItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //        ILifecycleDataItem checkInOPDItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutOPDItem);
        //        versionManager.CreateVersion(checkInOPDItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(checkInOPDItem);

        //        versionManager.CreateVersion(checkInOPDItem, true);

        //        TransactionManager.CommitTransaction(transactionName);

        //    }

        //}

        //public static void PatchDoctorPantai(JToken jsonObject, int hosId)
        //{
        //    var ProviderName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Create Doctor";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    string docFullUrl = (string)jsonObject["profileUrl"];

        //    string docUrl = docFullUrl.Split('/').Last();


        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
        //    DynamicContent doctorItem = ManagerHelper.GetMasterDynamicContentsByUrlName(
        //                  IHHConstants.DoctorTypeFullName, docUrl);
        //    if (doctorItem == null)
        //    {
        //        doctorItem = dynamicModuleManager.CreateDataItem(
        //            ManagerHelper.GetResolvedType(IHHConstants.DoctorTypeFullName));
        //    }
        //    else
        //    {

        //        //check doc main specialty n sub specialty
        //        var specialty = doctorItem.GetRelatedItems("MainSpecialty").FirstOrDefault();

        //        DynamicContent spec = ManagerHelper.GetDynamicContentsByOriginalContentId(IHHConstants.SpecialtyTypeFullName, specialty.Id);

        //        var pantaiCultureName = "en-US";
        //        string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";

        //        var item = ExternalApiHelper.GetSingleDocObjectsFromPostUrl(url, hosId, (string)jsonObject["full_name"], pantaiCultureName);

        //        var cultureName = "en";
        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        RelationHelper.DeleteAllRelation(doctorItem, "SubSpecialties");

        //        for (int specIndex = 0; specIndex < item["specialties"].Count(); specIndex++)
        //        {
        //            string specialtyName = (string)item["specialties"][specIndex]["specialty_name"];
        //            string specialtyUrl = specialtyName;
        //            Regex reg = new Regex("[^0-9A-Za-z ]");

        //            specialtyUrl = reg.Replace(specialtyUrl, string.Empty);
        //            var removespace = Regex.Replace(specialtyUrl, " +", " ");

        //            specialtyUrl = removespace.Replace(" ", "-").ToLower();

        //            if ((bool)item["specialties"][specIndex]["is_subspecialty"] == false)
        //            {
        //                var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
        //                if (spec != null && mainSpecialtiesItem != null)
        //                {
        //                    var mainSpecUrl = spec.GetValue("UrlName").ToString();
        //                    if (mainSpecUrl != specialtyUrl)
        //                    {
        //                        RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);
        //                    }

        //                }

        //            }
        //            else
        //            {
        //                // Get related item manager
        //                var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
        //                if (subSpecialtiesItem != null)
        //                {
        //                    // This is how we relate an item
        //                    RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

        //                }

        //            }
        //        }

        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(doctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version
        //        versionManager.CreateVersion(doctorItem, true);
        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        versionManager.CreateVersion(checkInDoctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        versionManager.CreateVersion(checkInDoctorItem, true);

        //        TransactionManager.CommitTransaction(transactionName);

        //        return;
        //    }

        //    //multilanguage
        //    foreach (var cultureName in IHHConstants.GleaglesSFCultures)
        //    {
        //        var pantaiCultureName = "";

        //        switch (cultureName)
        //        {
        //            case "en":
        //                pantaiCultureName = "en-US";
        //                break;
        //            case "id":
        //                pantaiCultureName = "id-ID";
        //                break;
        //            default:
        //                pantaiCultureName = "en-US";
        //                break;
        //        }

        //        string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";

        //        var item = ExternalApiHelper.GetSingleDocObjectsFromPostUrl(url, hosId, (string)jsonObject["full_name"], pantaiCultureName);

        //        if ((string)item["display_name"] == null || (string)item["display_name"] == "")
        //        {
        //            Log.Write("Docotr Dont have " + cultureName + " Translate");
        //            continue;
        //        }

        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        // This is how values for the properties are set
        //        doctorItem.SetString("Name", (string)item["full_name"], cultureName);
        //        //doctorItem.SetString("Description", (string)item["Description"], cultureName);
        //        //doctorItem.SetValue("DisableRequestForAppointment", false);
        //        doctorItem.SetString("Salutation", (string)item["salutation"], cultureName);
        //        //doctorItem.SetString("DoctorStatus", (string)item["DoctorStatus"], cultureName);
        //        //doctorItem.SetValue("PhoneNo", (string)item["PhoneNo"]);
        //        doctorItem.SetString("LanguageSpoken", (string)item["spoken_lang"], cultureName);
        //        //if ((string)item["YearsOfExperience"] != null)
        //        //{
        //        //    doctorItem.SetValue("YearsOfExperience", decimal.Parse((string)item["YearsOfExperience"]));
        //        //}

        //        //if ((string)item["SortNumber"] != null)
        //        //{
        //        //    doctorItem.SetValue("SortNumber", decimal.Parse((string)item["SortNumber"]));
        //        //}


        //        doctorItem.SetString("QualificationAndCertifications", (string)item["qualification"], cultureName);
        //        //doctorItem.SetString("ProceduresPerformed", (string)item["ProceduresPerformed"], cultureName);
        //        //doctorItem.SetString("ConditionsTreated", (string)item["ConditionsTreated"], cultureName);

        //        for (int hosIndex = 0; hosIndex < item["ehealth"].Count(); hosIndex++)
        //        {
        //            if (decimal.Parse((string)item["ehealth"][hosIndex]["hospital_id"]) == hosId)
        //            {
        //                var phone_num = (string)item["ehealth"][hosIndex]["phone1_areacode"] + " " + (string)item["ehealth"][hosIndex]["phone1"] + (((string)item["ehealth"][hosIndex]["phone1_ext"] != "") ? (" / " + (string)item["ehealth"][hosIndex]["phone1_ext"]) : "");
        //                doctorItem.SetValue("PhoneNo", phone_num);

        //                if (cultureName == "en")
        //                {
        //                    doctorItem.SetValue("DisableRequestForAppointment", false);

        //                    var residencyStatus = "";

        //                    switch (decimal.Parse((string)item["ehealth"][hosIndex]["category_id"]))
        //                    {
        //                        case 1:
        //                            residencyStatus = "resident";
        //                            break;
        //                        case 2:
        //                            residencyStatus = "sessional";
        //                            break;
        //                        case 3:
        //                            residencyStatus = "visiting";
        //                            break;
        //                        default:
        //                            residencyStatus = "";
        //                            break;
        //                    }

        //                    if (residencyStatus != "")
        //                    {
        //                        var ResidencyStatusClassification = TaxonomyHelper.GetAFlatTaxon("residency-status", residencyStatus);

        //                        if (ResidencyStatusClassification != null)
        //                        {
        //                            doctorItem.Organizer.AddTaxa("residencystatus", ResidencyStatusClassification.Id);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (cultureName == "en")
        //        {
        //            var hospitalUrl = "";

        //            switch (hosId)
        //            {
        //                case 2:
        //                    hospitalUrl = "pantai-hospital-ayer-keroh";
        //                    break;
        //                case 7:
        //                    hospitalUrl = "pantai-hospital-kuala-lumpur";
        //                    break;
        //                case 9:
        //                    hospitalUrl = "pantai-hospital-penang";
        //                    break;
        //                default:
        //                    Log.Write("HospitalId = " + hosId);
        //                    break;
        //            }

        //            var hospitalItem = ManagerHelper.GetDynamicContentsByUrlName(IHHConstants.HospitalTypeFullName, hospitalUrl);
        //            if (hospitalItem != null)
        //            {
        //                //This is how we relate an item
        //                RelationHelper.RemoveAndCreateRelation(doctorItem, "Hospital", hospitalItem);

        //                //add hospital name classification
        //                var HospitalNameClassification = TaxonomyHelper.GetAFlatTaxon("HospitalClassification", hospitalUrl);

        //                if (HospitalNameClassification != null)
        //                {
        //                    doctorItem.Organizer.AddTaxa("HospitalClassification", HospitalNameClassification.Id);
        //                }


        //            }

        //            for (int specIndex = 0; specIndex < item["specialties"].Count(); specIndex++)
        //            {
        //                string specialtyName = (string)item["specialties"][specIndex]["specialty_name"];
        //                string specialtyUrl = specialtyName;
        //                Regex reg = new Regex("[^0-9A-Za-z ]");

        //                specialtyUrl = reg.Replace(specialtyUrl, string.Empty);
        //                var removespace = Regex.Replace(specialtyUrl, " +", " ");

        //                specialtyUrl = removespace.Replace(" ", "-").ToLower();

        //                if ((bool)item["specialties"][specIndex]["is_subspecialty"] == false)
        //                {
        //                    var mainSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
        //                    if (mainSpecialtiesItem != null)
        //                    {
        //                        // This is how we relate an item
        //                        RelationHelper.RemoveAndCreateRelation(doctorItem, "MainSpecialty", mainSpecialtiesItem);

        //                    }

        //                }
        //                else
        //                {
        //                    // Get related item manager
        //                    var subSpecialtiesItem = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.SpecialtyTypeFullName, specialtyUrl);
        //                    if (subSpecialtiesItem != null)
        //                    {
        //                        // This is how we relate an item
        //                        RelationHelper.CreateRelation(doctorItem, "SubSpecialty", subSpecialtiesItem);

        //                    }

        //                }
        //            }



        //            if ((string)item["photo_src"] != null)
        //            {

        //                var img_src = (string)item["photo_src"];

        //                img_src = img_src.Split('/').Last();
        //                img_src = img_src.Split('?').First();
        //                var img_extension = img_src.Substring(img_src.LastIndexOf('.'));


        //                LibrariesManager doctorImageManager = LibrariesManager.GetManager();

        //                var doctorImgUrlName = Regex.Replace(docUrl.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
        //                //// Get related item manager

        //                var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //                    i.UrlName.Contains(doctorImgUrlName));

        //                if (ImageItem == null)
        //                {
        //                    var doctorImage = ImageHelper.GetImage("https://www.pantai.com.my" + (string)item["photo_src"]);
        //                    ImageHelper.CreateImageWithNativeAPI(Guid.NewGuid(), IHHConstants.DoctorImageLibraryGUID, (string)item["display_name"], doctorImage, docUrl, img_extension);
        //                }

        //                var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
        //                    i.UrlName.Contains(doctorImgUrlName));

        //                if (doctorImageItem != null)
        //                {
        //                    // This is how we relate an item
        //                    doctorItem.CreateRelation(doctorImageItem, "DoctorImage");
        //                }
        //                else
        //                {
        //                    Log.Write("Doctor image not found");
        //                }
        //            }

        //        }


        //        //UpdateMetaData(doctorItem, cultureName);


        //        doctorItem.SetString("UrlName", docUrl, cultureName);
        //        doctorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //        doctorItem.SetValue("PublicationDate", DateTime.UtcNow);


        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));


        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(doctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(doctorItem);

        //        doctorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version
        //        versionManager.CreateVersion(doctorItem, true);
        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutDoctorItem = dynamicModuleManager.Lifecycle.CheckOut(doctorItem) as DynamicContent;
        //        ILifecycleDataItem checkInDoctorItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutDoctorItem);
        //        versionManager.CreateVersion(checkInDoctorItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(checkInDoctorItem);

        //        versionManager.CreateVersion(checkInDoctorItem, true);

        //        TransactionManager.CommitTransaction(transactionName);

        //    }

        //    return;
        //}

        //public static void PatchClinicHourPantai(JToken jsonObjects, string docUrl, int hosUrl)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Clinic Hour Pantai";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    var parent = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.DoctorTypeFullName, docUrl);


        //    // Check pantai and ihh chinic hour count is same or not, if not same delete all and create, else skip 

        //    if (parent != null)
        //    {

        //        var childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorClinicHourTypeFullName));

        //        foreach (var cultureName in IHHConstants.GleaglesSFCultures)
        //        {
        //            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //            // This is how values for the properties are set
        //            childItem.SetString("Title", (string)jsonObjects["availible_days"] + " Clinic Hours", cultureName);
        //            childItem.SetString("StartTime", (string)jsonObjects["start_time"], cultureName);
        //            childItem.SetString("EndTime", (string)jsonObjects["end_time"], cultureName);

        //            childItem.SetString("UrlName", Guid.NewGuid().ToString(), cultureName);
        //            childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //            childItem.SetValue("PublicationDate", DateTime.UtcNow);

        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //            //set parent
        //            Type doctorsType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Doctors");
        //            childItem.SetParent(parent.Id, doctorsType.FullName);


        //            //// Create a version and commit the transaction in order changes to be persisted to data store
        //            //versionManager.CreateVersion(childItem, false);
        //            //TransactionManager.CommitTransaction(transactionName);

        //            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            //DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            //DynamicContent checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem) as DynamicContent;
        //            //versionManager.CreateVersion(checkInClinicHourItem, false);
        //            //TransactionManager.CommitTransaction(transactionName);
        //            //Log.Write("Create Clinic Hour");


        //            //// Create a version and commit the transaction in order changes to be persisted to data store
        //            versionManager.CreateVersion(childItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(childItem);

        //            childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //            // Create a version
        //            versionManager.CreateVersion(childItem, true);
        //            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //            DynamicContent checkOutClinicHourItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //            ILifecycleDataItem checkInClinicHourItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutClinicHourItem);
        //            versionManager.CreateVersion(checkInClinicHourItem, false);
        //            dynamicModuleManager.Lifecycle.Publish(checkInClinicHourItem);

        //            versionManager.CreateVersion(checkInClinicHourItem, true);

        //            TransactionManager.CommitTransaction(transactionName);

        //            if (cultureName == "en")
        //            {
        //                string days = (string)jsonObjects["availible_days"];

        //                string[] availableDays = days.Split(',');

        //                foreach (var d in availableDays)
        //                {
        //                    PatchOperationHourPantai(childItem, d, (string)jsonObjects["by_appt_only"]);
        //                }
        //            }

        //        }

        //    }
        //    else
        //    {
        //        Log.Write("Parent is null");
        //    }
        //}

        //public static void PatchOperationHourPantai(DynamicContent parentItems, string d, string byAppt)
        //{
        //    var providerName = IHHConstants.IHHDynamicModuleProvider;
        //    var transactionName = "Patch Operation Hour";
        //    var versionManager = VersionManager.GetManager(null, transactionName);

        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);

        //    DynamicContent childItem = dynamicModuleManager.CreateDataItem(ManagerHelper.GetResolvedType(IHHConstants.DoctorOperationHourTypeFullName));

        //    string dayValue = DoctorHelper.GetOptionsValue(d);

        //    string availability = ((byAppt == "No") ? "8" : "1");

        //    foreach (var cultureName in IHHConstants.GleaglesSFCultures)
        //    {
        //        Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

        //        // This is how values for the properties are set
        //        childItem.SetString("Title", d, cultureName);
        //        // Set the selected value 
        //        childItem.SetValue("Day", dayValue);
        //        // Set the selected value 
        //        childItem.SetValue("Availability", availability);
        //        childItem.SetString("OtherAvailabilities", "", cultureName);

        //        childItem.SetString("UrlName", Guid.NewGuid().ToString(), cultureName);
        //        childItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //        childItem.SetValue("PublicationDate", DateTime.UtcNow);


        //        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));


        //        // Set item parent
        //        childItem.SetParent(parentItems.Id, parentItems.GetType().FullName);

        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        //versionManager.CreateVersion(childItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        //DynamicContent checkOutOPDItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //        //DynamicContent checkInOPDItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutOPDItem) as DynamicContent;
        //        //versionManager.CreateVersion(checkInOPDItem, false);
        //        //TransactionManager.CommitTransaction(transactionName);

        //        //// Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(childItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(childItem);

        //        childItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version
        //        versionManager.CreateVersion(childItem, true);
        //        //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutOPDItem = dynamicModuleManager.Lifecycle.CheckOut(childItem) as DynamicContent;
        //        ILifecycleDataItem checkInOPDItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutOPDItem);
        //        versionManager.CreateVersion(checkInOPDItem, false);
        //        dynamicModuleManager.Lifecycle.Publish(checkInOPDItem);

        //        versionManager.CreateVersion(checkInOPDItem, true);

        //        TransactionManager.CommitTransaction(transactionName);


        //    }

        //}

    }
}