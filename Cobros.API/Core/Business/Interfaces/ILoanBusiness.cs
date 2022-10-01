using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.Pagination;

namespace Cobros.API.Core.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Task<PaginationResult<LoanDto>> GetAllByCobroId(int cobroId, PaginationParameters paginationParameters);
        Task<LoanDetailsDto> GetByIdWithDetails(int id);
        Task InsertLoan(int cobroId, LoanCreateDto loanCreateDto);
    }
}
