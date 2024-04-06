using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;
using Model.Repositories;

namespace Model.Services
{
    public class CarWorkshopService : ICarWorkshopService
    {
        private readonly ICarWorkshopRepository _carServiceRepository;
        private readonly IValidationService _validationService;

        public CarWorkshopService(ICarWorkshopRepository carServiceRepository, IValidationService validationService)
        {
            _carServiceRepository = carServiceRepository;
            _validationService = validationService;
        }

        public ServiceResult<RegistrationResponse> Register(RegistrationRequest<CarWorkshopRegistrationArgs> request)
        {
            try
            {
                var validationResult = _validationService.ValidateCredentails(new() { Username = request.Username, Password = request.Password });
                if (!validationResult.Success)
                    return new() { Success = false, Message = validationResult.Message, Data = new(RegistrationResultCode.ValidationFailed) };

                if (string.IsNullOrWhiteSpace(request.AdditionalData?.CompanyName))
                    return new() { Success = false, Message = "Company name is missing", Data = new(RegistrationResultCode.ValidationFailed) };

                if (_carServiceRepository.CheckIfCompanyNameExsits(request.AdditionalData.CompanyName))
                    return new() { Success = false, Message = "Company with same already exsits" };

                if (!string.IsNullOrWhiteSpace(request.AdditionalData.PhoneNumber) && !_validationService.ValidatePhoneNumber(request.AdditionalData.PhoneNumber))
                    return new() { Success = false, Message = "Phone number is invalid", Data = new(RegistrationResultCode.InvalidPhoneNumber) };

                if (!_validationService.ValidateEmail(request.AdditionalData?.Email))
                    return new() { Success = false, Message = "Invalid email", Data = new(RegistrationResultCode.InvalidEmail) };

                _carServiceRepository.Insert(new()
                {
                    Username = request.Username,
                    Password = request.Password,
                    Address = request.AdditionalData.Address,
                    CompanyName = request.AdditionalData.CompanyName,
                    Email = request.AdditionalData.Email,
                    PhoneNumber = request.AdditionalData.PhoneNumber,
                    Website = request.AdditionalData.Website,
                    Description = request.AdditionalData.Description,
                });

                return new() { Success = true, Data = new(RegistrationResultCode.Success) };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = $"Registration failed due to technical error: {ex.Message}" };
            }
        }

        public ServiceResult<LoginResponse<CarWorkshopBasicData>> Login(LoginRequest request)
        {
            try
            {
                var validationResult = _validationService.ValidateCredentails(new() { Username = request.Username, Password = request.Password });
                if (!validationResult.Success)
                    return new() { Success = false, Message = validationResult.Message, Data = new(LoginResultCode.InvalidCredentials) };

                if (!_carServiceRepository.ValidateCredentials(request.Username, request.Password))
                    return new() { Success = false, Message = "Invalid credentials", Data = new(LoginResultCode.InvalidCredentials) };

                var carServiceData = _carServiceRepository.GetBasicByUsername(request.Username);

                return new() { Success = true, Data = new() { ResultCode = LoginResultCode.Authorized, UserData = carServiceData } };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        public ServiceResult<List<CarWorkshopDisplayBasicData>> List(ListArgs args)
        {
            try
            {
                var list = _carServiceRepository.List(args);
                return new() { Success = true, Data = list };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = $"Failed to fetch car workshop list due to: {ex.Message}" };
            }
        }
    }
}
