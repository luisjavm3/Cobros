using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Repositories
{
    public class PartialPaymentRepository : Repository<PartialPayment>,IPartialPaymentRepository
    {
        public PartialPaymentRepository(ApplicationDbContext context) : base(context)
        {}


    }
}
