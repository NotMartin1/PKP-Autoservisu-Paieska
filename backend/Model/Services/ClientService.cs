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

                if (_clientRepository.CheckIfExsitsByUsername(request.Username))
                    return new() { Success = false, Message = "Duplicate username", Data = new(RegistrationResultCode.DuplicateUsername) };

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

        public ServiceResult<LoginResponse<ClientBasicData>> Login(LoginRequest request)
        {
            try
            {
                var credentialsValidationResult = _validationService.ValidateCredentails(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                });

                if (!credentialsValidationResult.Success)
                    return new()
                    {
                        Success = false,
                        Message = credentialsValidationResult.Message,
                        Data = new(LoginResultCode.InvalidCredentials)
                    };

                if (!_clientRepository.ValidateCredentials(request.Username!, request?.Password!))
                    return new()
                    {
                        Success = false,
                        Message = "Invalid credentials",
                        Data = new(LoginResultCode.InvalidCredentials)
                    };

                var clientData = _clientRepository.GetBasicByUsername(request?.Username!);
                if (!clientData.IsEnabled)
                    return new() { Success = false, Message = "User is disabled", Data = new(LoginResultCode.UserDisabled) };
                
                return new()
                {
                    Success = true,
                    Data = new()
                    {
                        ResultCode = LoginResultCode.Authorized,
                        UserData = clientData,
                    }
                };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        public bool CheckIfExsitsById(int id) => _clientRepository.CheckIfExsitsById(id);
    }
}
