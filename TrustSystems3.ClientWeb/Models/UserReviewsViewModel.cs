using System.Collections.Generic;
using PagedList;

namespace TrustSystems3.ClientWeb.Models
{
    public class UserReviewsViewModel
    {
        public AspNetUsers User { get; set; }
        public IPagedList<Review> Reviews { get; set; }
        public int ReviewsCount { get; set; }
    }
}