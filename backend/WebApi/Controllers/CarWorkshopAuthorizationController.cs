using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Services;
using Model.Services.Interfaces;

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

        [HttpPost("Register")]
        public ServiceResult<RegistrationResponse> Register([FromBody] RegistrationRequest<CarWorkshopRegistrationArgs> request)
        {
            var result = _carWorkshopService.Register(request);
            return result;
        }

        [HttpPost("Login")]
        public ServiceResult<LoginResponse<CarWorkshopBasicData>> Login([FromBody] LoginRequest request)
        {
            var result = _carWorkshopService.Login(request);
            return result;
        }
    }
}
