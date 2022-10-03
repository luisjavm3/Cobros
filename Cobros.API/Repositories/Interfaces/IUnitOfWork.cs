using Cobros.API.DataAccess;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IDebtCollectorRepository DebtCollectors { get; }
        ICobroRepository Cobros { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        ILoanRepository Loans { get; }
        ICustomerRepository Customers { get; }
        IPartialPaymentRepository PartialPayments { get; }
        IPersonRepository People { get; }

        void BeginTransaccion();
        void Commit();
        void Rollback();
        Task CompleteAsync();
    }
}
