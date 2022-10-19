using Cobros.API.Entities;

namespace Cobros.API.Repositories.Interfaces
{
    public interface ILoanRepository : IRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetAllByCobroIdAndSortedByRoutePositionASC(int cobroId);
        Task<Loan> GetDetails(int id);
        Task<Loan> GetByCobroIdAndRoutePosition(int cobroId, int routePosition);
    }
}
