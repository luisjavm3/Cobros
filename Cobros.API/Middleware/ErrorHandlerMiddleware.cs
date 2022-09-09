using Cobros.API.Core.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cobros.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;

                    case KeyNotFoundException e:
                    case NotFoundException ee:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;

                    case AccessForbiddenException e:
                        response.StatusCode = StatusCodes.Status403Forbidden;
                        break;

                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {message = error.Message});
                await response.WriteAsync(result);
            }
        }
    }
}
