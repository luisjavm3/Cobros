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
                var userAuthenticatedDto = await userBusiness.GetByIdWithCobros(userId);

                context.Items["User"] = userAuthenticatedDto;
                Console.WriteLine($"---→ User: {JsonSerializer.Serialize(userAuthenticatedDto)}");
            }
            catch (Exception)
            {
                context.Items["User"] = null;
            }

            await _next(context);
        }
    }
}
