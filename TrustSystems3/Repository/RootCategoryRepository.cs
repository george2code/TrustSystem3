using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class RootCategoryRepository: BaseRepository<RootCategory>
    {
        public RootCategoryRepository(IUnitOfWork unit)
            : base(unit)
        {
        }
    }
}