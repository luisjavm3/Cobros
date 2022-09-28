using Cobros.API.Core.Model.Pagination;
using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {}

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _applicationDbContext.Users
                    .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        }
    }
}
