using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Pagination;
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
        public async Task<IActionResult> GetUsers([FromQuery] PaginationParameters paginationParameters)
        {
            var result = await _userBusiness.GetUsers(paginationParameters);

            return Ok(result);
        }
    }
}
