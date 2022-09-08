using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        [HttpPost("Register")]
        public Task<IActionResult> Register()
        {
            throw new NotImplementedException();
        }
    }
}
