using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Transavia.Infrastructure.Data;

namespace Transavia.API.Middlewares
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException e)
            {
                _logger.LogError(e.ToString());
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}