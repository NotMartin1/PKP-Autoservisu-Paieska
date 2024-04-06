using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;

namespace Model.Services
{
    public interface ICarWorkshopService
    {
        ServiceResult<LoginResponse<CarWorkshopBasicData>> Login(LoginRequest request);
        ServiceResult<RegistrationResponse> Register(RegistrationRequest<CarWorkshopRegistrationArgs> request);
    }
}
