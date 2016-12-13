using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TrustSystems3.Entity;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class CompanyRepository: BaseRepository<Companies>
    {
        public CompanyRepository(IUnitOfWork unit)
            : base(unit)
        {
        }

        public Dictionary<Companies, float> ListRatingLevelByCategoryId(IEnumerable<int> categoryIdList,
           int count, bool isTopLevel)
        {
            Dictionary<Companies, float> result = new Dictionary<Companies, float>();
            String query = "";
            try
            {
                query =
                string.Format(
                    @"SELECT TOP {0} cc.Companies_Id, ISNULL(CAST(SUM(r.Rating) AS FLOAT)/COUNT(*), 0) AS Rating
							FROM dbo.CompanyCategory cc
							LEFT JOIN dbo.Reviews r ON r.CompanyId = cc.Companies_Id
							WHERE cc.Categories_Id IN ({1})
							GROUP BY cc.Companies_Id
							ORDER BY Rating {2}, COUNT(*) {2}",
                    count, string.Join(",", categoryIdList), isTopLevel ? "DESC" : string.Empty);

                IEnumerable<CompanyRaiting> ratings = Database.Database.SqlQuery<CompanyRaiting>(query).ToList();
                IEnumerable<int> companyRatingIds = ratings.Select(r => r.Companies_Id);
                var companies = GetAll().Where(c => companyRatingIds.Contains(c.Id));

                foreach (var raiting in ratings)
                {
                    if (count != int.MaxValue && (isTopLevel && raiting.Rating < 2.5 || !isTopLevel && raiting.Rating >= 2.5))
                        continue;

                    var company = companies.FirstOrDefault(c => c.Id == raiting.Companies_Id);
                    if (company != null)
                        result.Add(company, (float)Math.Round(raiting.Rating, 1));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + query);
            }
            

            return result;
        }

        public Companies GetByCompanySlug(string slug)
        {
            return GetAll().FirstOrDefault(c => c.Slug == slug);
        }

        public Companies GetByCompanyId(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Companies> Search(string term)
        {
            return GetAll().Where(x => x.Name.Contains(term) ||
                (x.Description != null && x.Description.Contains(term)) || 
                (x.Address != null && x.Address.Contains(term)));
        }

        public int GetPositionInCategory(int categoryId, int companyId, out int totalCount)
        {
            var ratingDictionary = ListRatingLevelByCategoryId(new List<int>{categoryId}, int.MaxValue, true);
            totalCount = ratingDictionary.Count;
            return ratingDictionary.TakeWhile(c => c.Key.Id != companyId).Count() + 1;
        }
    }
}