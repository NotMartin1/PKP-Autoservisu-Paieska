using Model.Entities;
using Model.Entities.Authorization;
using Model.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Model.Services
{
    public class ValidationService : IValidationService
    {
        private const int MINIMAL_PASSWORD_LENGTH = 3;

        public ServiceResult ValidateCredentails(CredentialsValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Username))
                return new(false, "Username is missing");

            if (string.IsNullOrWhiteSpace(args.Password))
                return new(false, "Password is missing");

            if (args.Password.Length < MINIMAL_PASSWORD_LENGTH)
                return new(false, $"Password length should be greater than or equal {MINIMAL_PASSWORD_LENGTH}");

            return new(true);
        }

        public bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(\+370|8)(6\d{7}|5\d{7}|46\d{6}|37\d{6}|45\d{6}|52\d{6}|4\d{7})$");
            Match match = regex.Match(phoneNumber);
            return match.Success;
        }
    }
}
