using Model.Entities;
using Model.Entities.Authorization;

namespace Model.Services
{
    public interface IValidationService
    {
        ServiceResult ValidateCredentails(CredentialsValidationArgs args);
        bool ValidatePhoneNumber(string phoneNumber);
    }
}