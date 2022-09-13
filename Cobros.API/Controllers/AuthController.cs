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
        public async Task<IActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            await _authBusiness.Register(authRegisterDto);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthLoginDto authLoginDto)
        {
            var tokens = await _authBusiness.Login(authLoginDto);

            SetCookie(tokens.RefreshToken);
            return Ok(tokens.AccessToken);
        }

        [HttpPost("Refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return BadRequest(new { message = "No cookie was found with refresh token." });

            var tokens = await _authBusiness.RefreshToken(refreshToken);

            SetCookie(tokens.RefreshToken);
            return Ok(tokens.AccessToken);
        }

        [HttpPost("Revoke-token")]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return BadRequest(new { message = "No cookie was found with refresh token." });

            await _authBusiness.RevokeRefreshToken(refreshToken);

            Response.Cookies.Delete("refresh_token");

            return Ok();
        }

        private void SetCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1),
            };

            Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);
        }
    }
}
