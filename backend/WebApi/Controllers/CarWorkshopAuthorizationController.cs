using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Entities.Constant;
using Model.Entities.Enums;
using Model.Services;
using Model.Services.Interfaces;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/CarWorkshop/Authorization")]
    public class CarWorkshopAuthorizationController : ControllerBase
    {
        private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopAuthorizationController(ICarWorkshopService carWorkshopService)
        {
            _carWorkshopService = carWorkshopService;
        }

        [HttpPost("Login")]
        public ServiceResult<LoginResponse<CarWorkshopBasicData>> Login([FromBody] LoginRequest request)
        {
            var authorizationResult = _carWorkshopService.Login(request);

            if (authorizationResult.Data.ResultCode == LoginResultCode.Authorized)
            {
                var claims = new List<Claim>
                {
                    new("userType", UserType.WorkshopManager.ToString()),
                    new("userId", authorizationResult.Data.UserData.Id.ToString()),
                    new("username", authorizationResult.Data.UserData.Username),
                    new("companyName", authorizationResult.Data.UserData.CompanyName),
                    new("email", authorizationResult.Data.UserData.Email),
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
        public ServiceResult<RegistrationResponse> Register([FromBody] RegistrationRequest<CarWorkshopRegistrationArgs> request)
        {
            return _carWorkshopService.Register(request);
        }
    }
}
