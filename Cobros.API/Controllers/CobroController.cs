using Cobros.API.Core.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CobrosController : ControllerBase
    {
        private readonly ICobroBusiness _cobroBusiness;

        public CobrosController(ICobroBusiness cobroBusiness)
        {
            _cobroBusiness = cobroBusiness;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cobroBusiness.GetCobroById(id);
            return Ok(result);
        }
    }
}
