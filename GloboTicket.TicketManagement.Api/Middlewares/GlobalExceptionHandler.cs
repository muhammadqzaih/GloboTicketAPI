using GloboTicket.TicketManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GloboTicket.TicketManagement.Api.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            var problemDetails = GetCleanProblemDetails(exception);

            context.Response.StatusCode = (int)problemDetails.Status!;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        private static ProblemDetails GetCleanProblemDetails(Exception exception)
        {
            return exception switch
            {
                BadRequestException badRequestException => new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = badRequestException.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                },
                NotFoundException notFoundException => new ProblemDetails
                {
                    Title = "The specified resource was not found.",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = notFoundException.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
                },
                ConflictException conflictException => new ProblemDetails
                {
                    Title = "Conflict",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = conflictException.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8"
                },
                PreconditionFailedException preconditionFailedException => new ProblemDetails
                {
                    Title = "Precondition Failed",
                    Status = (int)HttpStatusCode.PreconditionFailed,
                    Detail = preconditionFailedException.Message,
                    Type = "https://tools.ietf.org/html/rfc7232#section-4.2"
                },
                UnauthorizedException unauthorizedAccessException => new ProblemDetails
                {
                    Title = "Unauthorized",
                    Status = (int)HttpStatusCode.Unauthorized,
                    Detail = unauthorizedAccessException.Message,
                    Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
                },
                ForbiddenException forbiddenException => new ProblemDetails
                {
                    Title = "Forbidden",
                    Status = (int)HttpStatusCode.Forbidden,
                    Detail = forbiddenException.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
                },
                _ => new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = "An unexpected error occurred",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                },
            };
        }
    }
}
