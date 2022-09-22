using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class CobroRepository:Repository<Cobro>, ICobroRepository
    {
        public CobroRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Cobro> GetByIdIncludingActiveLoansAsync(int id)
        {
            return await _applicationDbContext.Cobros
                .Include(c => c.Loans.Where(l => l.DeletedAt == null))
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
