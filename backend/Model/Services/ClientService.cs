using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;
using Model.Repositories;

namespace Model.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidationService _validationService;

        public ClientService(IClientRepository clientRepository, IValidationService validationService)
        {
            _clientRepository = clientRepository;
            _validationService = validationService;
        }

        public ServiceResult<RegistrationResponse> Register(RegistrationRequest<ClientRegistrationData> request)
        {
            try
            {
                var credentialsValidationResult = _validationService.ValidateCredentails(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                });

                if (string.IsNullOrWhiteSpace(request.AdditionalData?.Fullname))
                    return new() { Success = false, Message = "Fullname is missing", Data = new(RegistrationResultCode.ValidationFailed) };
               
                if (!credentialsValidationResult.Success)
                    return new()
                    {
                        Success = false,
                        Message = credentialsValidationResult.Message,
                        Data = new(RegistrationResultCode.ValidationFailed)
                    };

                _clientRepository.Insert(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                    Fullname = request.AdditionalData?.Fullname,
                    IsEnabled = true,
                });

                return new() { Success = true, Data = new(RegistrationResultCode.Success) };

            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }
    }
}
