using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.DTO.PartialPayment;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [Authorize]
    [ApiController]
    public class PartialPaymentController:ControllerBase
    {
        private readonly IPartialPaymentBusiness _partialPaymentBusiness;

        public PartialPaymentController(IPartialPaymentBusiness partialPaymentBusiness)
        {
            _partialPaymentBusiness = partialPaymentBusiness;
        }

        [HttpPost("Loans/{loanId}/PartialPayments")]
        public async Task<IActionResult> Insert(int loanId, PartialPaymentCreateDto partialPaymentCreateDto)
        {
            await _partialPaymentBusiness.InsertPartialPayment(loanId, partialPaymentCreateDto);
            return Ok();
        }
    }
}
