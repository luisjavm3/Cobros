using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetRangeOfUsers([FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _userBusiness.GetRangeOfUsers(paginationParameters);

            return Ok(result);
        }
    }
}
