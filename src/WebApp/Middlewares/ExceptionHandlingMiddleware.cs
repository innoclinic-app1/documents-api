using System.Net;
using Domain.Dtos;
using Domain.Exceptions;

namespace WebApp.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate request, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
        }
        catch (BadFormatException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (AlreadyExistsException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Conflict);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode code)
    {
        LogMessage(message, code);
        
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)code;
        
        var error = new ErrorDto((int)code, message);
        await response.WriteAsJsonAsync(error);
    }

    private void LogMessage(string message, HttpStatusCode statusCode)
    {
        if (statusCode >= HttpStatusCode.InternalServerError)
        {
            _logger.LogError("{message}", message);
        }
        else
        {
            _logger.LogInformation("{message}", message);
        }
    }
}
