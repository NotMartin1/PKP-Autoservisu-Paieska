using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.Entities.Constant;
using Model.Services;

namespace Server.Core.Middleware
{
    public class TokenValidationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Cookies[TokenConstants.COOKIE_IDENTIFIER];

                if (token == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var principal = TokenService.ValidateToken(token);

                context.HttpContext.User = principal;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
