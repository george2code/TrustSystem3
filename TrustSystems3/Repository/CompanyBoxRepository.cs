using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class CompanyBoxRepository: BaseRepository<CompanyBox>
    {
        public CompanyBoxRepository(IUnitOfWork unit)
            : base(unit)
        {
        }

        public CompanyBox GetCompanyBoxByCompanyIdAndType(int companyId, CompanyBoxType type)
        {
            return this.GetAll().FirstOrDefault(b => b.CompaniesId == companyId && b.BoxType == type);
        }
    }
}