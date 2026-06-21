using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fiap.FCGames.Notifications.CrossCutting.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Ocorreu um erro não tratado na requisição: {Message}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde."
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public required string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DetailedMessage { get; set; }
    }
}
