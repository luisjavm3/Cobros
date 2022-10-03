using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByIdWithActiveLoan(int id);
        Task<Customer> GetByNationalID(string nationalID);
    }
}
