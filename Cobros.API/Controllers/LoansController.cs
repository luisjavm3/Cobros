using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
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
        public async Task<IActionResult> GetAll(int cobroId)
        {
            throw new NotImplementedException();
        }
    }
}
