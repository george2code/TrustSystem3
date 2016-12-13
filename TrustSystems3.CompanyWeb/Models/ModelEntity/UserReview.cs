namespace TrustSystems3.CompanyWeb.Models.ModelEntity
{
    public class UserReview
    {
        public Review Review { get; set; }
        public AspNetUsers User { get; set; }
    }
}