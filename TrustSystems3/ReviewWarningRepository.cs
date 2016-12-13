using System;
using System.Linq;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3
{
    public class ReviewWarningRepository: BaseRepository<ReviewWarning>
    {
        public ReviewWarningRepository(IUnitOfWork unit)
            : base(unit)
        {
        }


        public bool AddWarning(string userId, int reviewId, string text)
        {
            if (!GetAll().Any(l => l.UserId == userId && l.ReviewId == reviewId))
            {
                Insert(new ReviewWarning
                {
                    UserId = userId,
                    ReviewId = reviewId,
                    Comment = text,
                    DateCreated = DateTime.Now
                });

                return true;
            }

            return false;
        }
    }
}