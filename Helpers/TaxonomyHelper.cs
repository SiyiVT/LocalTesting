using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.OpenAccess;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace SitefinityWebApp.Helpers
{
    public class TaxonomyHelper
    {
        public static IQueryable<FlatTaxon> GetTaxonomies(string taxonomyName)
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

            var taxonomies = taxonomyManager.GetTaxa<FlatTaxon>().Where(x => x.Taxonomy.Name == taxonomyName);

            return taxonomies;
        }

        public static ITaxon GetTaxon(Guid id)
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            return taxonomyManager.GetTaxon(id);
        }

        public static void RemoveAndAddTaxa(DynamicContent dynamicContentItem, string fieldName, Guid taxonId)
        {
            if (dynamicContentItem != null)
            {
                RemovedAllTaxaInField(dynamicContentItem, fieldName);
                dynamicContentItem.Organizer.AddTaxa(fieldName, taxonId);
            }

        }

        public static void RemovedAllTaxaInField(DynamicContent dynamicContentItem, string fieldName)
        {
            var existingTaxa = dynamicContentItem.GetValue<IList<Guid>>(fieldName);
            if (existingTaxa != null && existingTaxa.Count() > 0)
            {
                foreach (var taxonId in existingTaxa.ToList())
                {
                    dynamicContentItem.Organizer.RemoveTaxa(fieldName, taxonId);
                }
            }
        }

        public static int GetClassificationCount(DynamicContent dynamicContentItem, string fieldName)
        {
            var existingTaxa = dynamicContentItem.GetValue<IList<Guid>>(fieldName);
            if (existingTaxa != null)
            {
                return existingTaxa.Count();
            }
            return 0;
        }

        public static FlatTaxon GetAFlatTaxon(string Classification, string flatTaxonName)
        {
            TaxonomyManager manager = TaxonomyManager.GetManager();

            var flatTaxon = manager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == Classification && t.Name == flatTaxonName).FirstOrDefault();
            
            return flatTaxon;

        }
    }
}