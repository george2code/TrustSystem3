using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class InvitationRepository : BaseRepository<Invitation>
    {
        public InvitationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
