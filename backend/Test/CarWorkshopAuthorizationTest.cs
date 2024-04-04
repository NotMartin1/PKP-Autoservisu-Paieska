using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Authorization.Request;
using Model.Entities.Authorization;
using Model.Entities.CarService;
using Model.Services;

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
    }
}
