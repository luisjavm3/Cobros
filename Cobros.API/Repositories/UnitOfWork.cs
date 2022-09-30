using Cobros.API.DataAccess;
using Cobros.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cobros.API.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public IUserRepository Users { get; }
        public IDebtCollectorRepository DebtCollectors { get; set; }
        public ICobroRepository Cobros { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public ILoanRepository Loans { get; }

        public ICustomerRepository Customers { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            DebtCollectors = new DebtCollectorRepository(_context);
            RefreshTokens = new RefreshTokenRepository(_context);
            Cobros = new CobroRepository(_context);
            Loans = new LoanRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public void BeginTransaccion()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
