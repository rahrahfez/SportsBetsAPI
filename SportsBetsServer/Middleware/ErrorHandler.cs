using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportsBetsServer.Helpers;

namespace SportsBetsServer.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(ex)
                {
                    case AppException _:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case NotFoundException _:
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
