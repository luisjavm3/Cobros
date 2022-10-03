using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.DTO.DebtCollector;
using Cobros.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebtCollectorsController : ControllerBase
    {
        private readonly IDebtCollectorBusiness _debtCollectorBusiness;

        public DebtCollectorsController(IDebtCollectorBusiness debtCollectorBusiness)
        {
            _debtCollectorBusiness = debtCollectorBusiness;
        }

        [Authorize(Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> InsertDebtCollector(DebtCollectorCreateDto debtCollectorCreateDto)
        {
            await _debtCollectorBusiness.InsertDebtCollector(debtCollectorCreateDto);
            return Ok();
        }
    }
}
