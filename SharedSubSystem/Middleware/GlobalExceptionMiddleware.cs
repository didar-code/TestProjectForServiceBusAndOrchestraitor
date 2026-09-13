using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedSubSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedSubSystem.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await WriteResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                
                await WriteResponse(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ConflictException ex)
            {
                
                _logger.LogWarning(ex, "Conflict: {Message}", ex.Message);
                await WriteResponse(context, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (BusinessRuleException ex)
            {
           
                await WriteResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (AuthenticationException ex)
            {
                await WriteResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Unhandled exception on {Path}", context.Request.Path);

                var message = _env.IsDevelopment()
                    ? ex.Message
                    : "An unexpected error occurred. Please try again later.";

                await WriteResponse(context, StatusCodes.Status500InternalServerError, message);
                
            }
        }

        private static async Task WriteResponse(HttpContext context, int statusCode, string error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { error });
        }

    }
}
