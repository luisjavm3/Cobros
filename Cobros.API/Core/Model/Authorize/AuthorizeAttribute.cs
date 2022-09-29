using Cobros.API.Core.Model.DTO.User;
using Cobros.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cobros.API.Core.Model.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role[] _roles;
        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[0];
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
                return;

            UserAuthenticatedDto user = (UserAuthenticatedDto)context.HttpContext.Items["User"];

            if(user == null || (_roles.Any() && !_roles.Contains(user.Role)))
            {
                context.Result = new JsonResult(new { message = "Action forbidden." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
