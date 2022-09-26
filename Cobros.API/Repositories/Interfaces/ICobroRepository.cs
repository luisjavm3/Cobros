using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface ICobroRepository:IRepository<Cobro>
    {
        Task<Cobro> GetByIdWithLoansAsync(int id);
        Task<IEnumerable<Cobro>> GetAllWithLoansAsync();
        Task<IEnumerable<Cobro>> GetAllByUserWithLoansAsync(int userId);
        Task<Cobro> GetByNameAsync(string name);
    }
}
