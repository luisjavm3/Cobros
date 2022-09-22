using Cobros.API.DataAccess;

namespace Cobros.API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICobroRepository Cobros { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        void BeginTransaccion();
        void Commit();
        void Rollback();
        Task CompleteAsync();
    }
}
