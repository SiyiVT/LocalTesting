using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Newsletters.Model;

namespace SitefinityWebApp.Helpers
{
    public class EmailHelper
    {
        public static string GetEmailTemplateByName(string templateName)
        {
            var templateBody = NewslettersManager.GetManager().GetMessageBodies().Where(b => b.Name == templateName).FirstOrDefault();
            if (templateBody != null)
                return templateBody.BodyText;
            return null;
        }

        public static List<Subscriber> GetMailingListByName(string mailingName)
        {
            List<Subscriber> subscribers = new List<Subscriber>();

            var mailingList = NewslettersManager.GetManager().GetMailingLists().Where(l => l.Title == mailingName).SingleOrDefault();
            if (mailingList != null)
            {
                subscribers = mailingList.Subscribers().ToList();
            }

            return subscribers;
        }


        public static void SendEmail(string to, List<string> cc, List<string> bcc, string subject, string body, bool isHtml, List<Attachment> items)
        {
            var config = Config.Get<NotificationsConfig>();
            var defaultProfile = config.Profiles[config.DefaultProfiles.Values.FirstOrDefault().ProfileName] as SmtpSenderProfileElement;
            var from = defaultProfile.DefaultSenderEmailAddress;
            using (var smtp = new SmtpClient(defaultProfile.Host, defaultProfile.Port))
            {
                if (defaultProfile.UseAuthentication)
                {
                    smtp.Credentials = new NetworkCredential(defaultProfile.Username, defaultProfile.Password);
                }
                smtp.EnableSsl = defaultProfile.UseSSL;

                //check for morethan one address in to address

                var message = new MailMessage(from, to.Replace(";", ","));

                if (cc != null && cc.Count > 0)
                {
                    foreach (var item in cc)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            if (item.Trim() != string.Empty)
                            {
                                message.CC.Add(new MailAddress(item.Trim()));
                            }
                        }
                    }
                }
                if (bcc != null && bcc.Count > 0)
                {
                    foreach (var item in bcc)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            if (item.Trim() != string.Empty)
                            {
                                message.Bcc.Add(new MailAddress(item.Trim()));
                            }
                        }
                    }
                }

                message.Subject = subject;

                message.Body = body;
                message.IsBodyHtml = isHtml;

                if (items != null)
                {
                    foreach (Attachment item in items)
                    {
                        message.Attachments.Add(item);
                    }
                }

                smtp.Send(message);
            }
        }


        public static string GetCleanStringNumber(string stringNumber)
        {
            string txt = stringNumber;
            txt = txt.Replace("+", string.Empty);
            txt = txt.Replace("-", string.Empty);
            txt = txt.Replace(" ", string.Empty);

            return txt;
        }
    }
}