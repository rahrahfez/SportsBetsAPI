using System;
using System.Text.Json;
using System.Threading.Tasks;
using LoggerService;
using Microsoft.AspNetCore.Http;
using SportsBetsServer.Helpers;

namespace SportsBetsServer.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _log;
        public ErrorHandler(RequestDelegate next, ILoggerManager log)
        {
            _next = next;
            _log = log;
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
                        _log.LogError($"{ ex?.Message }");
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case NotFoundException _:
                        _log.LogError($"{ ex.Message }");
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        _log.LogError($"{ ex?.Message }");
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
