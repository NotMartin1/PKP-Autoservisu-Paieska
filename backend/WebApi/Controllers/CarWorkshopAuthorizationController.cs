using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
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

        [HttpPost("Login")]
        public ServiceResult<LoginResponse<CarWorkshopBasicData>> Login([FromBody] LoginRequest request)
        {
            return _carWorkshopService.Login(request);
        }

        [HttpPost("Register")]
        public ServiceResult<RegistrationResponse> Register([FromBody] RegistrationRequest<CarWorkshopRegistrationArgs> request)
        {
            return _carWorkshopService.Register(request);
        }
    }
}
