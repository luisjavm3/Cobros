using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cobros.API.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckPersonExistence(string nationalID)
        {
            return await _applicationDbContext.Persons
                .AnyAsync(p => p.NationalID.Equals(nationalID));
        }
    }
}
