using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Entities;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Task<IEnumerable<LoanDto>> GetAllByCobroId(int cobroId);
    }
}
