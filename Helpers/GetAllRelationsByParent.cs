using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Model;

namespace SitefinityWebApp.Helpers
{  
    public class GetAllRelationsByParent
    {
        public static string TEST = "Testing";
        public static IQueryable<IDataItem> GetRelationsByParent(Guid itemId, string itemProviderName, string itemTypeName, string fieldName)
        {
            ContentLinksManager contentLinksManager = ContentLinksManager.GetManager();

            var linksToRelatedItems = contentLinksManager.GetContentLinks()
                .Where(cl => cl.ParentItemId == itemId &&
                    cl.ParentItemProviderName == itemProviderName &&
                    cl.ParentItemType == itemTypeName &&
                    cl.ComponentPropertyName == fieldName);

            return linksToRelatedItems;

        }
        public static IQueryable<IDataItem> GetRelationsByChild(Guid itemId, string itemType, string itemProviderName, string parentItemTypeFullName)
        {
            ContentLinksManager contentLinksManager = ContentLinksManager.GetManager();

            var links = contentLinksManager.GetContentLinks()
                .Where(cl => cl.ChildItemId == itemId &&
                    cl.ChildItemType == itemType &&
                    cl.ChildItemProviderName == itemProviderName &&
                    cl.ParentItemType == parentItemTypeFullName);

            return links;
        }
    }

}