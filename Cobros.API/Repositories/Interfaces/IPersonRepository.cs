using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<bool> CheckPersonExistence(string nationalID);
    }
}
