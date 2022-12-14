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
                .Include(x=>x.Customer)
                .Where(x => x.CobroId == cobroId && x.DeletedAt == null)
                .OrderBy(x => x.RoutePosition)
                .ToListAsync();
        }

        public Task<Loan> GetByCobroIdAndRoutePosition(int cobroId, int routePosition)
        {
            return _applicationDbContext
                .Loans
                .FirstOrDefaultAsync(l => l.CobroId == cobroId && l.RoutePosition == routePosition);
        }

        public async Task<Loan> GetDetails(int id)
        {
            return await _applicationDbContext.Loans
                .Include(l => l.Customer)
                .Include(l => l.PartialPayments)
                .Include(l => l.Cobro)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
