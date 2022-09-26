using Cobros.API.Entities;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Task<IEnumerable<Loan>> GetAllByCobroId(int cobroId);
    }
}
