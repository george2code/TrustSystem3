using System;
using System.Collections.Generic;
using System.Linq;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class ReviewRepository: BaseRepository<Review>
    {
        public ReviewRepository(IUnitOfWork unit)
            : base(unit)
        {
        }

        public float GetRatingByCompanyId(int companyId)
        {
            var allRatings = ListReviewsByCompanyId(companyId);
            return (allRatings.Any()) ? (float)Math.Round(allRatings.Sum(r => (float)r.Rating) / allRatings.Count(), 1) : 0;
        }

        public IEnumerable<Review> GetReviewsByCompanyId(int companyId)
        {
            return GetAll().Where(r => r.CompanyId == companyId);
        }

        public int GetReviewsCountByCompanyId(int companyId)
        {
            return GetAll().Count(r => r.CompanyId == companyId);
        }

        public IEnumerable<Review> ListReviewsByCompanyId(int companyId)
        {
            return GetAll().Where(r => r.CompanyId == companyId);
//            return ListAll().Where(r => r.IsComfirmed && r.CompanyId == companyId).OrderByDescending(cr => cr.PublishDate);
        }

        public int GetUserReviewsCount(string userId)
        {
            return GetAll().Count(x => x.UserId == userId);
        }

        public IEnumerable<Review> GetUserReviews(string userId)
        {
            return GetAll().Where(x => x.UserId == userId).OrderByDescending(x => x.DatePublished);
        }
    }
}