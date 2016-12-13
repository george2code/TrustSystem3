using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class TemplateRepository : BaseRepository<Template>
    {
        public TemplateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}