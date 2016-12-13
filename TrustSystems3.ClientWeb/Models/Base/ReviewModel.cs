namespace TrustSystems3.ClientWeb.Models.Base
{
    public class ReviewModel
    {
        public Review Review { get; set; }
        public AspNetUsers User { get; set; }
        public string UserAvatar { get; set; }
        public int UserReviewsCount { get; set; }
    }
}