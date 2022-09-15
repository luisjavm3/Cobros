﻿using Cobros.API.Core.Model;
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

        public async Task<IEnumerable<User>> GetRangeOfUser(PaginationParameters pagination)
        {
            var skip = (pagination.Page - 1) * pagination.PageSize;

            return await _applicationDbContext.Users
                        .OrderBy(x => x.Id)
                        .Skip(skip)
                        .Take(pagination.PageSize)
                        .ToListAsync();
        }
    }
}
