using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustSystems3.Entity;
using TrustSystems3.Repository;

namespace TrustSystems3.Services
{
    public class CompanyService
    {
        private TSModelContainer Context
        {
            get { return Context ?? new TSModelContainer(); }
        }

        public Dictionary<Companies, float> ListRatingLevelByCategoryId(int categoryId, bool isRoot, 
            int count, bool isTopLevel)
        {
//            var companies = Context.Companies.ToList()
//                .Where(x => x.Categories.Any(b => b.RootCategoryId == rootCategoryId));
//
//            Dictionary<Companies, double> topCompaniesList = new Dictionary<Companies, double>();
//            foreach (var company in companies)
//            {
//                var averageRating = company.Reviews.Average(r => r.Rating);
//                topCompaniesList.Add(company, averageRating);
//            }

            Dictionary<Companies, float> result = new Dictionary<Companies, float>();

            IEnumerable<int> categoryIds;
            if (isRoot) {
                categoryIds = Context.Categories
                    .Where(c => c.RootCategoryId == categoryId)
                    .Select(c => c.Id)
                    .ToList();
            }
            else {
                categoryIds = new List<int> { categoryId };
            }

            string query =
                string.Format(
                    @"
						SELECT TOP {0} cc.CompanyID, ISNULL(CAST(SUM(r.RatingValue) AS FLOAT)/COUNT(*), 0) AS Rating
							FROM dbo.CompanyToCategories cc
							LEFT JOIN dbo.CompanyReviews r ON r.CompanyId = cc.CompanyID AND r.IsComfirmed = 1
							WHERE cc.CategoryID IN ({1})
							GROUP BY cc.CompanyID
							ORDER BY Rating {2}, COUNT(*) {2}",
                    count, categoryIds, isTopLevel ? "DESC" : string.Empty);


            IEnumerable<CompanyRaiting> ratings = Context.Database.SqlQuery<CompanyRaiting>(query).ToList();
            IEnumerable<int> companyRatingIds = ratings.Select(r => r.Companies_Id);
            var companies = Context.Companies.Where(c => companyRatingIds.Contains(c.Id));

            foreach (var raiting in ratings)
            {
                if (count != int.MaxValue && (isTopLevel && raiting.Rating < 2.5 || !isTopLevel && raiting.Rating >= 2.5))
                    continue;

                var company = companies.FirstOrDefault(c => c.Id == raiting.Companies_Id);
                if (company != null)
                    result.Add(company, (float)Math.Round(raiting.Rating, 1));
            }

            return result;
        }
    }
}