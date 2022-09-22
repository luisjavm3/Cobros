using Cobros.API.Core.Business.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace Cobros.API.Middleware
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserBusiness userBusiness)
        {
            try
            {
                var userId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var userDto = await userBusiness.GetById(userId);

                context.Items["User"] = userDto;
            }
            catch (Exception)
            {
                context.Items["User"] = null;
            }

            await _next(context);
        }
    }
}
