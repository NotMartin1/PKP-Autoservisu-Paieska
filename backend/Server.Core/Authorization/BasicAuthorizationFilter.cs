using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.Core.Authorization
{
    public class BasicAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Response.Headers.FirstOrDefault(header => string.Equals(header.Key, "Authorization", StringComparison.InvariantCultureIgnoreCase));
            if (authorizationHeader.Key == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var encodedUsernamePassword = authorizationHeader.ToString().Substring("Basic ".Length).Trim();
            var decodedUsernamePassword = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            var usernamePasswordArray = decodedUsernamePassword.Split(':');

            var username = usernamePasswordArray[0];
            var password = usernamePasswordArray[1];

            if (!IsAuthenticated(username, password))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        private bool IsAuthenticated(string username, string password)
        {
            return username == "admin" && password == "admin";
        }
    }
}
