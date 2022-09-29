using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
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

        [HttpGet("Loans/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
