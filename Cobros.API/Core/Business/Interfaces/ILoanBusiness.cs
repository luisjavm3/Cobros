using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Entities;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Task<PaginationResult<LoanDto>> GetAllByCobroId(int cobroId, PaginationParameters paginationParameters);
    }
}
