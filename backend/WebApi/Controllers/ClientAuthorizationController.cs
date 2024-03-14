using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;
using Model.Entities.Constant;
using Model.Entities.Enums;
using Model.Services;
using Model.Services.Interfaces;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Client/Authorization")]
    public class ClientAuthorizationController : ControllerBase
    {
        private readonly IClientService _usersService;

        public ClientAuthorizationController(IClientService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("Login")]
        public ServiceResult<LoginResponse<ClientBasicData>> Login([FromBody] LoginRequest request)
        {
            var authorizationResult = _usersService.Login(request);

            if (authorizationResult.Data.ResultCode == LoginResultCode.Authorized)
            {
                var claims = new List<Claim>
                {
                    new("userType", UserType.Client.ToString()),
                    new("userid", authorizationResult.Data.UserData.Id.ToString()),
                    new("username", authorizationResult.Data.UserData.Username)
                };

                var token = TokenService.GetToken(new(claims));

                Response.Cookies.Append(TokenConstants.COOKIE_IDENTIFIER, token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
            }

            return authorizationResult;
        }

        [HttpPost("Register")]
        public ServiceResult<RegistrationResponse> Register([FromBody] RegistrationRequest<ClientRegistrationData> request)
        {
            return _usersService.Register(request);
        }
    }
}
