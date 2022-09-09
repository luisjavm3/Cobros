using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.Exceptions;
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
        public async Task<IActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            await _authBusiness.Register(authRegisterDto);
            return Ok();
        }
    }
}
