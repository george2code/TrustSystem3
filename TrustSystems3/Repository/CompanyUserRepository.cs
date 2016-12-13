using System.Linq;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class CompanyUserRepository: BaseRepository<CompanyUsers>
    {
        public CompanyUserRepository(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool IsUserOwnThisCompany(string userId, int companyId)
        {
            return GetAll().Any(c => c.UserId == userId && 
                c.CompanyId == companyId && 
                c.UserState == UserStateType.Admin);
        }

        public bool IsUserBelongToThisCompany(string userId, int companyId)
        {
            return GetAll().Any(x => x.CompanyId == companyId && x.UserId == userId);
        }

        public bool IsUserBelongAnyCompany(string userId)
        {
            return GetAll().Any(x => x.UserId == userId);
        }
    }
}