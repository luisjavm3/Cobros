using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByIdWithCobros(int id);
    }
}
