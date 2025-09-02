using GloboTicket.TicketManagement.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace GloboTicket.TicketManagement.Api.Middlewares
{
    public class CustomAuthorizationMiddlewareHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded)
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedException("You must be logged in to access this resource.");
                }

                throw new ForbiddenException("You do not have permission to access this resource.");
            }
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }

    }
}
