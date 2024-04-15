using System.Drawing.Printing;
using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.Client;
using Model.Repositories;
using Model.Services.Interfaces;

namespace Model.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ClientService(IClientRepository clientRepository,IPasswordHasher passwordHasher)
        {
            _clientRepository = clientRepository;
            _passwordHasher = passwordHasher;
        }

        public ServiceResult<LoginResponse<ClientBasicData>> Login(LoginRequest request)
        {
            try
            {   
                    
                if (!_clientRepository.CheckIfExsitsByUsername(request.Username))
                    return new()
                    {
                        Success = false,
                        Message = "User does not exsits",
                        Data = new(LoginResultCode.InvalidCredentials)
                    };



                var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, _clientRepository.GetPasswordByUsername(request.Username));

                
                

                if (!isPasswordValid)
                {
                    return new()
                    {
                        Success = false,
                        Message = "Invalid Password",
                        Data = new(LoginResultCode.InvalidCredentials)
                    };
                }


                var credentialsValidationResult = ValidateCredentials(new()
                {
                    Username = request.Username,
                    Password = _clientRepository.GetPasswordByUsername(request.Username),
                });
                    

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
                var passwordHash = _passwordHasher.HashPassword(request.Password);

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
                    Password = passwordHash,
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

        public bool CheckIfExsitsById(int id) => _clientRepository.CheckIfExsitsById(id);

        private ServiceResult ValidateCredentials(CredentialsValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Username))
                return new(false, "Username not specified");

            if (string.IsNullOrWhiteSpace(args.Password))
                return new(false, "Password not specified");


            if (args.Password.Length < 3)
                return new(false, "Pasword length should be greather than or equal to 3");

            return new(true);
        }

        public string GetPasswordByUsername(string username)
        {
            return _clientRepository.GetPasswordByUsername(username);
        }
    }
}
