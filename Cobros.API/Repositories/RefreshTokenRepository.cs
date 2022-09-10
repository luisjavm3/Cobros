using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {   }

        public async Task<IEnumerable<RefreshToken>> GetAllByUserId(int userId)
        {
            var result = await _applicationDbContext.RefreshTokens
                            .Include(x => x.User)
                            .Where(x => x.User.Id == userId)
                            .ToListAsync();

            return result;
        }

        //public string GetRefreshToken()
        //{

        //}

        public void HardDelete(RefreshToken entity)
        {
            _applicationDbContext.RefreshTokens.Remove(entity);
        }
    }
}
