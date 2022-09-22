using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface ICobroRepository:IRepository<Cobro>
    {
        Task<Cobro> GetByIdIncludingActiveLoansAsync(int id);
    }
}
