using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.Repository
{
    public class AfsRepository: BaseRepository<Afs>
    {
        public AfsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
