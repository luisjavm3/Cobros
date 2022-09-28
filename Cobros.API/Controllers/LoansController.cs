using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.Pagination;
using Cobros.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Cobros/{cobroId}/[controller]")]
    public class LoansController:ControllerBase
    {
        private readonly ILoanBusiness _loanBusiness;

        public LoansController(ILoanBusiness loanBusiness)
        {
            _loanBusiness = loanBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int cobroId, [FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _loanBusiness.GetAllByCobroId(cobroId, paginationParameters);
            return Ok(result);
        }
    }
}
