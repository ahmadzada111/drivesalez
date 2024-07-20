using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using DriveSalez.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace DriveSalez.WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, message) = GetErrorDetails(exception);

        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var response = (message, (int)statusCode);
        var jsonResponse = JsonSerializer.Serialize(response);
        
        await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

        return true;
    }

    private (HttpStatusCode code, string message) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            UserNotFoundException => (HttpStatusCode.NotFound, exception.Message),
            UserNotAuthorizedException => (HttpStatusCode.Forbidden, exception.Message),
            EmailNotConfirmedException => (HttpStatusCode.BadRequest, exception.Message),
            PaymentFailedException => (HttpStatusCode.PaymentRequired, exception.Message),
            BannedUserException => (HttpStatusCode.Forbidden, exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
            ValidationException => (HttpStatusCode.BadRequest, exception.Message),
            KeyNotFoundException => (HttpStatusCode.BadRequest, exception.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };
    }
}