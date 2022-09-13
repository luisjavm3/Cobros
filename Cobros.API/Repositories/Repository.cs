using Cobros.API.Core.Model.Exceptions;
using Cobros.API.DataAccess;
using Cobros.API.Entities;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cobros.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ApplicationDbContext _applicationDbContext;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }

        public async Task DeleteAsync(T entity)
        {
            var existing = await _dbSet.FirstOrDefaultAsync(x=>x.Id == entity.Id);

            if (existing == null)
                throw new NotFoundException($"{typeof(T).Name} with Id: {entity.Id} not found.");

            _dbSet.Remove(existing);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
