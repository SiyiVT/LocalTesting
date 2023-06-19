using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using System.IO;
using System.Text.RegularExpressions;

namespace SitefinityWebApp.Helpers
{
    public class AppointmentTasksHelper
    {
        public static void RunCreateImageTask(DynamicContent dynamicContentItem)
        {
            if (dynamicContentItem.GetValue("ImageBase64").ToString() != "" && dynamicContentItem.GetValue("HealthReportURL").ToString() == "")
            {
                Log.Write("Appointment has image");
                CreateSitefinityImage(dynamicContentItem);
            }
            else
            {
                Log.Write("APpoiintment not image base64 value, SKIP");
            }
        }
        public static void RunTask(DynamicContent dynamicContentItem)
        {

            //GetRequestIP
            //Log.Write("Update... IP :" + FrontendUrlHelper.GetIP());
            //dynamicContentItem.SetValue("IPAdress", FrontendUrlHelper.GetIP());

            //set appointment status
            if (IsItemCompleted(dynamicContentItem))
            {
                try
                {
                    if (!ManagerHelper.ItemIsMaster(dynamicContentItem))
                    {
                        
                        PrepareAppointnentSubmittedEmailToPatient(dynamicContentItem);
                        PrepareAppointmentCreatedNotificationEmailToAdmin(dynamicContentItem);
                    }

                }
                catch (Exception ex)
                {
                    Log.Write("Error Sending Appointment creation Notification " + ex);
                }  

            }

            //save changes
            ManagerHelper.SaveDynamicContentChanges();
            //ManagerHelper.PublishItem(dynamicContentItem);

        }


        private static bool IsItemCompleted(DynamicContent dynamicContentItem)
        {
            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("Email").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("FullName").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("ICorPassportNumber").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("DateofBirth").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("FirstAppointmentDate").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("FirstAppointmentTime").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("SecondAppointmentDate").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("SecondAppointmentTime").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("HospitalName").ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(dynamicContentItem.GetValue("AppointmentType").ToString()))
            {
                return false;
            }

            return true;
        }
        private static void CreateSitefinityImage(DynamicContent appointmentItem)
        {
            string imageBase64 = "";
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("ImageBase64").ToString()))
            {
                imageBase64 = appointmentItem.GetValue("ImageBase64").ToString();
            }

            
            Log.Write("Appointment convert image");

            byte[] binaryData = Convert.FromBase64String(imageBase64);

            // Create a memory stream from the byte array
            MemoryStream imgStream = new MemoryStream(binaryData);

            string ImageTitle = appointmentItem.GetValue("FullName").ToString() +"_"+ DateTime.Now.ToString("dd-MM-yy_HH:mm");
            string ImageUrl = Regex.Replace(ImageTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            LibrariesManager apptImageManager = LibrariesManager.GetManager();

            var CheckImageItem = apptImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                           i.UrlName.Contains(ImageUrl));

            if(CheckImageItem == null)
            {
                ImageHelper.CreateImageWithNativeAPI(Guid.NewGuid(), IHHConstants.HealthReportImageLibraryGUID, ImageTitle, imgStream, ImageTitle, ".jpg");
            }
           
            var ImageItem = apptImageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master &&
                           i.UrlName.Contains(ImageUrl));

            if (ImageItem != null && appointmentItem.GetValue("HealthReportURL").ToString() == "")
            {
                var ProviderName = IHHConstants.IHHDynamicModuleProvider;
                var transactionName = "Update Appointment Health Report Url";
                var versionManager = VersionManager.GetManager(null, transactionName);

                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(ProviderName, transactionName);

                var cultureName = "en";
                Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);

                var healthReportUrl = FrontendUrlHelper.GenerateSiteUrl(ImageItem.ResolveMediaUrl());
                var healthReportID = ImageItem.Id.ToString();
                appointmentItem.SetValue("HealthReportURL", healthReportUrl);
                appointmentItem.SetValue("HealthReportID", healthReportID);

            }
        }

        
        private static void PrepareAppointnentSubmittedEmailToPatient(DynamicContent appointmentItem)
        {
            string fullName = appointmentItem.GetValue("FullName").ToString();
            string firstAppointmentDate = appointmentItem.GetValue("FirstAppointmentDate").ToString();
            string firstAppointmentTime = appointmentItem.GetValue("FirstAppointmentTime").ToString();
            string secondAppointmentDate = appointmentItem.GetValue("SecondAppointmentDate").ToString();
            string secondAppointmentTime = appointmentItem.GetValue("SecondAppointmentTime").ToString();
            string emailAddress = appointmentItem.GetValue("Email").ToString();

            string doctorName = string.Empty;
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("DoctorName").ToString()))
            {
                doctorName = appointmentItem.GetValue("DoctorName").ToString();
            }

            string hospital = appointmentItem.GetValue("HospitalName").ToString();
            string travelGuideUrl = FrontendUrlHelper.GenerateSiteUrl(FrontendUrlHelper.GetHospNameFromString(hospital) + "/travel-guide");

            string template = EmailHelper.GetEmailTemplateByName(IHHConstants.AppointmentEmailToClientTemplateName);
            if (!string.IsNullOrEmpty(template))
            {
                //replace the variables from the Template
                template = template.Replace("{|Appointment.FullName|}", fullName);
                
                template = template.Replace("{|Appointment.HospitalName|}", hospital);

                template = template.Replace("{|Appointment.TravelGuideUrl|}", travelGuideUrl);

                string subject = IHHConstants.SubjectNameAppointmentRequestReceived;
                var to = emailAddress;
                var bcc = new List<string>();
                var body = template;
                try
                {
                    EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);
                }
                catch (Exception ex)
                {
                    Log.Write("Error Sending Appointment creation receipt " + ex);
                }
            }

        }

        public static void PrepareAppointmentCreatedNotificationEmailToAdmin(DynamicContent appointmentItem)
        {
            string fullName = appointmentItem.GetValue("FullName").ToString();
            string gender = appointmentItem.GetValue("Gender").ToString();
            string dateOfBirth = appointmentItem.GetValue("DateofBirth").ToString();
            string nationality = appointmentItem.GetValue("Nationality").ToString();
            string iCorPassportNumber = appointmentItem.GetValue("ICorPassportNumber").ToString();
            string emailAddress = appointmentItem.GetValue("Email").ToString();
            string countryCode = appointmentItem.GetValue("CountryCode").ToString();
            string contactNumber = appointmentItem.GetValue("ContactNumber").ToString();

            string countryCode2 = string.Empty;
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("CountryCode2").ToString()))
            {
                countryCode2 = appointmentItem.GetValue("CountryCode2").ToString();
            }
           
            string contactNumber2 = string.Empty; 
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("ContactNumber2").ToString()))
            {
                contactNumber2 = appointmentItem.GetValue("ContactNumber2").ToString();
            }

            string appointmentType = appointmentItem.GetValue("AppointmentType").ToString();
            string hospital = appointmentItem.GetValue("HospitalName").ToString();
            string currentMedicalConditionsSymptoms = appointmentItem.GetValue("CurrentMedicalConditionsSymptoms").ToString();
            string firstAppointmentDate = appointmentItem.GetValue("FirstAppointmentDate").ToString();
            string firstAppointmentTime = appointmentItem.GetValue("FirstAppointmentTime").ToString();
            string secondAppointmentDate = appointmentItem.GetValue("SecondAppointmentDate").ToString();
            string secondAppointmentTime = appointmentItem.GetValue("SecondAppointmentTime").ToString();
            
            string doctorName = "N/A";
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("DoctorName").ToString()))
            {
                doctorName = appointmentItem.GetValue("DoctorName").ToString();
            }

            string specialtyName = "N/A";
            if (!string.IsNullOrEmpty(appointmentItem.GetValue("SpecialtyName").ToString()))
            {
                specialtyName = appointmentItem.GetValue("SpecialtyName").ToString();
            }

            string healthReportUrl = appointmentItem.GetValue("HealthReportURL").ToString();
            string healthReportId = appointmentItem.GetValue("HealthReportID").ToString();


            string itemUrl = FrontendUrlHelper.GetSitefinityItemUrl(
                appointmentItem.GetType().FullName, appointmentItem.SystemParentId.ToString(),
                appointmentItem.OriginalContentId.ToString());


            var emails = EmailHelper.GetMailingListByName(IHHConstants.EmailNotifyAppointment);

            string template = EmailHelper.GetEmailTemplateByName(IHHConstants.AppointmentEmailToAdminTemplateName);

            if (!string.IsNullOrEmpty(template))
            {
                //replace the variables from the Template
                template = template.Replace("{|FullName|}", fullName);
                template = template.Replace("{|Gender|}", gender);
                template = template.Replace("{|DateofBirth|}", dateOfBirth);
                template = template.Replace("{|Nationality|}", nationality);
                template = template.Replace("{|ICorPassportNumber|}", iCorPassportNumber);
                template = template.Replace("{|EmailAddress|}", emailAddress);
                template = template.Replace("{|CountryCode|}", EmailHelper.GetCleanStringNumber(countryCode));
                template = template.Replace("{|ContactNumber|}", EmailHelper.GetCleanStringNumber(contactNumber));
                template = template.Replace("{|CountryCode2|}", EmailHelper.GetCleanStringNumber(countryCode2));
                template = template.Replace("{|ContactNumber2|}", EmailHelper.GetCleanStringNumber(contactNumber2));

                template = template.Replace("{|AppointmentType|}", appointmentType);
                template = template.Replace("{|FirstAppointmentDate|}", firstAppointmentDate);
                template = template.Replace("{|FirstAppointmentTime|}", firstAppointmentTime);
                template = template.Replace("{|SecondAppointmentDate|}", secondAppointmentDate);
                template = template.Replace("{|SecondAppointmentTime|}", secondAppointmentTime);

                template = template.Replace("{|Hospital|}", hospital);
                template = template.Replace("{|CurrentMedicalConditionsSymptoms|}", currentMedicalConditionsSymptoms);
                template = template.Replace("{|Doctor|}", doctorName);
                template = template.Replace("{|Specialty|}", specialtyName);
                //template = template.Replace("{|HealthReportUrl|}", healthReportUrl);
                template = template.Replace("{|Url|}", itemUrl);

                //string subject = (!string.IsNullOrEmpty(subjectLine)) ? subjectLine : GleneaglesConstants.SubjectNameAppointmentRequestCreatedbyUser;
                string subject = IHHConstants.SubjectNameAppointmentRequestCreatedbyUser;

                List<Attachment> attachments = new List<Attachment>();

                if (!String.IsNullOrEmpty(healthReportId) && !String.IsNullOrEmpty(healthReportUrl))
                {
                    var librariesManager = LibrariesManager.GetManager();

                    Document document = null;
                    Image image = null;

                    if (healthReportUrl.Contains("/docs/"))
                    {
                        document = librariesManager.GetDocument(Guid.Parse(healthReportId));

                        if (document != null)
                        {
                            using (var stream = librariesManager.Download(document))
                            {
                                Attachment attachment = new Attachment(stream, document.Title, document.MimeType);
                                attachments.Add(attachment);

                               
                                foreach (var email in emails)
                                {
                                    var to = email.Email;
                                    var bcc = new List<string>();
                                    var body = template;

                                    try
                                    {
                                        EmailHelper.SendEmail(to, null, bcc, subject, body, true, (attachments.Count > 0 ? attachments : null));

                                        //Mark the item to be deleted.
                                        //librariesManager.DeleteDocument(document);

                                        ////Save the changes.
                                        //librariesManager.SaveChanges();

                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Write("Error Sending email with attachment" + ex);
                                    }
                                }
                            }
                        }
                    }
                    else if(healthReportUrl.Contains("/images/"))
                    {
                        image = librariesManager.GetImage(Guid.Parse(healthReportId));

                        if (image != null)
                        {
                            using (var stream = librariesManager.Download(image))
                            {
                                Attachment attachment = new Attachment(stream, image.Title, image.MimeType);
                                attachments.Add(attachment);

                                foreach (var email in emails)
                                {
                                    var to = email.Email;
                                    var bcc = new List<string>();
                                    var body = template;

                                    try
                                    {
                                        EmailHelper.SendEmail(to, null, bcc, subject, body, true, (attachments.Count > 0 ? attachments : null));

                                        //Mark the item to be deleted.
                                        //librariesManager.DeleteDocument(document);

                                        ////Save the changes.
                                        //librariesManager.SaveChanges();

                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Write("Error Sending email with attachment" + ex);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var email in emails)
                        {
                            var to = email.Email;
                            var bcc = new List<string>();
                            var body = template;

                            try
                            {
                                EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);

                                //Mark the item to be deleted.
                                //librariesManager.DeleteDocument(document);

                                ////Save the changes.
                                //librariesManager.SaveChanges();

                            }
                            catch (Exception ex)
                            {
                                Log.Write("Error Sending email with attachment" + ex);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var email in emails)
                    {
                        var to = email.Email;
                        var bcc = new List<string>();
                        var body = template;

                        try
                        {
                            EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);

                            //Mark the item to be deleted.
                            //librariesManager.DeleteDocument(document);

                            ////Save the changes.
                            //librariesManager.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            Log.Write("Error Sending email with attachment" + ex);
                        }
                    }
                }
            }

        }

        //private static void PrepareAppointnentConfirmationEmailToPatient(DynamicContent appointmentItem)
        //{
        //    string fullName = appointmentItem.GetValue("FullName").ToString();
        //    string patientSalutation = appointmentItem.GetValue("Salutation").ToString();
        //    string patientName = string.Format("{0} {1}", patientSalutation, fullName);
        //    string appointmentDate = appointmentItem.GetValue("AppointmentDate").ToString();
        //    string appointmentTime = appointmentItem.GetValue("AppointmentTime").ToString();
        //    string emailAddress = appointmentItem.GetValue("EmailAddress").ToString();

        //    string appointmentDateTime = string.Empty;
        //    if (appointmentItem.GetValue("ConfirmationDateTime") != null && !string.IsNullOrEmpty(appointmentItem.GetValue("ConfirmationDateTime").ToString()))
        //    {
        //        appointmentDateTime = appointmentItem.GetValue("ConfirmationDateTime").ToString();
        //    }
        //    else
        //    {
        //        appointmentDateTime = string.Format("{0} {1} (first come first serve basis)", appointmentDate, appointmentTime);
        //    }

        //    var doctor = appointmentItem.GetRelatedItems<DynamicContent>("Doctor").FirstOrDefault();
        //    string doctorName = string.Empty;
        //    if (doctor != null)
        //    {
        //        doctorName = string.Format("{0} {1}", doctor.GetValue("Salutation").ToString(), doctor.GetValue("Title").ToString());
        //    }

        //    var hospital = appointmentItem.GetRelatedItems<DynamicContent>("Hospital").FirstOrDefault();
        //    string mapLink = hospital.GetValue("GoogleMapBtnLink").ToString();
        //    string location = hospital.GetValue("Location").ToString();
        //    string hospitalName = hospital.GetValue("Title").ToString();


        //    var hospitalContactInfos = ManagerHelper.GetChildItems(hospital, "Hospitals", "Contactinfo").ToList();
        //    var telephoneObj = hospitalContactInfos.Where(c => c.UrlName.ToString().Contains("general-line")).FirstOrDefault();
        //    var whatAppsObj = hospitalContactInfos.Where(c => c.UrlName.ToString().Contains("whatsapp-line")).FirstOrDefault();
        //    var aAndEObj = hospitalContactInfos.Where(c => c.UrlName.ToString().Contains("ambulance-emergency")).FirstOrDefault();

        //    string emptyString = string.Empty;

        //    string telephone = (telephoneObj != null) ? telephoneObj.GetValue("CTALink").ToString() : emptyString;
        //    string whatApps = (whatAppsObj != null) ? whatAppsObj.GetValue("CTALink").ToString() : emptyString;
        //    string aAndE = (aAndEObj != null) ? aAndEObj.GetValue("CTALink").ToString() : emptyString;

        //    string template = EmailHelper.GetEmailTemplateByName(IHHConstants.AppointConfirmationEmailTemplateName);

        //    if (!string.IsNullOrEmpty(template))
        //    {
        //        //replace the variables from the Template
        //        template = template.Replace("{|Appointment.FullName|}", patientName);
        //        template = template.Replace("{|Appointment.DoctorName|}", doctorName);
        //        template = template.Replace("{|Appointment.HospitalName|}", hospitalName);
        //        template = template.Replace("{|Appointment.DateTime|}", appointmentDateTime);
        //        template = template.Replace("{|Hospital.Location|}", location);
        //        template = template.Replace("{|Hospital.MapLink|}", mapLink);
        //        template = template.Replace("{|Hospital.Enquiry|}", emptyString);
        //        template = template.Replace("{|Hospital.WhatsApp|}", EmailHelper.GetCleanStringNumber(whatApps));
        //        template = template.Replace("{|Hospital.Telephone|}", EmailHelper.GetCleanStringNumber(telephone));
        //        template = template.Replace("{|Hospital.AAndE|}", EmailHelper.GetCleanStringNumber(aAndE));
        //        template = template.Replace("{|Hospital.Url|}", FrontendUrlHelper.GetHospitalUrl(hospitalName));

        //        string subject = IHHConstants.SubjectNameAppointmentConfirmed;
        //        var to = emailAddress;
        //        var bcc = new List<string>();
        //        var body = template;
        //        try
        //        {
        //            EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Write("Error Sending Appointment confirmation email " + ex);
        //        }
        //    }

        //    //EmailHelper.SendAppointmentConfirmationEmailToPatient(
        //    //    patientName, appointmentDate, appointmentTime, doctorName, emailAddress, mapLink, telephone, whatApps, emptyString, aAndE,location,
        //    //    hospitalName,null, null);


        //}
        private static bool IsConfirmationMailSent(DynamicContent dynamicContentItem)
        {
            return dynamicContentItem.GetValue<bool>("ConfirmationMailSent");
        }

        private static bool IsApproved(DynamicContent dynamicContentItem)
        {
            return dynamicContentItem.GetValue<bool>("IsApproved");
        }

        public static void RunAdminTask(DynamicContent dynamicContentItem)
        {

            if (dynamicContentItem.GetValue("HasBeenAttendedBy") != null &&
                string.IsNullOrEmpty(dynamicContentItem.GetValue("HasBeenAttendedBy").ToString()) &&
                 !RoleHelper.IsAdminstrator())
            {
                string adminName = UserHelper.GetCurrentUserNickname();
                dynamicContentItem.SetValue("HasBeenAttendedBy", adminName);
            }

        }

        //public static void SendReminderEmailToPatient()
        //{
        //    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
        //    var incompleteAppointments = dynamicModuleManager.GetDataItems(ManagerHelper.GetResolvedType(GleneaglesConstants.AppointmentTypeFullName))
        //        .Where(w => w.DateCreated > DateTime.Now.AddDays(1) && w.DateCreated < DateTime.Now && !w.GetValue<bool>("ReminderMailSent")).ToList();

        //    foreach (var item in incompleteAppointments)
        //    {
        //        if (item.Organizer.TaxonExists("appointmentstatuses", new Guid(GleneaglesConstants.AppointmentStatusIncompleteId)) &&
        //            item.GetValue("Email") != null && !string.IsNullOrEmpty(item.GetValue("Email").ToString()))
        //        {
        //            var to = item.GetValue("Email").ToString();
        //            var bcc = new List<string>();
        //            var subject = "subject";
        //            var body = "testing";
        //            try
        //            {
        //                EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Write("Error Sending email " + ex);
        //            }
        //        }
        //    }

        //}

        //public static void PatchAppointmentData()
        //{
        //    var dynamicModuleManager = ManagerHelper.GetModuleManager();

        //    Type type = TypeResolutionService.ResolveType(IHHConstants.AppointmentTypeFullName);

        //    var myCollection = dynamicModuleManager.GetDataItems(type).
        //        Where(w => w.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);

        //    var items = myCollection.ToList().Where(a => a.GetValue("HospitalName") == null).Take(1000).ToList();

        //    var versionManager = VersionManager.GetManager(null, "");

        //    foreach (var appointmentItem in items)
        //    {

        //        var doctor = ManagerHelper.GetRelatedItems(appointmentItem, "Doctor");
        //        var specialty = ManagerHelper.GetRelatedItems(appointmentItem, "Specialty");
        //        var hosp = ManagerHelper.GetRelatedItems(appointmentItem, "Hospital");

        //        if (doctor.Count > 0)
        //        {
        //            appointmentItem.SetValue("DoctorName", doctor.First().GetValue("Title").ToString());
        //        }

        //        if (specialty.Count > 0)
        //        {
        //            appointmentItem.SetValue("SpecialtyName", specialty.First().GetValue("Title").ToString());
        //        }

        //        if (hosp.Count > 0)
        //        {
        //            appointmentItem.SetValue("HospitalName", hosp.First().GetValue("Title").ToString());
        //        }

        //        appointmentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");


        //        // Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(appointmentItem, false);

        //        // We can now call the following to publish the item
        //        ILifecycleDataItem publishedAppointmentItem = dynamicModuleManager.Lifecycle.Publish(appointmentItem);

        //        // You need to set appropriate workflow status
        //        appointmentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

        //        // Create a version and commit the transaction in order changes to be persisted to data store
        //        versionManager.CreateVersion(appointmentItem, true);


        //        // Commit the transaction in order for the items to be actually persisted to data store
        //        TransactionManager.CommitTransaction("");
        //    }
        //}
    }
}