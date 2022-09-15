using Cobros.API.Core.Model;
using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {}

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _applicationDbContext.Users
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        }

        public async Task<PagedList<User>> GetRangeOfUser(PaginationParameters paginationParameters)
        {
            var skip = (paginationParameters.PageNumber - 1) * paginationParameters.PageSize;
            var count = await _applicationDbContext.Users.CountAsync();
            var users = await _applicationDbContext.Users
                        .OrderBy(x => x.Id)
                        .Skip(skip)
                        .Take(paginationParameters.PageSize)
                        .ToListAsync();

            return new PagedList<User>(users, paginationParameters, count);
        }
    }
}
