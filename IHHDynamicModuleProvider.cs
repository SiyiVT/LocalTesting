using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace SitefinityWebApp
{
    public class IHHDynamicModuleProvider : OpenAccessDynamicModuleProvider
    {
        private bool resolvingChildUrlFormat = false;
        public override string GetUrlFormat(ILocatable item)
        {
            var resolvedItem = (DynamicContent)item;
            if (resolvedItem.GetType().FullName == "Telerik.Sitefinity.DynamicTypes.Model.Doctors.Ihhdoctor")
            {
                return "/[MainSpecialty.UrlName]/[UrlName]";
            }

            this.resolvingChildUrlFormat = false;

            return "/[UrlName]";
        }
    }
}