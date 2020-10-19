using IPStackExternalService.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IPStackWebAPI.Middleware
{

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Custom error log : {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// All the erros in the api pass through this handler 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            if (ex is EntryPointNotFoundException) code = HttpStatusCode.NotFound;
            else if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (ex is TaskCanceledException || ex is Exception) code = HttpStatusCode.BadRequest;
            else if (ex is InvalidOperationException) code = HttpStatusCode.Conflict;

            string errorMessage = string.Empty;
            if (ex.GetType().Name == nameof(IPServiceNotAvailableException))
                errorMessage = $"IPServiceNotAvailableException-{ex.Message}";
            else
                errorMessage = ex.Message;

            var result = JsonConvert.SerializeObject(new { error = errorMessage, stacktrace = ex.StackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;


            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
