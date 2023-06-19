using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Events;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;


namespace SitefinityWebApp.Helpers
{
    public class DynamicContentEventHelper
    {
        public static void ItemCreatingEventHandler(IDynamicContentCreatingEvent eventInfo)
        {
            var userId = eventInfo.UserId;
            var dynamicContentItem = eventInfo.Item;
            
        }

        public static void ItemCreatedEventHandler(IDynamicContentCreatedEvent eventInfo)
        {
            var userId = eventInfo.UserId;
            var createdDate = eventInfo.CreationDate;
            var dynamicContentItem = eventInfo.Item;
            var visible = eventInfo.Visible;

            if (!IsPublicUser(userId.ToString()))
            {
                switch (dynamicContentItem.GetType().FullName)
                {
                    case IHHConstants.DoctorHospitalTypeFullName:
                        DoctorHelper.UpdateDoctorHospitalClassification(dynamicContentItem);
                        DoctorHelper.UpdateDoctorHospitalTitle(dynamicContentItem);
                        break;
                }

                StandardizeUrlName(dynamicContentItem);
                SetDefaultSortNumber(dynamicContentItem);

                ManagerHelper.SaveDynamicContentChanges();
            }
            else if (IsPublicUser(userId.ToString()))
            {
                SetAppointmentImage(dynamicContentItem);

                ManagerHelper.SaveDynamicContentChanges();
            }

        }

        public static void ItemtUpdatingEventHandler(IDynamicContentUpdatingEvent eventInfo)
        {
            var userId = eventInfo.UserId;
            var dynamicContentItem = eventInfo.Item;

            //ValidateData(dynamicContentItem);

        }

        public static void ItemUpdatedEventHandler(IDynamicContentUpdatedEvent eventInfo)
        {
            var userId = eventInfo.UserId;
            var modificationDate = eventInfo.ModificationDate;
            var dynamicContentItem = eventInfo.Item;
            var visible = eventInfo.Visible;

            if (IsPublicUser(userId.ToString()))
            {
                RunPublicTaskByDynamiContent(dynamicContentItem);
            }

            else
            {
                RunAdminTaskByDynamiContent(dynamicContentItem);
                RunAutomation(dynamicContentItem);

            }

        }

        private static void RunPublicTaskByDynamiContent(DynamicContent dynamicContentItem)
        {
            switch (dynamicContentItem.GetType().FullName)
            {
                //case IHHConstants.AppointmentTypeFullName:
                //    AppointmentTasksHelper.RunTask(dynamicContentItem);
                //    break;

                //case IHHConstants.EnquiriesTypeFullName:
                //    EnquiryTaskHelper.RunTask(dynamicContentItem);
                //    break;

                case IHHConstants.FormAppointmentTypeFullName:
                    AppointmentTasksHelper.RunTask(dynamicContentItem);
                    break;

                case IHHConstants.FormEnquiriesTypeFullName:
                    EnquiryTaskHelper.RunTask(dynamicContentItem);
                    break;

            }
        }

        private static void RunAdminTaskByDynamiContent(DynamicContent dynamicContentItem)
        {
            switch (dynamicContentItem.GetType().FullName)
            {
                case IHHConstants.FormAppointmentTypeFullName:
                    AppointmentTasksHelper.RunAdminTask(dynamicContentItem);
                    break;
                case IHHConstants.FormEnquiriesTypeFullName:
                    EnquiryTaskHelper.RunAdminTask(dynamicContentItem);
                    break;
                case IHHConstants.DoctorTypeFullName:
                    DoctorHelper.UpdateDoctorDataRunTask(dynamicContentItem);
                    break;
                case IHHConstants.DoctorHospitalTypeFullName:
                    DoctorHelper.UpdateDoctorHospitalClassification(dynamicContentItem);
                    break;
                    //case IHHConstants.DoctorHospitalTypeFullName:
                    //    DoctorHelper.UpdateDoctorHospitalClassification(dynamicContentItem);
                    //    DoctorHelper.UpdateDoctorHospitalTitle(dynamicContentItem);
                    //    break;
            }

        }
        private static void SetAppointmentImage(DynamicContent dynamicContentItem)
        {
            switch (dynamicContentItem.GetType().FullName)
            {
               
                case IHHConstants.FormAppointmentTypeFullName:
                    AppointmentTasksHelper.RunCreateImageTask(dynamicContentItem);
                    break;

            }
        }
        private static void RunAutomation(DynamicContent dynamicContentItem)
        {
            switch (dynamicContentItem.GetType().FullName)
            {
                //case IHHConstants.FacilityTypeFullName:
                //    RenameUrlNameWithHospital(dynamicContentItem);
                //    break;

                //case IHHConstants.HospitalSpecialtyTypeFullName:
                //    RenameUrlNameWithHospital(dynamicContentItem);
                //    //SpecialtyHelper.UpdateHospitalSpecialyList(dynamicContentItem);
                //    break;

                case IHHConstants.DoctorTypeFullName:
                    DoctorHelper.UpdateMetaSetting(dynamicContentItem);
                    //DoctorHelper.GenerateDetailPageUrl(dynamicContentItem);
                    break;
                //case IHHConstants.DoctorLeaveTypeFullName:
                //    DoctorHelper.SetLeaveTitle(dynamicContentItem);
                //    break;
            }
            StandardizeUrlName(dynamicContentItem);
            SetDefaultSortNumber(dynamicContentItem);
        }

        private static bool IsPublicUser(string id)
        {
            //user is 'Sitefinity System'
            if (id != null && id == IHHConstants.PublicUserId)
            {
                return true;
            }
            return false;
        }

        private static void StandardizeUrlName(DynamicContent dynamicContentItem)
        {
            Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo("en");
            var en_UrlName = dynamicContentItem.GetString("UrlName");
            foreach (var culture in IHHConstants.IHHSFCultures)
            {
                if (culture != "en")
                {
                    if (dynamicContentItem.PublishedTranslations.Contains(culture))
                    {
                        dynamicContentItem.SetString("UrlName", en_UrlName, culture);
                    }
                }

            }
        }

        private static void SetDefaultSortNumber(DynamicContent dynamicContentItem)
        {
            if (dynamicContentItem.DoesFieldExist("SortNumber") && dynamicContentItem.GetValue("SortNumber") == null)
            {
                dynamicContentItem.SetValue("SortNumber", 999);
            }
        }

        //private static void ValidateData(DynamicContent dynamicContentItem)
        //{
        //    if (dynamicContentItem.DoesFieldExist("Summary"))
        //    {
        //        if (dynamicContentItem.GetValue("Summary") != null && dynamicContentItem.GetValue("Summary").ToString().Length > 200)
        //        {
        //            throw new WebProtocolException(HttpStatusCode.InternalServerError, "Summary content too long!. Maximum 200 characters", null);
        //        }

        //    }
        //}

        private static void RenameUrlNameWithHospital(DynamicContent dynamicContentItem)
        {
            if (dynamicContentItem.DoesFieldExist("Hospital") && dynamicContentItem.GetRelatedItemsCountByField("Hospital") > 0 ||
               dynamicContentItem.SystemParentItem != null &&
               dynamicContentItem.SystemParentItem.GetType().FullName == IHHConstants.HospitalTypeFullName)
            {
                DynamicContent hospital = dynamicContentItem.GetRelatedItems<DynamicContent>("Hospital").FirstOrDefault();
                if (hospital == null && dynamicContentItem.SystemParentItem != null &&
                    dynamicContentItem.SystemParentItem.GetType().FullName == IHHConstants.HospitalTypeFullName)
                {
                    hospital = dynamicContentItem.SystemParentItem;
                }

                if (hospital != null)
                {
                    string hosp_name = FrontendUrlHelper.GetHospNameFromString(hospital.UrlName);
                    string urlName = dynamicContentItem.UrlName.ToString();
                    urlName = string.Format("{0}-{1}", urlName.Replace("-" + hosp_name, string.Empty), hosp_name);
                    dynamicContentItem.SetString("UrlName", urlName);
                }
            }
        }


    }
}