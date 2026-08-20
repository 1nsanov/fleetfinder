using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using fleetfinder.service.main.application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace fleetfinder.service.main.application.Common.Middlewares;

public class ExceptionHandlerMiddleware
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpResponse response = context.Response;
        response.ContentType = "application/problem+json";

        ProblemDetails problemDetails = exception switch
        {
            EntityNotFoundException notFoundEx => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.NotFound,
                type: "urn:fleetfinder:error:not-found",
                title: "Resource not found",
                detail: notFoundEx.Message),
            DbUpdateException dbUpdateEx when IsUniqueConstraintViolation(dbUpdateEx) =>
                HandleUniqueConstraintViolation(context, dbUpdateEx),
            ValidationException validationEx when validationEx.Errors.Any() =>
                CreateValidationProblemDetails(context, validationEx),
            ValidationException validationEx => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.BadRequest,
                type: "urn:fleetfinder:error:validation",
                title: "Validation failed",
                detail: validationEx.Message),
            UnauthorizedAccessException unauthorizedAccessException => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.Unauthorized,
                type: "urn:fleetfinder:error:unauthorized",
                title: "Unauthorized",
                detail: unauthorizedAccessException.Message),
            SecurityTokenException securityTokenException => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.Unauthorized,
                type: "urn:fleetfinder:error:unauthorized",
                title: "Unauthorized",
                detail: securityTokenException.Message),
            ArgumentException argumentException => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.BadRequest,
                type: "urn:fleetfinder:error:invalid-argument",
                title: "Invalid argument",
                detail: argumentException.Message),
            InvalidOperationException invalidOperationException => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.BadRequest,
                type: "urn:fleetfinder:error:invalid-operation",
                title: "Invalid operation",
                detail: invalidOperationException.Message),
            not null => CreateProblemDetails(
                context: context,
                statusCode: HttpStatusCode.InternalServerError,
                type: "urn:fleetfinder:error:internal-server-error",
                title: "Internal server error",
                detail: "An unexpected error occurred."),
            _ => throw new ArgumentOutOfRangeException(nameof(exception), exception, null)
        };

        response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

        if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "Handled exception: {Message}", exception.Message);
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true
        };

        await response.WriteAsync(JsonSerializer.Serialize(problemDetails, jsonOptions));
    }

    private static ProblemDetails CreateProblemDetails(
        HttpContext context,
        HttpStatusCode statusCode,
        string type,
        string title,
        string detail,
        Dictionary<string, object?>? extensions = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Type = type,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = context.TraceIdentifier;

        if (extensions is not null)
        {
            foreach ((string key, object? value) in extensions)
            {
                problemDetails.Extensions[key] = value;
            }
        }

        return problemDetails;
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext context,
        ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).Distinct().ToArray());

        var validationProblemDetails = new ValidationProblemDetails(errors)
        {
            Status = (int)HttpStatusCode.BadRequest,
            Type = "urn:fleetfinder:error:validation",
            Title = "Validation failed",
            Detail = "One or more validation errors occurred.",
            Instance = context.Request.Path
        };

        validationProblemDetails.Extensions["traceId"] = context.TraceIdentifier;
        validationProblemDetails.Extensions["errorsDetailed"] = exception.Errors.Select(error => new
        {
            property = error.PropertyName,
            message = error.ErrorMessage,
            attemptedValue = error.AttemptedValue
        });

        return validationProblemDetails;
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        if (exception.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
            return true;

        string message = exception.InnerException?.Message ?? exception.Message;
        return message.Contains("duplicate key", StringComparison.OrdinalIgnoreCase) ||
               message.Contains("unique constraint", StringComparison.OrdinalIgnoreCase) ||
               message.Contains("UNIQUE KEY", StringComparison.OrdinalIgnoreCase) ||
               message.Contains("IX_", StringComparison.OrdinalIgnoreCase);
    }

    private static ProblemDetails HandleUniqueConstraintViolation(HttpContext context, DbUpdateException exception)
    {
        string message = exception.InnerException?.Message ?? exception.Message;

        string? propertyName = ExtractPropertyNameFromMessage(message);
        string? value = ExtractValueFromMessage(message);

        return CreateProblemDetails(
            context: context,
            statusCode: HttpStatusCode.Conflict,
            type: "urn:fleetfinder:error:unique-constraint",
            title: "Duplicate resource",
            detail: string.IsNullOrEmpty(propertyName)
                ? "A record with the same unique value already exists."
                : $"A record with {propertyName} '{value}' already exists.",
            extensions: new Dictionary<string, object?>
            {
                ["constraint"] = "unique_violation",
                ["propertyName"] = propertyName ?? "unknown",
                ["value"] = value ?? "unknown"
            });
    }

    private static string? ExtractPropertyNameFromMessage(string message)
    {
        Match postgresKeyMatch = Regex.Match(message, @"Key \(([^)]+)\)=");
        if (postgresKeyMatch.Success)
            return postgresKeyMatch.Groups[1].Value;

        Match indexMatch = Regex.Match(message, @"IX_\w+_(\w+)");
        if (indexMatch.Success && indexMatch.Groups.Count > 1)
            return indexMatch.Groups[1].Value;

        return null;
    }

    private static string? ExtractValueFromMessage(string message)
    {
        Match postgresValueMatch = Regex.Match(message, @"Key \([^)]+\)=\(([^)]+)\)");
        if (postgresValueMatch.Success)
            return postgresValueMatch.Groups[1].Value;

        Match match = Regex.Match(message, @"\(([^)]+)\)");
        if (match.Success && match.Groups.Count > 1)
            return match.Groups[1].Value;

        return null;
    }
}
