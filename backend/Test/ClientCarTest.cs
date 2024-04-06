using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities;
using Model.Entities.Car;
using Model.Entities.Car.Request;
using Model.Repositories;
using Model.Services;

namespace Test
{
    public static class ClientCarTestData
    {
        public static int Id { get; set; }
    }


    [TestClass]
    public class ClientCarTest
    {
        private readonly ICarService _carService;
        private readonly ICarMakeRepository _carMakeRepository;
        private readonly IClientRepository _clientRepository;

        public ClientCarTest()
        {
            var container = ConfigurationMock.GetContainer();

            _carService = container.GetInstance<ICarService>();
            _clientRepository = container.GetInstance<IClientRepository>();
            _carMakeRepository = container.GetInstance<ICarMakeRepository>();
        }

        [TestMethod]
        public void AddCarNotAllFieldsSpecified()
        {
            var carAddRequest = new CarAddRequest()
            {
                ClientId = null,
                MakeId = null,
                Model = null,
                Engine = null,
                Mileage = null,
                ProductionYear = null,
            };

            var result = _carService.AddCar(carAddRequest);

            Assert.AreEqual(result.Data, CarCreateResult.ValidationFailed);
        }

        [TestMethod]
        public void AddCarClientNotFound()
        {
            var carAddRequest = new CarAddRequest()
            {
                ClientId = -1,
                MakeId = GetMakeId(),
                Model = Guid.NewGuid().ToString(),
                Engine = Guid.NewGuid().ToString(),
                Mileage = 0,
                ProductionYear = DateOnly.FromDateTime(DateTime.Now),
            };

            var result = _carService.AddCar(carAddRequest);

            Assert.AreEqual(result.Data, CarCreateResult.ClientNotFound);
        }

        [TestMethod]
        public void AddCarMakeNotFound()
        {
            var carAddRequest = new CarAddRequest()
            {
                ClientId = GetClientId(),
                MakeId = -1,
                Model = Guid.NewGuid().ToString(),
                Engine = Guid.NewGuid().ToString(),
                Mileage = 0,
                ProductionYear = DateOnly.FromDateTime(DateTime.Now),
            };

            var result = _carService.AddCar(carAddRequest);

            Assert.AreEqual(result.Data, CarCreateResult.MakeNotFound);
        }

        [TestMethod]
        public void AddCarSuccess()
        {
            var clientId = GetClientId();

            var carAddRequest = new CarAddRequest()
            {
                ClientId = clientId,
                MakeId = GetMakeId(),
                Model = Guid.NewGuid().ToString(),
                Engine = Guid.NewGuid().ToString(),
                Mileage = 0,
                ProductionYear = DateOnly.FromDateTime(DateTime.Now),
            };

            var result = _carService.AddCar(carAddRequest);

            ClientCarTestData.Id = _carService.GetClientCars(clientId).Data.FirstOrDefault().Id;

            Assert.AreEqual(result.Data, CarCreateResult.Created);
        }

        [TestMethod]
        public ServiceResult DeleteCar()
        {
            var carId = ClientCarTestData.Id;
            if (carId == 0)
            {
                AddCarSuccess();
                carId = ClientCarTestData.Id;
            }

            _carService.DeleteCar(new()
            {
                CarId = carId
            });

            return new(true, "Car deleted");
        }

        private int GetClientId()
        {
            var username = Guid.NewGuid().ToString();

            _clientRepository.Insert(new()
            {
                Username = username,
                Password = username,
                Fullname = username,
                IsEnabled = true,
            });

            return _clientRepository.GetBasicByUsername(username).Id;
        }

        private int GetMakeId()
        {
            var make = _carMakeRepository.GetMakes().FirstOrDefault();
            if (make == null)
            {
                var name = Guid.NewGuid().ToString();
                _carMakeRepository.Insert(name);
                make = _carMakeRepository.GetMakes().FirstOrDefault();
            }

            return make.Id;
        }
    }
}
