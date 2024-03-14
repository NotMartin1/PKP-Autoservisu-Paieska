using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Repositories.Interfaces;
using Model.Services.Interfaces;

namespace Model.Services
{
    public class CarService : ICarService
    {
        private readonly ICarMakeRepository _carMakeRepository;

        public CarService(ICarMakeRepository carMakeRepository)
        {
            _carMakeRepository = carMakeRepository;
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
                if (_carMakeRepository.CheckIfExsitsByName(request.Name))
                    return new() { Success = false, Message = "Make with same name already exists" };

                _carMakeRepository.Insert(request.Name);

                return new() { Success = true }; 
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }
    }
}
