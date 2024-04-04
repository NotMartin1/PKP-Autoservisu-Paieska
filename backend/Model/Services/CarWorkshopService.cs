using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization.Response;
using Model.Entities.CarService;
using Model.Repositories;

namespace Model.Services
{
    public class CarWorkshopService : ICarWorkshopService
    {
        private readonly ICarWorkshopRepository _carServiceRepository;

        public CarWorkshopService(ICarWorkshopRepository carServiceRepository)
        {
            _carServiceRepository = carServiceRepository;
        }

        public ServiceResult<RegistrationResponse> Register(RegistrationRequest<CarWorkshopRegistrationArgs> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.AdditionalData?.CompanyName))
                    return new() { Success = false, Message = "Company name is missing", Data = new(RegistrationResultCode.ValidationFailed) };

                if (_carServiceRepository.CheckIfCompanyNameExsits(request.AdditionalData.CompanyName))
                    return new() { Success = false, Message = "Company with same already exsits" };

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
    }
}
