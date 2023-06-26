
using Newtonsoft.Json.Linq;
using SitefinityWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Versioning;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp
{
    public partial class PatchDoctor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Telerik.Sitefinity.Security.SecurityManager.GetCurrentUserName();
            var duplicatedName = string.Join(",", DoctorHelper.GetDuplicated().ToList());
            lblDoctor.Text = duplicatedName;

            ListItemCollection items = new ListItemCollection
            {
                new ListItem("Select Type", String.Empty),
                new ListItem("Doctor", IHHConstants.DoctorTypeFullName),
                new ListItem("ClinicHour", IHHConstants.DoctorClinicHourTypeFullName),
                new ListItem("DoctorHospital", IHHConstants.DoctorHospitalTypeFullName),
            };

            //ShowDataDropDown.DataSource = items;
            //ShowDataDropDown.DataBind();

            //DoctorHelper.PublishItems();

            //DoctorHelper.UpdateClinicHour();
        }

        protected void Patch_Specialty(object sender, EventArgs e)
        {
            string url = "https://gleneagles.com.my/api/default/specialties";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                decimal count = 0;
                decimal percent = 0;
                foreach (var item in items)
                {
                    ++count;
                    percent = (count / items.Count()) * 100;

                    string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                    Log.Write("Current Progress : " + progress);

                    var ProviderName = IHHConstants.IHHDynamicModuleProvider;
                    var transactionName = "Create Specialty";
                    var versionManager = VersionManager.GetManager(null, transactionName);

                    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
                    DynamicContent specItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(
                                  IHHConstants.SpecialtyTypeFullName, (string)item["UrlName"]);
                    if (specItem == null)
                    {
                        specItem = dynamicModuleManager.CreateDataItem(
                            ManagerHelper.GetResolvedType(IHHConstants.SpecialtyTypeFullName));

                        foreach (var cultureName in IHHConstants.GleaglesSFCultures)
                        {

                            var spec_item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)item["Id"], cultureName);

                            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                            // This is how values for the properties are set
                            specItem.SetString("Title", (string)spec_item["Title"], cultureName);
                            specItem.SetString("Description", (string)spec_item["Title"], cultureName);
                            specItem.SetString("TitleToDisplay", (string)spec_item["TitleToDisplay"], cultureName);
                            specItem.SetValue("isHidden", false);
                            if ((string)spec_item["SortNumber"] != null)
                            {
                                specItem.SetValue("SortNumber", decimal.Parse((string)spec_item["SortNumber"]));
                            }

                            specItem.SetString("UrlName", (string)item["UrlName"], cultureName);
                            specItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                            specItem.SetValue("PublicationDate", DateTime.UtcNow);


                           
                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(specItem, false);
                            dynamicModuleManager.Lifecycle.Publish(specItem);

                            specItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
                            versionManager.CreateVersion(specItem, true);
                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutSpecialtyItem = dynamicModuleManager.Lifecycle.CheckOut(specItem) as DynamicContent;
                            DynamicContent checkInSpecialtyItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutSpecialtyItem) as DynamicContent;
                            versionManager.CreateVersion(checkInSpecialtyItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInSpecialtyItem);
                            versionManager.CreateVersion(checkInSpecialtyItem, true);
                            TransactionManager.CommitTransaction(transactionName);
                        }
                    }
                    else
                    {
                        Log.Write("Existing Specialty Decteced -- Skip!");

                        foreach (var cultureName in IHHConstants.GleaglesSFCultures)
                        {

                            var spec_item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)item["Id"], cultureName);

                            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                            // This is how values for the properties are set
                            specItem.SetString("Title", (string)spec_item["Title"], cultureName);
                            specItem.SetString("TitleToDisplay", (string)spec_item["TitleToDisplay"], cultureName);

                            //// Create a version and commit the transaction in order changes to be persisted to data store
                            versionManager.CreateVersion(specItem, false);
                            dynamicModuleManager.Lifecycle.Publish(specItem);

                            specItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
                            versionManager.CreateVersion(specItem, true);
                            //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                            DynamicContent checkOutSpecialtyItem = dynamicModuleManager.Lifecycle.CheckOut(specItem) as DynamicContent;
                            DynamicContent checkInSpecialtyItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutSpecialtyItem) as DynamicContent;
                            versionManager.CreateVersion(checkInSpecialtyItem, false);
                            dynamicModuleManager.Lifecycle.Publish(checkInSpecialtyItem);
                            versionManager.CreateVersion(checkInSpecialtyItem, true);
                            TransactionManager.CommitTransaction(transactionName);
                        }
                    }

                    //multilanguage

                }
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }

        }

        protected void Patch_Specialty_Pantai(object sender, EventArgs e)
        {
            string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/GetHospitalSpecialties";

            try
            {
                int[] hospitalsId = new int[] { 2, 7, 9 };

                foreach (int hosId in hospitalsId)
                {
                    var items = ExternalApiHelper.GetSpecialtyObjectsFromPostUrl(url, hosId);

                    decimal count = 0;
                    decimal percent = 0;
                    foreach (var item in items)
                    {
                        ++count;
                        percent = (count / items.Count()) * 100;

                        string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                        Log.Write("Current Progress Specialty Pantai: " + progress);

                        var ProviderName = IHHConstants.IHHDynamicModuleProvider;
                        var transactionName = "Create Specialty";
                        var versionManager = VersionManager.GetManager(null, transactionName);

                        var specialtyUrl = (string)item["name"];

                        Regex reg = new Regex("[^0-9A-Za-z ]");

                        specialtyUrl = reg.Replace(specialtyUrl, string.Empty);
                        var removespace = Regex.Replace(specialtyUrl, " +", " ");

                        specialtyUrl = removespace.Replace(" ", "-").ToLower();

                        DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
                        DynamicContent specItem = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(
                                      IHHConstants.SpecialtyTypeFullName, specialtyUrl);
                        if (specItem == null)
                        {
                            specItem = dynamicModuleManager.CreateDataItem(
                                ManagerHelper.GetResolvedType(IHHConstants.SpecialtyTypeFullName));

                            foreach (var cultureName in IHHConstants.IHHSFCultures)
                            {

                                //var spec_item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)item["Id"], cultureName);

                                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                                if (cultureName == "en")
                                {
                                    // This is how values for the properties are set
                                    specItem.SetString("Title", (string)item["name"], cultureName);
                                    specItem.SetString("TitleToDisplay", (string)item["name"], cultureName);
                                    specItem.SetValue("isHidden", false);
                                }
                                else if (cultureName == "id")
                                {
                                    // This is how values for the properties are set
                                    if ((string)item["id_value"] != string.Empty)
                                    {
                                        specItem.SetString("Title", (string)item["id_value"], cultureName);
                                        specItem.SetString("TitleToDisplay", (string)item["id_value"], cultureName);
                                        specItem.SetValue("isHidden", false);
                                    }
                                    else
                                    {
                                        specItem.SetString("Title", (string)item["name"], cultureName);
                                        specItem.SetString("TitleToDisplay", (string)item["name"], cultureName);
                                        specItem.SetValue("isHidden", false);
                                    }

                                }

                                specItem.SetString("UrlName", specialtyUrl, cultureName);
                                specItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
                                specItem.SetValue("PublicationDate", DateTime.UtcNow);


                                

                                //// Create a version and commit the transaction in order changes to be persisted to data store
                                versionManager.CreateVersion(specItem, false);
                                dynamicModuleManager.Lifecycle.Publish(specItem);
                                
                                specItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
                                versionManager.CreateVersion(specItem, true);
                                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                                DynamicContent checkOutSpecialtyItem = dynamicModuleManager.Lifecycle.CheckOut(specItem) as DynamicContent;
                                DynamicContent checkInSpecialtyItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutSpecialtyItem) as DynamicContent;
                                versionManager.CreateVersion(checkInSpecialtyItem, false);
                                dynamicModuleManager.Lifecycle.Publish(checkInSpecialtyItem);
                                versionManager.CreateVersion(checkInSpecialtyItem, true);
                                TransactionManager.CommitTransaction(transactionName);

                            }
                        }
                        else
                        {
                            Log.Write("Existing Specialty Decteced -- Skip!");

                            foreach (var cultureName in IHHConstants.IHHSFCultures)
                            {

                                //var spec_item = ExternalApiHelper.GetSingleObjWithCulture(url, (string)item["Id"], cultureName);

                                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);
                                if(cultureName == "en")
                                {
                                    // This is how values for the properties are set
                                    specItem.SetString("Title", (string)item["name"], cultureName);
                                    specItem.SetString("TitleToDisplay", (string)item["name"], cultureName);
                                }
                                else if(cultureName == "id")
                                {
                                    // This is how values for the properties are set
                                    if((string)item["id_value"] != string.Empty)
                                    {
                                        specItem.SetString("Title", (string)item["id_value"], cultureName);
                                        specItem.SetString("TitleToDisplay", (string)item["id_value"], cultureName);
                                    }
                                    
                                }

                                //// Create a version and commit the transaction in order changes to be persisted to data store
                                versionManager.CreateVersion(specItem, false);
                                dynamicModuleManager.Lifecycle.Publish(specItem);

                                specItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
                                versionManager.CreateVersion(specItem, true);
                                //// Use lifecycle so that LanguageData and other Multilingual related values are correctly created
                                DynamicContent checkOutSpecialtyItem = dynamicModuleManager.Lifecycle.CheckOut(specItem) as DynamicContent;
                                DynamicContent checkInSpecialtyItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutSpecialtyItem) as DynamicContent;
                                versionManager.CreateVersion(checkInSpecialtyItem, false);
                                dynamicModuleManager.Lifecycle.Publish(checkInSpecialtyItem);
                                versionManager.CreateVersion(checkInSpecialtyItem, true);
                                TransactionManager.CommitTransaction(transactionName);

                            }
                        }

                    }
                }

            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }

        }

        protected void Patch_Doctor_Dummy(object sender, EventArgs e)
        {
            string url = "https://myg03mws013.azurewebsites.net/api/default/images?$filter=ParentId%20eq%20cf26d641-ab66-4354-92d4-ba9142201172";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                decimal count = 0;
                decimal percent = 0;
                foreach (var item in items)
                {

                    ++count;
                    percent = (count / items.Count()) * 100;

                    string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                    Log.Write("Current Progress : " + progress);

                    ImageHelper.translateImage(item);

                }

            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        protected void Patch_Doctor_2(object sender, EventArgs e)
        {
            string url = "https://gleneagles.com.my/api/default/doctors?$expand=MainSpecialties($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialties($select=UrlName)";
            //string url = "https://myg03mws013.azurewebsites.net/api/default/doctors?$filter=HospitalName%20eq%20%27Gleneagles%20Hospital%20Kuala%20Lumpur%27%20&$Top=5&$expand=MainSpecialty($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialty($select=UrlName)";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                decimal count = 0;
                decimal percent = 0;
                foreach (var item in items)
                {
                    string exist = (DoctorHelper.IsDoctorExist((string)item["UrlName"])) ? "Exist" : "New";
                    lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["Title"], exist);

                    ++count;
                    percent = (count / items.Count()) * 100;

                    string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                    Log.Write("Current Progress : " + progress);

                    DoctorHelper.PatchDoctor2(item);
                    

                    var parent = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

                    if (parent != null)
                    {
                        DoctorHelper.PatchDoctorHopital(item, parent);
                    }
                    else
                    {
                        Log.Write("NOT PARENT ");
                    }


                }
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        protected void Patch_Doctor_Pantai_2(object sender, EventArgs e)
        {
            string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";
            try
            {
                // 2, 7, 9
                int[] hospitalsId = new int[] { 2, 7, 9 };

                foreach (int hosId in hospitalsId)
                {
                    var items = ExternalApiHelper.GetDocObjectsFromPostUrl(url, hosId);

                    decimal count = 0;
                    decimal percent = 0;
                    foreach (var item in items)
                    {
                        string docFullUrl = (string)item["profileUrl"];

                        string docUrl = docFullUrl.Split('/').Last();

                        Log.Write("doctor url: " + docUrl);
                        string exist = (DoctorHelper.IsDoctorExist(docUrl)) ? "Exist" : "New";
                        lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["display_name"], exist);

                        ++count;
                        percent = (count / items.Count()) * 100;

                        string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                        Log.Write("Current Progress : " + progress);
                        DoctorHelper.PatchDoctorPantai2(item, hosId);

                        var parent = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, docUrl);

                        if (parent != null)
                        {
                            DoctorHelper.PatchPantaiDoctorHopital(item, parent, hosId);
                        }
                        else
                        {
                            Log.Write("NOT PARENT ");
                        }
                        //int docId = (int)decimal.Parse((string)item["specialties"][0]["doctor_id"]);

                        //var clinichouritems = ExternalApiHelper.GetClinicHourObjectsFromPostUrl(docId, hosId);

                        //if (clinichouritems != null)
                        //{
                        //    foreach (var clinichouritem in clinichouritems)
                        //    {
                        //        DoctorHelper.PatchClinicHourPantai(clinichouritem, docUrl, hosId);
                        //    }
                        //}


                    }
                }

            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        
        protected void Patch_Doctor_PCMC(object sender, EventArgs e)
        {
            //string url = "https://gleneagles.com.my/api/default/doctors?$filter=contains(FullUrl,%27Chee%27)&$expand=MainSpecialties($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialties($select=UrlName)&$Top=5";
            string url = "https://myg03mws013.azurewebsites.net/api/default/doctors?$filter=HospitalName%20eq%20%27Prince%20Court%20Medical%20Centre%27%20&$expand=MainSpecialty($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialty($select=UrlName)";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                decimal count = 0;
                decimal percent = 0;
                foreach (var item in items)
                {
                    string exist = (DoctorHelper.IsDoctorExist((string)item["UrlName"])) ? "Exist" : "New";
                    lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["Name"], exist);

                    ++count;
                    percent = (count / items.Count()) * 100;

                    string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                    Log.Write("Current Progress : " + progress);

                    DoctorHelper.PatchDoctorPCMC(item);


                    var parent = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

                    if (parent != null)
                    {
                        DoctorHelper.PatchDoctorHopitalPCMC(item, parent);
                    }
                    else
                    {
                        Log.Write("NOT PARENT ");
                    }


                }
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        protected void Patch_Doctor_GE_IHH(object sender, EventArgs e)
        {
            //string url = "https://gleneagles.com.my/api/default/doctors?$filter=contains(FullUrl,%27Chee%27)&$expand=MainSpecialties($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialties($select=UrlName)&$Top=5";
            string url = "https://myg03mws013.azurewebsites.net/api/default/doctors?$filter=contains(HospitalName,%20%27Gleneagles%27)&$expand=MainSpecialty($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialty($select=UrlName)";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                decimal count = 0;
                decimal percent = 0;
                foreach (var item in items)
                {
                    string exist = (DoctorHelper.IsDoctorExist((string)item["UrlName"])) ? "Exist" : "New";
                    lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["Name"], exist);

                    ++count;
                    percent = (count / items.Count()) * 100;

                    string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
                    Log.Write("Current Progress : " + progress);

                    DoctorHelper.PatchDoctorPCMC(item);


                    var parent = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

                    if (parent != null)
                    {
                        DoctorHelper.PatchDoctorHopitalPCMC(item, parent);
                    }
                    else
                    {
                        Log.Write("NOT PARENT ");
                    }


                }
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        protected void Patch_Doctor_Image_GE(object sender, EventArgs e)
        {
            string url = "https://gleneagles.com.my/api/default/doctors?$expand=DoctorImage";
            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                foreach (var item in items)
                {
                    var doc = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

                    if (doc != null)
                    {
                        if (doc.GetRelatedItemsCountByField("DoctorImage") == 0 && (string)item["DoctorImage"][0]["Url"] != null)
                        {

                            doc.SetValue("ImageUrl", (string)item["DoctorImage"][0]["Url"]);
                            Log.Write("ImageUrl: " + (string)item["DoctorImage"][0]["Url"]);


                            LibrariesManager doctorImageManager = LibrariesManager.GetManager();
                            var doctorImgUrlName = (string)item["UrlName"];
                            doctorImgUrlName = Regex.Replace(doctorImgUrlName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                            var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                               i.UrlName == doctorImgUrlName);

                            if (ImageItem == null)
                            {
                                var doctorImage = ImageHelper.GetImage((string)item["DoctorImage"][0]["Url"]);
                                ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);

                            }
                            //// Get related item manager

                            var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                                i.UrlName == doctorImgUrlName);

                            if (doctorImageItem != null)
                            {
                                // This is how we relate an item
                                doc.CreateRelation(doctorImageItem, "DoctorImage");
                            }
                            else
                            {
                                Log.Write("Doctor image not found");
                            }
                        }
                        
                    }
                }
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }

        }

        protected void Patch_Doctor_Image_Pantai(object sender, EventArgs e)
        {
            string pantaiurl = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";

            // 2, 7, 9
            int[] hospitalsId = new int[] { 2, 7, 9 };
            foreach (int hosId in hospitalsId)
            {
                var pantaiitems = ExternalApiHelper.GetDocObjectsFromPostUrl(pantaiurl, hosId);

                foreach (var item in pantaiitems)
                {
                    string docFullUrl = (string)item["profileUrl"];

                    string docUrl = docFullUrl.Split('/').Last();

                    var doc = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, docUrl);

                    if (doc != null)
                    {
                        var ProviderName = IHHConstants.IHHDynamicModuleProvider;
                        var transactionName = "Update Doctor Image";
                        var versionManager = VersionManager.GetManager(null, transactionName);

                        DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);
                        
                        if (doc.GetRelatedItemsCountByField("DoctorImage") == 0 && (string)item["photo_src"] != null)
                        {
                            doc.DeleteRelations("DoctorImage");

                            var img_src = (string)item["photo_src"];
                            doc.SetValue("ImageUrl", "https://www.pantai.com.my" + img_src);

                            img_src = img_src.Split('/').Last();
                            img_src = img_src.Split('?').First();
                            var img_extension = img_src.Substring(img_src.LastIndexOf('.'));


                            LibrariesManager doctorImageManager = LibrariesManager.GetManager();

                            var doctorImgUrlName = Regex.Replace(docUrl.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                            //// Get related item manager

                            var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                                i.UrlName == doctorImgUrlName);

                            if (ImageItem == null)
                            {
                                var doctorImage = ImageHelper.GetImage("https://www.pantai.com.my" + (string)item["photo_src"]);
                                ImageHelper.CreateImageWithNativeAPI(Guid.NewGuid(), IHHConstants.DoctorImageLibraryGUID, (string)item["display_name"], doctorImage, docUrl, img_extension);
                            }

                            var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                                i.UrlName == doctorImgUrlName);

                            if (doctorImageItem != null)
                            {
                                // This is how we relate an item
                                doc.CreateRelation(doctorImageItem, "DoctorImage");
                            }
                            else
                            {
                                Log.Write("Doctor image not found");
                            }

                        }
                        
                    }
                }
            }
        }

        protected void Patch_Doctor_Image_PCMC(object sender, EventArgs e)
        {
            string url = "https://www.ihhmalaysia-international.com/api/default/doctors?$filter=contains(HospitalName,%27Prince%27)&$expand=DoctorImage";

            try
            {
                JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
                //string culturename = ManagerHelper.Getsf_culture(url);
                if(items!= null)
                {
                    Log.Write("PCMC DOC NOT NULL");
                    Log.Write("PCMC: "+ items.Count);
                }
                else
                {
                    Log.Write("PCMC DOC NULL");
                }

                foreach (var item in items)
                {
                    var doc = ManagerHelper.GetMasterDynamicContentsByExactlyUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

                    if (doc != null)
                    {
                        Log.Write("PCMC doctor not null");

                        if ((string)item["DoctorImage"][0]["UrlName"] != "" && (string)item["DoctorImage"][0]["UrlName"] != null && doc.GetRelatedItemsCountByField("DoctorImage") == 0)
                        {
                            Log.Write("ImageUrl: " + (string)item["DoctorImage"][0]["UrlName"]);

                            LibrariesManager doctorImageManager = LibrariesManager.GetManager();
                            var doctorImgUrlName = (string)item["DoctorImage"][0]["UrlName"];
                            //doctorImgUrlName = Regex.Replace(doctorImgUrlName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                            var ImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                               i.UrlName == doctorImgUrlName);

                            if (ImageItem == null)
                            {
                                //var doctorImage = ImageHelper.GetImage("https://www.ihhmalaysia-international.com" + (string)item["DoctorImage"][0]["Url"]);
                                //ImageHelper.CreateImageWithNativeAPI((Guid)item["DoctorImage"][0]["Id"], IHHConstants.DoctorImageLibraryGUID, (string)item["Title"], doctorImage, (string)item["UrlName"], (string)item["DoctorImage"][0]["Extension"]);
                                Log.Write("PCMC Doctor image not found: " + (string)item["DoctorImage"][0]["UrlName"]);
                            }
                            //// Get related item manager

                            var doctorImageItem = doctorImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                                i.UrlName == doctorImgUrlName);

                            if (doctorImageItem != null)
                            {
                                // This is how we relate an item
                                doc.CreateRelation(doctorImageItem, "DoctorImage");
                            }
                            else
                            {
                                Log.Write("Doctor image not found");
                            }
                        }

                    }
                    else
                    {
                        Log.Write("PCMC doctor equal null");
                    }
                }

                Log.Write("DONE");
            }
            catch (Exception err)
            {
                Log.Write("Json Err " + err);
            }
        }

        protected void Remove_Empty_Doctor(object sender, EventArgs e)
        {
            var doctors = ManagerHelper.GetMasterDyanamicContents(IHHConstants.DoctorTypeFullName).Where(x => x.GetValue<Lstring>("Name").ToString() == "");
            foreach (var doc in doctors)
            {
                ManagerHelper.DeleteItem(doc);
            }

        }
        //OLD CODE
        //protected void Patch_Doctor(object sender, EventArgs e)
        //{
        //    string url = "https://gleneagles.com.my/api/default/doctors?$expand=MainSpecialties($select=UrlName),%20Hospital($select=UrlName),%20SubSpecialties($select=UrlName)";
        //    try
        //    {
        //        JArray items = ExternalApiHelper.GetObjectsFromUrl(url);
        //        //string culturename = ManagerHelper.Getsf_culture(url);
        //        decimal count = 0;
        //        decimal percent = 0;
        //        foreach (var item in items)
        //        {
        //            string exist = (DoctorHelper.IsDoctorExist((string)item["UrlName"])) ? "Exist" : "New";
        //            lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["Title"], exist);

        //            ++count;
        //            percent = (count / items.Count()) * 100;

        //            string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
        //            Log.Write("Current Progress : " + progress);

        //            DoctorHelper.PatchDoctor(item);

        //            var parent = ManagerHelper.GetMasterDynamicContentsByUrlName(IHHConstants.DoctorTypeFullName, (string)item["UrlName"]);

        //            if (parent != null)
        //            {
        //                string childUrl = "https://gleneagles.com.my/api/default/clinichours?$filter=contains(ItemDefaultUrl,%20%27" + item["UrlName"] + "%27)";

        //                JArray childItem = ExternalApiHelper.GetObjectsFromUrl(childUrl);

        //                if (childItem != null)
        //                {
        //                    foreach (var child in childItem)
        //                    {
        //                        Log.Write((string)item["UrlName"]);
        //                        DoctorHelper.PatchClinicHourGE(child, parent);
        //                        Log.Write("Patch doctor child item : " + (string)child["ItemDefaultUrl"]);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Log.Write("NOT PARENT ");
        //            }


        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write("Json Err " + err);
        //    }
        //}

        //protected void Patch_Clinic_Hour(object sender, EventArgs e)
        //{
        //    string url = "https://gleneagles.com.my/api/default/clinichours";
        //    try
        //    {
        //        JArray items = ExternalApiHelper.GetObjectsFromUrl(url);

        //        decimal count = 0;
        //        decimal percent = 0;
        //        foreach (var item in items)
        //        {
        //            ++count;
        //            percent = (count / items.Count()) * 100;

        //            string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
        //            Log.Write("Current Progress : " + progress);

        //            DoctorHelper.PatchClinicHour(item);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write("Json Err " + err);
        //    }
        //}

        //protected void Patch_OPD_Day(object sender, EventArgs e)
        //{
        //    //string url = "https://gleneagles.com.my/api/default/opddays?$filter=ParentId%20eq%20c088f527-3fda-4eee-b0dd-57cbb4f0e4a2";
        //    string url = "https://gleneagles.com.my/api/default/opddays";
        //    try
        //    {
        //        JArray items = ExternalApiHelper.GetObjectsFromUrl(url);

        //        decimal count = 0;
        //        decimal percent = 0;
        //        foreach (var item in items)
        //        {
        //            ++count;
        //            percent = (count / items.Count()) * 100;

        //            string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
        //            Log.Write("Current Progress : " + progress);

        //            PatchOperationHour(item);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write("Json Err " + err);
        //    }
        //}

        //protected void Show_All_From_Drop_Down(object sender, EventArgs e)
        //{
        //    string value = ShowDataDropDown.SelectedValue;
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        var data = ManagerHelper.GetMasterDyanamicContents(value);
        //        if (data != null)
        //        {
        //            lblDoctor.Text = string.Join("<br/>", data.Select(x => x.GetValue<Lstring>("Title").ToString()).ToList());
        //        }
        //        else
        //        {
        //            lblDoctor.Text = "No Data loaded";
        //        }


        //    }
        //}

        //protected void Delete_Duplicated(object sender, EventArgs e)
        //{
        //    DoctorHelper.RemoveDuplicatedDoctor();
        //    lblDoctor.Text = "Duplicated Doctor Removed! <br/>";


        //}

        //protected void Delete_Clinic_Hour(object sender, EventArgs e)
        //{
        //    var data = ManagerHelper.GetTempDyanamicContents(IHHConstants.DoctorClinicHourTypeFullName);
        //    foreach (var d in data)
        //    {
        //        ManagerHelper.DeleteItem(d);
        //    }
        //}


        //protected void Patch_Doctor_Pantai(object sender, EventArgs e)
        //{
        //    string url = "https://www.pantai.com.my/Webservice/PHWebservice.aspx/LoadDoctorsList";
        //    try
        //    {
        //        // 2, 7, 9
        //        int[] hospitalsId = new int[] { 2, 7, 9 };

        //        foreach (int hosId in hospitalsId)
        //        {
        //            var items = ExternalApiHelper.GetDocObjectsFromPostUrl(url, hosId);

        //            decimal count = 0;
        //            decimal percent = 0;
        //            foreach (var item in items)
        //            {
        //                string docFullUrl = (string)item["profileUrl"];

        //                string docUrl = docFullUrl.Split('/').Last();

        //                Log.Write("doctor url: " + docUrl);
        //                string exist = (DoctorHelper.IsDoctorExist(docUrl)) ? "Exist" : "New";
        //                lblDoctor.Text += string.Format("Title : {0} ({1}) <br/>", (string)item["display_name"], exist);

        //                ++count;
        //                percent = (count / items.Count()) * 100;

        //                string progress = string.Format("{0}/{1} ({2}%)", count, items.Count(), percent);
        //                Log.Write("Current Progress : " + progress);
        //                PatchDoctorPantai(item, hosId);

        //                int docId = (int)decimal.Parse((string)item["specialties"][0]["doctor_id"]);

        //                var clinichouritems = ExternalApiHelper.GetClinicHourObjectsFromPostUrl(docId, hosId);

        //                if (clinichouritems != null)
        //                {
        //                    foreach (var clinichouritem in clinichouritems)
        //                    {
        //                        DoctorHelper.PatchClinicHourPantai(clinichouritem, docUrl, hosId);
        //                    }
        //                }


        //            }
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write("Json Err " + err);
        //    }
        //}

    }
}

