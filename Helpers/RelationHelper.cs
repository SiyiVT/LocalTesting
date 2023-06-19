using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Libraries.Model;


namespace SitefinityWebApp.Helpers
{
    public class RelationHelper
    {
        public static void CreateRelation(DynamicContent item, string relatedItemFieldName, DynamicContent relatedItem)
        {
            if (item != null)
            {
                item.CreateRelation(relatedItem, relatedItemFieldName);
            }
        }

        public static void RemoveAndCreateRelation(DynamicContent item, string relatedItemFieldName, DynamicContent relatedItem)
        {
            if (item != null)
            {
                //remove the targeted relation before adding
                DeleteAllRelation(item, relatedItemFieldName);
                item.CreateRelation(relatedItem, relatedItemFieldName);
            }
        }

        public static void RemoveAndCreateRelation(DynamicContent item, string relatedItemFieldName, Document relatedItem)
        {
            if (item != null)
            {
                //remove the targeted relation before adding
                DeleteAllRelation(item, relatedItemFieldName);
                item.CreateRelation(relatedItem, relatedItemFieldName);
            }
        }

        public static void DeleteAllRelation(DynamicContent item, string relatedItemFieldName)
        {
            if (item != null)
            {
                var relatedItems = item.GetRelatedItems<DynamicContent>(relatedItemFieldName);
                if (relatedItems != null && relatedItems.Count() > 0)
                {
                    foreach (var relatedItem in relatedItems)
                    {
                        item.DeleteRelation(relatedItem, relatedItemFieldName);
                    }
                }
            }
        }
    }
}