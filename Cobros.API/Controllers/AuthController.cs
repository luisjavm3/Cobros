using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;

        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            try
            {
                await _authBusiness.Register(authRegisterDto);
                return Ok();
            }
            catch (Exception er)
            {
                return BadRequest(er.Message);
            }
        }
    }
}
