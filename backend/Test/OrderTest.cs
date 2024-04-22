using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Car.Request;
using Model.Entities.Order;
using Model.Repositories;
using Model.Services;

namespace Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly IOrdersService _ordersService;
        private readonly IClientRepository _clientRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICarMakeRepository _carMakeRepository;
        private readonly IOrdersRepository _orderRepository;

        public OrderTest()
        {
            var container = ConfigurationMock.GetContainer();

            _ordersService = container.GetInstance<IOrdersService>();
            _clientRepository = container.GetInstance<IClientRepository>();
            _carMakeRepository = container.GetInstance<ICarMakeRepository>();
            _carRepository = container.GetInstance<ICarRepository>();
            _orderRepository = container.GetInstance<IOrdersRepository>();
        }

        [TestMethod]
        public void OrderCreateValidationTest()
        {
            var result = _ordersService.CreateOrder(new());
            Assert.AreEqual(result?.Data, OrderCreateResult.ValidationFailed);   
        }

        [TestMethod]
        public void OrderCreateUserNotFound()
        {
            var result = _ordersService.CreateOrder(new OrderCreateRequest 
            {
                CarId = 1,
                ClientId = -1,
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            Assert.AreEqual(result?.Data, OrderCreateResult.ClientNotFound);
        }

        [TestMethod]
        public void OrderCreateUserDisabled()
        {
            var result = _ordersService.CreateOrder(new OrderCreateRequest
            {
                CarId = 1,
                ClientId = GetClientId(status: false),
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            Assert.AreEqual(result?.Data, OrderCreateResult.ClientDisabled);
        }

        [TestMethod]
        public void OrderCreateCarNotFound()
        {
            var result = _ordersService.CreateOrder(new OrderCreateRequest
            {
                CarId = -1,
                ClientId = GetClientId(status: true),
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            Assert.AreEqual(result?.Data, OrderCreateResult.CarNotFound);
        }

        [TestMethod]
        public void OrderCreateSuccessful()
        {
            var clientId = GetClientId(status: true);
            
            var result = _ordersService.CreateOrder(new OrderCreateRequest
            {
                CarId = GetCarId(clientId),
                ClientId = clientId,
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            Assert.AreEqual(result?.Data, OrderCreateResult.Success);
        }

        [TestMethod]
        public void OrderCancelValidationFailed()
        {
            var result = _ordersService.CancelOrder(default(int));
            Assert.AreEqual(result?.Data, OrderCancelResult.ValidationFailed);
        }

        [TestMethod]
        public void OrderCancelNotFound()
        {
            var result = _ordersService.CancelOrder(int.MaxValue - 1);
            Assert.AreEqual(result?.Data, OrderCancelResult.OrderNotFound);
        }

        [TestMethod]
        public void OrderCancelAlreadyCancelled()
        {
            var clientId = 10001;
            _orderRepository.Insert(new OrderCreateRequest
            {
                CarId = clientId,
                ClientId = clientId,
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            var order = _orderRepository.GetClientOrders(clientId).LastOrDefault();

            _ordersService.CancelOrder(order.Id);
            var result = _ordersService.CancelOrder(order.Id);
            Assert.AreEqual(result?.Data, OrderCancelResult.AlreadyCancelled);
        }

        [TestMethod]
        public void OrderCancelSuccess()
        {
            var clientId = 10001;
            _orderRepository.Insert(new OrderCreateRequest
            {
                CarId = clientId,
                ClientId = clientId,
                ArrivalTime = DateTime.Now,
                Description = "test",
                ServiceId = 1
            });

            var order = _orderRepository.GetClientOrders(clientId).LastOrDefault();

            var result = _ordersService.CancelOrder(order.Id);
            Assert.AreEqual(result?.Data, OrderCancelResult.Success);
        }

        private int GetClientId(bool status)
        {
            var username = Guid.NewGuid().ToString();

            _clientRepository.Insert(new()
            {
                Username = username,
                Password = username,
                Fullname = username,
                IsEnabled = status,
            });

            return _clientRepository.GetBasicByUsername(username).Id;
        }

        private int GetCarId(int clientId)
        {
            _carRepository.Insert(new CarAddRequest()
            {
                ClientId = clientId,
                MakeId = GetMakeId(),
                Model = Guid.NewGuid().ToString(),
                Engine = Guid.NewGuid().ToString(),
                Mileage = 0,
                ProductionYear = DateOnly.FromDateTime(DateTime.Now),
            });

            return _carRepository.List(clientId).FirstOrDefault()?.Id ?? 0;
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
