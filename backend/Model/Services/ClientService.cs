using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;
using Model.Repositories.Interfaces;
using Model.Services.Interfaces;

namespace Model.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public ServiceResult<LoginResponse<ClientBasicData>> Login(LoginRequest request)
        {
            try
            {
                var credentialsValidationResult = ValidateCredentials(new()
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

                if (!_clientRepository.ValidateCredentials(request.Username, request.Password))
                    return new()
                    {
                        Success = false,
                        Message = "Invalid credentials",
                        Data = new(LoginResultCode.InvalidCredentials)
                    };

                var clientData = _clientRepository.GetBasicByUsername(request.Username);

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

        public ServiceResult<RegistrationResponse> Register(RegistrationRequest<ClientRegistrationData> request)
        {
            try
            {
                var credentialsValidationResult = ValidateCredentials(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                });

                if (!credentialsValidationResult.Success)
                    return new()
                    {
                        Success = false,
                        Message = credentialsValidationResult.Message,
                        Data = new(RegistrationResultCode.ValidationFailed)
                    };

                if (_clientRepository.CheckIfExsitsByUsername(request.Username))
                    return new()
                    {
                        Success = false,
                        Message = "User with same name already exsits",
                        Data = new(RegistrationResultCode.DuplicateUsername)
                    };

                _clientRepository.Insert(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                    Fullname = request.AdditionalData.Fullname,
                    IsEnabled = true,
                });

                return new() { Success = true, Data = new(RegistrationResultCode.Success) };
                
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        private ServiceResult ValidateCredentials(CredentialsValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Username))
                return new()
                {
                    Success = false,
                    Message = "Username not specified",
                };

            if (string.IsNullOrWhiteSpace(args.Password))
                return new()
                {
                    Success = false,
                    Message = "Password not specified",
                };

            if (args.Password.Length < 3)
                return new()
                {
                    Success = false,
                    Message = "Pasword length should be greather than or equal to 3",
                };

            return new() { Success = true };
        }
    }
}
