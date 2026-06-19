using EventsApp.Controllers;
using EventsApp.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace EventsApp.Infrastructure.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, error) = exception switch
        {
            NotFoundException ex => (StatusCodes.Status404NotFound,
                ApiError.From(StatusCodes.Status404NotFound, ex.Message)),

            AlreadyExistsException ex => (StatusCodes.Status400BadRequest,
                ApiError.From(StatusCodes.Status400BadRequest, ex.Message)),

            _ => (StatusCodes.Status500InternalServerError,
                ApiError.From(StatusCodes.Status500InternalServerError, "An unexpected error occurred"))
        };

        logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

        return true;
    }
}
