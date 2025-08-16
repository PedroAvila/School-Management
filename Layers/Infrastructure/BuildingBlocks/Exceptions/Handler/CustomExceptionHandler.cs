using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exception.Message,
            DateTime.UtcNow
        );

        (string Detail, string Title, int StatusCode) = exception switch
        {
            ValidationException => (
                exception.Message,
                exception.GetType().Name,
                //"Error de validación",
                //string.Join(", ", validationEx.Errors.Select(e => e.ErrorMessage)),
                StatusCodes.Status400BadRequest
            ),
            BusinessException customException => (
                customException.Message,
                // exception.GetType().Name,
                "Error de negocio",
                (int)customException.Status
            ),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            ),
        };

        context.Response.StatusCode = StatusCode;

        var problemDetails = new ProblemDetails
        {
            Title = Title,
            Detail = Detail,
            Status = StatusCode,
            Instance = context.Request.Path,
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        if (exception is ValidationException validationException)
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);

        await context.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken: cancellationToken
        );
        return true;
    }
}
