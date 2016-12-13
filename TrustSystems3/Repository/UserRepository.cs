using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class UserRepository : BaseRepository<AspNetUsers>
    {
        public UserRepository(IUnitOfWork unit)
            : base(unit)
        {
        }
    }
}
