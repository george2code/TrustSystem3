using System.Collections.Generic;
using System.Linq;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IUnitOfWork unit)
            : base(unit)
        {
        }

        public Category GetById(int categoryId)
        {
            return SingleOrDefault(categoryId);
        }

        public IEnumerable<Category> ListCategoriesByCompanyId(int companyId)
        {
            return GetAll().Where(c => c.Companies.Any(m => m.Id == companyId));
        } 
    }
}