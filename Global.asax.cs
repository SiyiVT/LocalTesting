using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Events;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using SitefinityWebApp.Helpers;
using Telerik.Sitefinity.Web.Events;
using System.Threading;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Workflow;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            EventHub.Subscribe<IDynamicContentCreatingEvent>(evt => DynamicContentEventHelper.ItemCreatingEventHandler(evt));
            EventHub.Subscribe<IDynamicContentCreatedEvent>(evt => DynamicContentEventHelper.ItemCreatedEventHandler(evt));
            EventHub.Subscribe<IDynamicContentUpdatingEvent>(evt => DynamicContentEventHelper.ItemtUpdatingEventHandler(evt));
            EventHub.Subscribe<IDynamicContentUpdatedEvent>(evt => DynamicContentEventHelper.ItemUpdatedEventHandler(evt));
            
            EventHub.Subscribe<IDataEvent>(evt => IDataEventHelper.DataEventHandler(evt));

            //custom workflow
            //ObjectFactory.Container.RegisterType<IWorkflowDefinitionResolver, CustomWorkflowDefinitionResolver>(new ContainerControlledLifetimeManager());
            EventHub.Subscribe<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler);


            // Register IHH Resource classes
            Res.RegisterResource<IHHLabelResources>();

        }

        private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent @event)
        {
            var page = @event.Page;

            HtmlPageHelper.AddDefaultOGImage(page);
            HtmlPageHelper.UpdateCustomOGUrl(page);
            HtmlPageHelper.AddHospitalTitle(page);
            HtmlPageHelper.AddCustomCanonicalTag(page);
            //HtmlPageHelper.AddCustomMetaDesc(page, "appointment", "DoctorApptMetaDescTemplate", GleneaglesConstants.DoctorTypeFullName);
            //HtmlPageHelper.AddCustomMetaDesc(page, "doctors", "DoctorMetaDescTemplate", GleneaglesConstants.DoctorTypeFullName);
            //HtmlPageHelper.AddCustomMetaDesc(page, "careers", "CareerMetaDescTemplate", GleneaglesConstants.CareersTypeFullName);
            //HtmlPageHelper.AddCustomMetaDesc(page, "packages-promotions", "PackagesPromoItemMetaDesc", GleneaglesConstants.PackageAndPromotionTypeFullName);
            //HtmlPageHelper.AddCustomMetaDesc(page, "events", "PackagesPromoItemMetaDesc", GleneaglesConstants.EventTypeFullName);
            //HtmlPageHelper.AddCustomMetaDesc(page, "medical-specialties", "MedicalSpecialtyItemMetaDesTemplate", GleneaglesConstants.SpecialtyTypeFullName);

        }
    }
}