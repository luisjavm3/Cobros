using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.DTO.Loan;
using Cobros.API.Core.Model.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [Authorize]
    [ApiController]
    public class LoansController:ControllerBase
    {
        private readonly ILoanBusiness _loanBusiness;

        public LoansController(ILoanBusiness loanBusiness)
        {
            _loanBusiness = loanBusiness;
        }

        /// <summary>
        /// Paginated List-loans- sorted by RoutePosition.
        /// </summary>
        /// <param name="cobroId"></param>
        /// <param name="paginationParameters">PageNumber and PageSize</param>
        /// <returns>List-Loan- sorted by RoutePosition.</returns>
        [HttpGet("Cobros/{cobroId}/Loans")]
        public async Task<IActionResult> GetAll(int cobroId, [FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _loanBusiness.GetAllByCobroId(cobroId, paginationParameters);
            return Ok(result);
        }

        /// <summary>
        /// Insert Loan in specific Cobro
        /// </summary>
        /// <param name="cobroId"></param>
        /// <param name="loanCreateDto"></param>
        /// <returns></returns>
        [HttpPost("Cobros/{cobroId}/Loans")]
        public async Task<IActionResult> Insert(int cobroId, LoanCreateDto loanCreateDto)
        {
            await _loanBusiness.InsertLoan(cobroId, loanCreateDto);
            return Ok();
        }

        /// <summary>
        /// Retrieve a loan details od Cobro, PartialPayments and Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Loan</returns>
        [HttpGet("Loans/{id}/details")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _loanBusiness.GetByIdWithDetails(id);
            return Ok(result);
        }
    }
}
