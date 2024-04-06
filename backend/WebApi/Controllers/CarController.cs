using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("Make/GetMakes")]
        public ServiceResult<List<CarMake>> GetMakes()
        {
            return _carService.GetMakes();
        }

        [HttpPost("Make/Create")]
        public ServiceResult Create([FromBody] CarMakeCreateRequest request)
        {
            return _carService.CreateMake(request);
        }

        [HttpPost("Add")]
        public ServiceResult<CarCreateResult> AddCar([FromBody] CarAddRequest request)
        {
            return _carService.AddCar(request);
        }

        [HttpDelete("Delete")]
        public ServiceResult DeleteCar([FromBody] CarDeleteRequest request)
        {
            return _carService.DeleteCar(request);
        }

        [HttpGet("List")]
        public ServiceResult<List<CarAddRequest>> GetCars([FromQuery] int clientId)
        {
            return _carService.GetClientCars(clientId);
        }
    }
}
