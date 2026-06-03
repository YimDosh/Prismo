using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Prismo.Api.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Si la excepción fue causada por FluentValidation...
        if (exception is ValidationException validationException)
        {
            logger.LogWarning("Validation failed: {Message}", exception.Message);

            // Estructuramos los errores en un diccionario para el JSON (Ej: "Name": ["The company name is required."])
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            // Usamos ProblemDetails, que es el estándar de la industria para reportar errores en APIs
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation Error",
                Detail = "One or more validation failures have occurred.",
                Instance = httpContext.Request.Path
            };
            
            problemDetails.Extensions.Add("errors", errors);

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true; // Le dice a .NET que ya manejamos el error con éxito
        }

        // Si es cualquier otro error (ej: se cayó la base de datos), dejamos que devuelva un 500 normal
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
        return false; 
    }
}
