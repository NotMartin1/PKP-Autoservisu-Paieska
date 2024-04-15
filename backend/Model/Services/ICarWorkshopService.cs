using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;

namespace Model.Services
{
    public interface ICarWorkshopService
    {
        ServiceResult<CarWorkshopDetails> GetCarWorkshopDetails(int id);
        ServiceResult<WorkingHours> GetCarWorkshopWorkingHours(int id);
        ServiceResult<List<CarWorkshopDisplayBasicData>> List(ListArgs request);
        ServiceResult<LoginResponse<CarWorkshopBasicData>> Login(LoginRequest request);
        ServiceResult<RegistrationResponse> Register(RegistrationRequest<CarWorkshopRegistrationArgs> request);
        ServiceResult SetWorkingHours(CarWorkshopWorkingHoursCreateArgs request);
    }
}
