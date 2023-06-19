using System;
using System.Collections.Generic;
using System.Linq;

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.Helpers
{
    public class EnquiryTaskHelper
    {
        public static void RunTask(DynamicContent dynamicContentItem)
        {

            if (IsItemCompleted(dynamicContentItem))
            {
                try
                {
                    if (!ManagerHelper.ItemIsMaster(dynamicContentItem))
                    {

                        SendNotificationEmailToAdmin(dynamicContentItem);

                    }
                }
                catch (Exception ex)
                {
                    Log.Write("Error Sending Appointment creation Notification " + ex);
                }
            }
            //save changes
            ManagerHelper.SaveDynamicContentChanges();
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

            return true;
        }

        private static void SendNotificationEmailToAdmin(DynamicContent dynamicContentItem)
        {
            string fullName = dynamicContentItem.GetValue("FullName").ToString();
            
            string emailAddress = dynamicContentItem.GetValue("Email").ToString();
            string nationality = dynamicContentItem.GetValue("Nationality").ToString();
            string countryCode = dynamicContentItem.GetValue("CountryCode").ToString();
            string contactNumber = dynamicContentItem.GetValue("ContactNumber").ToString();
            string message = dynamicContentItem.GetValue("Message").ToString();

            string itemUrl = FrontendUrlHelper.GetSitefinityItemUrl(
                dynamicContentItem.GetType().FullName, dynamicContentItem.SystemParentId.ToString(),
                dynamicContentItem.OriginalContentId.ToString());

            //string emailString = hospital.GetValue("EmailNotifyEnquiry").ToString();
            //var emails = GleneaglesConstants.EmailNotifyEnquiry.GetValueOrNull(FrontendUrlHelper.GetHospNameFromSrting(hospital.UrlName.ToString())) ?? (new string[] { });

            var emails = EmailHelper.GetMailingListByName(IHHConstants.EmailNotifyEnquiry);

            string template = EmailHelper.GetEmailTemplateByName(IHHConstants.EnquiryEmailToAdminTemplateName);

            if (!string.IsNullOrEmpty(template))
            {
                //replace the variables from the Template
                template = template.Replace("{|FullName|}", fullName);
                template = template.Replace("{|CountryCode|}", EmailHelper.GetCleanStringNumber(countryCode));
                template = template.Replace("{|ContactNumber|}", EmailHelper.GetCleanStringNumber(contactNumber));
                template = template.Replace("{|EmailAddress|}", emailAddress);
                template = template.Replace("{|Nationality|}", nationality);
                template = template.Replace("{|Message|}", message);
                template = template.Replace("{|Url|}", itemUrl);

                string subject = IHHConstants.SubjectNameNewEnquiryFromUser;

                foreach (var email in emails)
                {
                    var to = email.Email;
                    var bcc = new List<string>();
                    var body = template;

                    EmailHelper.SendEmail(to, null, bcc, subject, body, true, null);

                }
            }
        }
    }
}