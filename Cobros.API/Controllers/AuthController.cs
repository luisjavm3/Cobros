using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.DTO.Auth;
using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;
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

        [Authorize(roles: Role.ADMIN)]
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


        /// <summary>
        /// Returns an Refresh Token on headers and an access token on responses body.
        /// </summary>
        /// <returns>A Refresh Token settep on headers and a access token returned on response body.</returns>
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

        [Authorize]
        [HttpPut("ChangePassword/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, ChangePasswordDto changePasswordDto)
        {
            await _authBusiness.ChangePassword(userId, changePasswordDto);
            return Ok();
        }

        private void SetCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                IsEssential = true,
            };

            Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);
            CookieDePrueba();
        }

        private void CookieDePrueba()
        {
            var cookieOptions = new CookieOptions
            {
                //HttpOnly = true,
                //Expires = DateTimeOffset.UtcNow.AddDays(1),
                IsEssential = true,
            };

            Response.Cookies.Append("cookie_prueba", "valor_cookie_prueba", cookieOptions);
        }
    }
}
