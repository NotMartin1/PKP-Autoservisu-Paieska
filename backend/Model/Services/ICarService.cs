using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;

namespace Model.Services
{
    public interface ICarService
    {
        ServiceResult AddCar(CarAddRequest request);
        ServiceResult CreateMake(CarMakeCreateRequest request);
        ServiceResult DeleteCar(CarDeleteRequest request);
        ServiceResult<List<CarMake>> GetMakes();
    }
}