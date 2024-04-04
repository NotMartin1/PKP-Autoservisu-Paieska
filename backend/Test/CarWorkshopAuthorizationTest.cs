using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization;
using Model.Entities.CarService;
using Model.Services;
using Model.Entities.Constants;

namespace Test
{
    public static class CarWorkshopAuthorizationData
    {
        public static readonly string username = Guid.NewGuid().ToString().Substring(0, 20);
    }

    [TestClass]
    public class CarWorkshopAuthorizationTest
    {
        private readonly string password = "test-123";

        private readonly ICarWorkshopService _carWorkshopService;

        public CarWorkshopAuthorizationTest()
        {
            _carWorkshopService = ConfigurationMock.GetContainer().GetInstance<ICarWorkshopService>();
        }

        [TestMethod]
        public void RegistrationMandatoryFieldsInvalid()
        {
            var registrationArgs = new RegistrationRequest<CarWorkshopRegistrationArgs>()
            {
                Username = "",
                Password = "",
                AdditionalData = new() { CompanyName = $"{CarWorkshopAuthorizationData.username}-company" },
            };

            var registrationResult = _carWorkshopService.Register(registrationArgs);

            Assert.AreEqual(registrationResult.Data?.ResultCode, RegistrationResultCode.ValidationFailed);
        }

        [TestMethod]
        public void RegistrationPasswordTooShort()
        {
            var password = Guid.NewGuid().ToString().Substring(0, ValidationConstants.MINIMAL_PASSWORD_LENGTH - 1);

            var registrationArgs = new RegistrationRequest<CarWorkshopRegistrationArgs>()
            {
                Username = CarWorkshopAuthorizationData.username,
                Password = password,
                AdditionalData = new()
                {
                    CompanyName = $"{CarWorkshopAuthorizationData.username}-company",
                    PhoneNumber = PhoneNumberGenerator.GenerateLithuanianPhoneNumber(),
                    Email = $"{CarWorkshopAuthorizationData.username}@test.com"
                },
            };

            var registrationResult = _carWorkshopService.Register(registrationArgs);

            Assert.AreEqual(registrationResult.Data?.ResultCode, RegistrationResultCode.ValidationFailed);
        }

        [TestMethod]
        public void RegistrationPhoneNumberInvalid()
        {
            var phoneNumber = "+3712333312";

            var registrationArgs = new RegistrationRequest<CarWorkshopRegistrationArgs>()
            {
                Username = CarWorkshopAuthorizationData.username,
                Password = Guid.NewGuid().ToString(),
                AdditionalData = new() 
                { 
                    CompanyName = $"{CarWorkshopAuthorizationData.username}-company",
                    PhoneNumber = phoneNumber,
                    Email = $"{CarWorkshopAuthorizationData.username}@test.com"
                },
            };

            var registrationResult = _carWorkshopService.Register(registrationArgs);

            Assert.AreEqual(registrationResult.Data?.ResultCode, RegistrationResultCode.InvalidPhoneNumber);
        }

        [TestMethod]
        public void RegistrationEmailInvalid()
        {
            var email = "invalidemail";

            var registrationArgs = new RegistrationRequest<CarWorkshopRegistrationArgs>()
            {
                Username = CarWorkshopAuthorizationData.username,
                Password = Guid.NewGuid().ToString(),
                AdditionalData = new() {
                    CompanyName = $"{CarWorkshopAuthorizationData.username}-company",
                    PhoneNumber = PhoneNumberGenerator.GenerateLithuanianPhoneNumber(),
                    Email = email 
                },
            };

            var registrationResult = _carWorkshopService.Register(registrationArgs);
            Assert.AreEqual(registrationResult.Data?.ResultCode, RegistrationResultCode.InvalidEmail);
        }
    }
}
