using Microsoft.IdentityModel.Tokens;
using Model.Entities.Constant;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Model.Services
{
    public static class TokenService
    {
        public static string GetToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenConstants.SECRET_KEY);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = TokenConstants.ISSUER,
                Expires = DateTime.UtcNow.AddMinutes(TokenConstants.EXPIRATION_MINUTES),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenConstants.SECRET_KEY);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenConstants.ISSUER,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

            return principal;
        }

        public static string ExtendTokenExpiration(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenConstants.SECRET_KEY);

            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claims = securityToken.Claims;
            if (claims == null)
                throw new SecurityTokenValidationException();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = TokenConstants.ISSUER,
                Expires = DateTime.UtcNow.AddMinutes(TokenConstants.EXPIRATION_MINUTES),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var extendedToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(extendedToken);
        }
    }
}
