using System.Collections.Generic;

namespace TrustSystems3.ClientWeb.Models
{
    public class CategoryCompaniesViewModel
    {
        public string CategoryName { get; set; }
        public int CurrentRootCategoryId { get; set; }
        public int CurrentCategoryId { get; set; }
        public List<RootCategory> RootCategories { get; set; }
        public List<Category> SubCategories { get; set; } 
        public Dictionary<Companies, float> CategoryCompanies { get; set; } 
    }
}