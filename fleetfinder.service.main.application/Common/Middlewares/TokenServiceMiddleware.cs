using fleetfinder.service.main.application.Services;
using Microsoft.AspNetCore.Http;

namespace fleetfinder.service.main.application.Common.Middlewares;

public class TokenServiceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenService _tokenService;

    public TokenServiceMiddleware(RequestDelegate next, TokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && _tokenService.IsTokenRevoked(token))
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}
