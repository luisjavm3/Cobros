using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {   }

        public async Task<IEnumerable<Loan>> GetAllByCobroIdAndSortedByRoutePositionASC(int cobroId)
        {
            return await _applicationDbContext.Loans
                .Include(x => x.PartialPayments)
                .Where(x => x.CobroId == cobroId && x.DeletedAt == null)
                .OrderBy(x => x.RoutePosition)
                .ToListAsync();
        }
    }
}
