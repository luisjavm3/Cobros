using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface ICobroRepository:IRepository<Cobro>
    {
        Task<Cobro> GetByIdIncludingActivedLoans(int id);
    }
}
