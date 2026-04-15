using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using StudentManagement.Application.Common;
using System.Net;
using System.Text.Json;

namespace StudentManagement.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log using Serilog
                Log.Error(ex, "Unhandled Exception Occurred");

                context.Response.ContentType = "application/json";

                // Default
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var message = ex.Message;

                // Optional: Handle specific exceptions
                if (ex is UnauthorizedAccessException)
                {
                    statusCode = (int)HttpStatusCode.Unauthorized;
                }
                else if (ex is KeyNotFoundException)
                {
                    statusCode = (int)HttpStatusCode.NotFound;
                }

                context.Response.StatusCode = statusCode;

                // Include inner exception if exists (useful for debugging)
                var fullMessage = ex.InnerException != null
                    ? $"{ex.Message} | Inner: {ex.InnerException.Message}"
                    : ex.Message;

                var response = ApiResponse<string>.Failure(fullMessage);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}