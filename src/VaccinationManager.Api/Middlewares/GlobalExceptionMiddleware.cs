using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace VaccinationManager.Api.Middlewares;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<GlobalExceptionMiddleware> _logger;

	public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
			_logger.LogError(ex, "An unhandled exception occurred.");
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		var response = new { error = exception.Message };

		switch (exception)
		{
			case ArgumentException:
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				break;

			case InvalidCredentialException:
			case UnauthorizedAccessException:
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				break;

			default:
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response = new { error = "An internal server error occurred." };
				break;
		}

		return context.Response.WriteAsync(JsonSerializer.Serialize(response));
	}
}
