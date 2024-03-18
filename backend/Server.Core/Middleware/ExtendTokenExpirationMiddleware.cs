using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Model.Entities.Constant;
using Model.Services;
using System;
using System.Threading.Tasks;

public class ExtendTokenExpirationMiddleware
{
    private readonly RequestDelegate _next;

    public ExtendTokenExpirationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 200)
        {
            if (context.Request.Cookies.TryGetValue(TokenConstants.COOKIE_IDENTIFIER, out var token))
            {
                var extendedToken = TokenService.ExtendTokenExpiration(token);
                context.Response.Cookies.Append(TokenConstants.COOKIE_IDENTIFIER, extendedToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
            }
        }
    }
}
