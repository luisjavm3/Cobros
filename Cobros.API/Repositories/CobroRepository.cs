using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;

namespace Cobros.API.Repositories
{
    public class CobroRepository:Repository<Cobro>, ICobroRepository
    {
        public CobroRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
