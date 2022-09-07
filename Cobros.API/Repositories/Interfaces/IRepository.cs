using Cobros.API.Entities;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
