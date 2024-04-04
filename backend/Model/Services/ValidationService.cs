using Model.Entities;
using Model.Entities.Authorization;
using Model.Entities.Constants;
using System.Text.RegularExpressions;

namespace Model.Services
{
    public class ValidationService : IValidationService
    {

        public ServiceResult ValidateCredentails(CredentialsValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Username))
                return new(false, "Username is missing");

            if (string.IsNullOrWhiteSpace(args.Password))
                return new(false, "Password is missing");

            if (args.Password.Length < ValidationConstants.MINIMAL_PASSWORD_LENGTH)
                return new(false, $"Password length should be greater than or equal {ValidationConstants.MINIMAL_PASSWORD_LENGTH}");

            return new(true);
        }
    }
}
