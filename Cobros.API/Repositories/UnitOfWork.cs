using Cobros.API.DataAccess;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cobros.API.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IDbContextTransaction? _transaction;
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        //public ApplicationDbContext Context { get { return _applicationDbContext; } }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            Users = new UserRepository(_applicationDbContext);
            RefreshTokens = new RefreshTokenRepository(_applicationDbContext);
        }

        public void BeginTransaccion()
        {
            _transaction = _applicationDbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public async Task CompleteAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
