using System;
using System.Linq;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class ReviewLikeRepository: BaseRepository<ReviewLike>
    {
        public ReviewLikeRepository(IUnitOfWork unit)
            : base(unit)
        {
        }


        public bool HasUserLikedReview(string userId, int reviewId)
        {
            return GetAll().Any(l => l.UserId == userId && l.ReviewId == reviewId);
        }

        public void Like(string userId, int reviewId)
        {
            Insert(new ReviewLike
            {
                UserId = userId,
                ReviewId = reviewId,
                DateCreated = DateTime.Now
            });
        }

        public void Dislike(string userId, int reviewId)
        {
            var reviewLike = GetAll().FirstOrDefault(l => l.UserId == userId && l.ReviewId == reviewId);
            if (reviewLike != null)
            {
                Delete(reviewLike);
            }
        }
    }
}