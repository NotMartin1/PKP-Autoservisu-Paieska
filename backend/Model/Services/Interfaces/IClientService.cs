using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;

namespace Model.Services.Interfaces
{
    public interface IClientService
    {
        bool CheckIfExsitsById(int id);
        ServiceResult<LoginResponse<ClientBasicData>> Login(LoginRequest request);
        ServiceResult<RegistrationResponse> Register(RegistrationRequest<ClientRegistrationData> request);
    }
}