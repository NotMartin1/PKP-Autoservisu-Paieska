using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;

namespace Model.Services.Interfaces
{
    public interface ICarService
    {
        ServiceResult CreateMake(CarMakeCreateRequest request);
        ServiceResult<List<CarMake>> GetMakes();
    }
}