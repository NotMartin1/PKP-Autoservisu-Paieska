using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Repositories;
using Model.Services.Interfaces;

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

        public ServiceResult<CarCreateResult> AddCar(CarAddRequest request)
        {
            try
            {
                if (!request.ClientId.HasValue)
                    return new() { Success = false, Message = "ClientId not specified", Data = CarCreateResult.ValidationFailed };

                if (!request.MakeId.HasValue)
                    return new() { Success = false, Message = "MakeId not specified", Data = CarCreateResult.ValidationFailed };

                if (string.IsNullOrWhiteSpace(request.Model))
                    return new() { Success = false, Message = "Model not specified", Data = CarCreateResult.ValidationFailed };

                if (string.IsNullOrWhiteSpace(request.Engine))
                    return new() { Success = false, Message = "Engine is not specified", Data = CarCreateResult.ValidationFailed };

                if (!request.Mileage.HasValue)
                    return new() { Success = false, Message = "Mileage is not specified", Data = CarCreateResult.ValidationFailed };

                if (!request.ProductionYear.HasValue)
                    return new() { Success = false, Message = "Production year is not specified", Data = CarCreateResult.ValidationFailed };

                if (!_clientService.CheckIfExsitsById(request.ClientId.Value))
                    return new() { Success = false, Message = $"Cannot find client with Id: ${request.ClientId.Value}", Data = CarCreateResult.ClientNotFound };

                if (!_carMakeRepository.CheckIfExsitsById(request.MakeId.Value))
                    return new() { Success = false, Message = $"Cannot find make with Id: ${request.MakeId.Value}", Data = CarCreateResult.MakeNotFound };

                _carRepository.Insert(request);

                return new() { Success = true, Data = CarCreateResult.Created };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred", Data = CarCreateResult.TechnicalError };
            }
        }

        public ServiceResult<List<CarAddRequest>> GetClientCars(int clientId)
        {
            try
            {
                if (clientId == 0)
                    return new() { Success = false, Message = "Client Id is not specified" };

                var list = _carRepository.List(clientId);

                return new() { Success = true, Data = list };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        public ServiceResult DeleteCar(CarDeleteRequest request)
        {
            try
            {
                _carRepository.Delete(request);

                return new(true);
            }
            catch (Exception ex)
            {
                return new(false, "Technical Error Occurred");
            }
        }
    }
}
