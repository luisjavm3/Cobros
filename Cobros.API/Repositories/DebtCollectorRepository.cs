using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Repositories
{
    public class DebtCollectorRepository : Repository<DebtCollector>, IDebtCollectorRepository
    {
        public DebtCollectorRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
