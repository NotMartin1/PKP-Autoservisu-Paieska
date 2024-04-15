using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;
using Model.Services;
using Model.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/CarWorkshop")]
    public class CarWorkshopController
    {
        private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopController(ICarWorkshopService carWorkshopService)
        {
            _carWorkshopService = carWorkshopService;
        }

        [HttpPost("List")]
        public ServiceResult<List<CarWorkshopDisplayBasicData>> List([FromBody] ListArgs request)
        {
            return _carWorkshopService.List(request);
        }
    }
}
