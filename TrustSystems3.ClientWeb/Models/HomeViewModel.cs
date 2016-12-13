using System.Collections.Generic;
using TrustSystems3.ClientWeb.Models.Base;

namespace TrustSystems3.ClientWeb.Models
{
    public class HomeViewModel
    {
        public List<ReviewModel> LatestReviews { get; set; }
        public ReviewModel ReviewOfTheDay { get; set; }
        public List<RootCategory> RootCategoryList { get; set; } 
    }
}