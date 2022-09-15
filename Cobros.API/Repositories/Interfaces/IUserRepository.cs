using Cobros.API.Core.Model;
using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetRangeOfUser(PaginationParameters pagination);
    }
}
