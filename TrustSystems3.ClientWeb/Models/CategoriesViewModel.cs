using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrustSystems3.ClientWeb.Models
{
    public class CategoriesViewModel
    {
        public List<RootCategory> RootCategories { get; set; }
        public IDictionary<int, IDictionary<Companies, float>> RootCompanies { get; set; }
        public IDictionary<RootCategory, IEnumerable<Category>> CategoryDictionary { get; set; }
    }
}