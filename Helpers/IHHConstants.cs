using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Helpers
{
    public class IHHConstants
    {
        //Provider
        public const string IHHDynamicModuleProvider = "IHHDynamicModuleProvider";

        //sf_culture
        public static readonly string[] IHHSFCultures = { "en", "id" };
        public static readonly string[] GleaglesSFCultures = { "en", "id" };
        public static readonly string[] PantaiCultures = { "en-US", "id-ID" };

        //Hospital
        public const string GKKUrlName = "gleneagles/kota-kinabalu";
        public const string GKLUrlName = "gleneagles/kuala-lumpur";
        public const string GMHUrlName = "gleneagles/medini-johor";
        public const string GPGUrlName = "gleneagles/penang";
        public const string PHAKUrlName = "pantai/ayer-keroh";
        public const string PHKLUrlName = "pantai/kuala-lumpur";
        public const string PHPUrlName = "pantai/penang";
        public const string PCMCUrlName = "princecourt-medicalcentre";

        public const string GKKItemUrlName = "gleneagles-hospital-kota-kinabalu";
        public const string GKLItemUrlName = "gleneagles-hospital-kuala-lumpur";
        public const string GMHItemUrlName = "gleneagles-hospital-medini-johor";
        public const string GPGItemUrlName = "gleneagles-hospital-penang";
        public const string PHAKItemUrlName = "pantai-hospital-ayer-keroh";
        public const string PHKLItemUrlName = "pantai-hospital-kuala-lumpur";
        public const string PHPItemUrlName = "pantai-hospital-penang";
        public const string PCMCItemUrlName = "prince-court-medical-centre";

        //Location State
        public const string SabahUrlName = "sabah";
        public const string KualaLumpurUrlName = "kuala-lumpur";
        public const string JohorUrlName = "johor";
        public const string PenangUrlName = "penang";
        public const string MalaccaUrlName = "malacca";

        //FrontendUrlOption
        public const string HospitalName = "hosp";
        public const string UrlSegment = "urlSegment";
        public const string Location = "loc";

        //Type Name
        public const string PageNodeTypeFullName = "Telerik.Sitefinity.Pages.Model.PageNode";
        public const string ImageTypeFullName = "Telerik.Sitefinity.Libraries.Model.Image";
        public const string HospitalTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.hospital";
        public const string SpecialtyTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Specialities";
        public const string ContactInfoTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Contactinfos";
        public const string ArticleTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Articles";
        public const string ArticleSectionTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Section";

        //Doctors
        //public const string DoctorTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.Doctors";
        //public const string DoctorClinicHourTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.ClinicHour";
        //public const string DoctorOperationHourTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Hospitals.OpdDay";
        public const string DoctorTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Doctors.Ihhdoctor";
        public const string DoctorHospitalTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Doctors.Doctorhospital";
        public const string DoctorClinicHourTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Doctors.Clinichour";


        //Form Type Name
        public const string FormHospitalTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Form.Hospital";
        public const string FormAppointmentTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Form.Appointments";
        public const string FormEnquiriesTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Form.Enquiries";

        //Mailing List Name
        public const string EmailNotifyAppointment = "EmailNotifyAppointment";
        public const string EmailNotifyEnquiry = "EmailNotifyEnquiry";

        //Email Template Name
        public const string AppointmentEmailToClientTemplateName = "AppointmentCreatedNotificationToClient";
        public const string AppointmentEmailToAdminTemplateName = "AppointmentCreatedNotificationToAdmin";

        public const string EnquiryEmailToAdminTemplateName = "EnquiryCreatedNotificationToAdmin";

        //Email Subject
        public const string SubjectNameAppointmentRequestCreatedbyUser = "Appointment Request Created by User";
        public const string SubjectNameAppointmentRequestReceived = "Appointment Request Received";
        public const string SubjectNameAppointmentConfirmed = "Your appointment has been confirmed";
        public const string SubjectNameNewEnquiryFromUser = "New Enquiry from User";
       

        //Media Library GUID
        public static Guid DoctorImageLibraryGUID = new Guid("cf26d641-ab66-4354-92d4-ba9142201172");
        public static Guid HealthReportLibraryGUID = new Guid("b56a6306-7e01-466b-a037-ee3baa39e09f");
        public static Guid HealthReportImageLibraryGUID = new Guid("27274a70-d86c-46ac-ae29-5de85b654917");

        //Public user id - sitefinity system
        public const string PublicUserId = "08c2d4a1-e738-403b-93f8-382dace590a7";

        //Web Service
        public const string WebServiceAppointments = "appointments";
        public const string WebServiceEnquiries = "enquiries";


        //Role and Permission On Module
        public static Dictionary<string, string[]> IHHRolePermissions = new Dictionary<string, string[]>()
        {
            {"Editor", new string[] { "" } },
            {"IHH Customer Services", new string[] { WebServiceAppointments, WebServiceEnquiries} },
            {"Approver", new string[] { "" } },
            {"HR", new string[] {""} },
        };
    }
}