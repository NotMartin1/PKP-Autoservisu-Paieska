using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;

namespace Model.Services.Interfaces
{
    public interface ICarWorkshopService
    {
        ServiceResult<List<CarWorkshopDisplayBasicData>> List(ListArgs args);
        ServiceResult<LoginResponse<CarWorkshopBasicData>> Login(LoginRequest request);
        ServiceResult<RegistrationResponse> Register(RegistrationRequest<CarWorkshopRegistrationArgs> request);
    }
}