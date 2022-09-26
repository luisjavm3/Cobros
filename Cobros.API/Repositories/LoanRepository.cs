using Cobros.API.Core.Business.Interfaces;
using Cobros.API.DataAccess;
using Cobros.API.Entities;

namespace Cobros.API.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanBusiness
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {   }

        public Task<IEnumerable<Loan>> GetAllByCobroId(int cobroId)
        {

            throw new NotImplementedException();
        }
    }
}
