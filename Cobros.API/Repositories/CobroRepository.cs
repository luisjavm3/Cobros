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

        public async Task<IEnumerable<Cobro>> GetAllByUserWithLoansAsync(int userId)
        {
            return await _applicationDbContext.Cobros
                .Include(x => x.Loans.Where(y => y.DeletedAt == null))
                .Include(x => x.User)
                .Include(x => x.DebtCollector)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cobro>> GetAllWithLoansAsync()
        {
            return await _applicationDbContext.Cobros
                .Include(x=>x.Loans.Where(y=>y.DeletedAt ==  null))
                .Include(x=>x.User)
                .Include(x=>x.DebtCollector)
                .ToListAsync();
        }

        public async Task<Cobro> GetByIdWithLoansAsync(int id)
        {
            return await _applicationDbContext.Cobros
                .Include(c => c.Loans.Where(l => l.DeletedAt == null))
                .Include(c => c.User)
                .Include(c=>c.DebtCollector)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cobro> GetByNameAsync(string name)
        {
            return await _applicationDbContext.Cobros
                .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
