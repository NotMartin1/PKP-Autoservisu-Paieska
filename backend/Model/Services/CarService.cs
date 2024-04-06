using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Repositories;

namespace Model.Services
{
    public class CarService : ICarService
    {
        private readonly ICarMakeRepository _carMakeRepository;
        private readonly ICarRepository _carRepository;
        private readonly IClientService _clientService;

        public CarService(ICarMakeRepository carMakeRepository, IClientService clientService, ICarRepository carRepository)
        {
            _carMakeRepository = carMakeRepository;
            _clientService = clientService;
            _carRepository = carRepository;
        }

        public ServiceResult<List<CarMake>> GetMakes()
        {
            try
            {
                var makes = _carMakeRepository.GetMakes();
                return new() { Success = true, Data = makes };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        public ServiceResult CreateMake(CarMakeCreateRequest request)
        {
            try
            {
                if (_carMakeRepository.CheckIfExsitsByName(request?.Name!))
                    return new(false, "Make with same name already exists");

                _carMakeRepository.Insert(request.Name);

                return new(true);
            }
            catch (Exception ex)
            {
                return new(false, "Technical Error Occurred");
            }
        }

        public ServiceResult AddCar(CarAddRequest request)
        {
            try
            {
                if (!request.ClientId.HasValue)
                    return new(false, "ClientId is not specified");

                if (!request.MakeId.HasValue)
                    return new(false, "MakeId is not specified");

                if (string.IsNullOrWhiteSpace(request.Model))
                    return new(false, "Model is not specified");

                if (string.IsNullOrWhiteSpace(request.Engine))
                    return new(false, "Engine is not specified");

                if (!request.Mileage.HasValue)
                    return new(false, "Mileage is not specified");

                if (!request.ProductionYear.HasValue)
                    return new(false, "Production year is not specified");

                if (!_clientService.CheckIfExsitsById(request.ClientId.Value))
                    return new(false, $"Cannot find client by Id: {request.ClientId}");

                if (!_carMakeRepository.CheckIfExsitsById(request.MakeId.Value))
                    return new(false, $"Cannot find make by Id: {request.MakeId}");

                _carRepository.Insert(request);

                return new(true);
            }
            catch (Exception ex)
            {
                return new(false, "Technical Error Occurred");
            }
        }
    }
}
