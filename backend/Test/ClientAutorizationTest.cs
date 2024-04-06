using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Authorization;
using Model.Entities.Authorization.Request;
using Model.Entities.Client;
using Model.Entities.Constants;
using Model.Repositories;
using Model.Services;

namespace Test
{
    [TestClass]
    public class ClientAutorizationTest
    {
        private readonly IClientService _clientService;

        public ClientAutorizationTest()
        {
            var container = ConfigurationMock.GetContainer();

            _clientService = container.GetInstance<IClientService>();
        }

        [TestMethod]
        public void RegistrationMissingMandatoryFields()
        {
            var registrationArgs = new RegistrationRequest<ClientRegistrationData>()
            {
                Username = "",
                Password = "",
                AdditionalData = new(),
            };

            var result = _clientService.Register(registrationArgs);

            Assert.AreEqual(result.Data?.ResultCode, RegistrationResultCode.ValidationFailed);
        }

        [TestMethod]
        public void RegistrationInvalidPasswordLength()
        {
            var password = Guid.NewGuid().ToString().Substring(0, ValidationConstants.MINIMAL_PASSWORD_LENGTH - 1);

            var registrationArgs = new RegistrationRequest<ClientRegistrationData>()
            {
                Username = Guid.NewGuid().ToString(),
                Password = password,
                AdditionalData = new()
                {
                    Fullname = Guid.NewGuid().ToString(),
                }
            };

            var result = _clientService.Register(registrationArgs);

            Assert.AreEqual(result.Data?.ResultCode, RegistrationResultCode.ValidationFailed);
        }

        [TestMethod]
        public void RegistrationSuccess()
        {
            var registrationArgs = new RegistrationRequest<ClientRegistrationData>()
            {
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                AdditionalData = new()
                {
                    Fullname = Guid.NewGuid().ToString(),
                }
            };

            var result = _clientService.Register(registrationArgs);

            Assert.AreEqual(result.Data?.ResultCode, RegistrationResultCode.Success);
        }

        [TestMethod]
        public void RegistrationUsernameDuplicate()
        {
            var registrationArgs = new RegistrationRequest<ClientRegistrationData>()
            {
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                AdditionalData = new()
                {
                    Fullname = Guid.NewGuid().ToString(),
                }
            };

            var result = _clientService.Register(registrationArgs);
            if (result.Data?.ResultCode != RegistrationResultCode.Success)
                Assert.Fail("First registration unsuccessfull");

            result = _clientService.Register(registrationArgs);
            Assert.AreEqual(result?.Data?.ResultCode, RegistrationResultCode.DuplicateUsername);
        }
    }
}
