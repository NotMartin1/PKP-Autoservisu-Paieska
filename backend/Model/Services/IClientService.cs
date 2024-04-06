using Model.Entities;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;

namespace Model.Services
{
    public interface IClientService
    {
        ServiceResult<RegistrationResponse> Register(RegistrationRequest<ClientRegistrationData> request);
    }
}