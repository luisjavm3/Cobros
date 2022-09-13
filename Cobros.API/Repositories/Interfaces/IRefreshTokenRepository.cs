using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IRefreshTokenRepository:IRepository<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetAllByUserId(int userId);
        Task<RefreshToken> GetByValueAsync(string value);
    }
}
