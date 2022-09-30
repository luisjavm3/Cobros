using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Customer> GetByIdWithActiveLoan(int id)
        {
            return await _applicationDbContext.Customers
                .Include(c => c.Loans.Where(l => l.DeletedAt == null))
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
