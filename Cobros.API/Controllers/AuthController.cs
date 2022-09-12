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


        /// <summary>
        /// Set a cookie with the resfresh token and an access token.
        /// </summary>
        /// <param name="authLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthLoginDto authLoginDto)
        {
            var loginResponse = await _authBusiness.Login(authLoginDto);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1),
            };

            Response.Cookies.Append("refresh_token", loginResponse.refreshToken, cookieOptions);

            return Ok(loginResponse.accessToken);
        }

    }
}
