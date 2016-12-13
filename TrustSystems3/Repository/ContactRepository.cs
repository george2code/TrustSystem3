using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class ContactRepository: BaseRepository<Contacts>
    {
        public ContactRepository(IUnitOfWork unit)
            : base(unit)
        {
        }
    }
}