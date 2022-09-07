using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {}

    }
}
