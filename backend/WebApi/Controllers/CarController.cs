﻿using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Services.Interfaces;
using Server.Core.Middleware;

namespace WebApi.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(TokenValidationFilter))]
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
    }
}
