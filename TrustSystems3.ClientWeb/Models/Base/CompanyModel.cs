using System;
using System.Collections.Generic;
using PagedList;

namespace TrustSystems3.ClientWeb.Models.Base {
    public class CompanyModel {
        public Companies Company { get; set; }
        public CompanyBox PromotionBox { get; set; }
        public CompanyBox GuaranteeBox { get; set; }
        public CompanyBox FacebookBox { get; set; }
        public IPagedList<ReviewModel> Reviews { get; set; } 
        public bool CanUserEvaluateReview { get; set; }
        public String CompanyRatingLabel { get; set; }
        public float CompanyRating { get; set; }
        public int? LastReviewPassTime { get; set; }
        public List<CategoryPosition> CategoryPositions { get; set; }
    }
}