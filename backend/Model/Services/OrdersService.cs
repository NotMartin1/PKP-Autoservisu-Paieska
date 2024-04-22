using Model.Entities;
using Model.Entities.Order;
using Model.Repositories;

namespace Model.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IClientService _clientService;
        private readonly ICarService _carService;
        public OrdersService(IOrdersRepository ordersRepository, IClientService clientService, ICarService carService)
        {
            _ordersRepository = ordersRepository;
            _clientService = clientService;
            _carService = carService;
        }

        public ServiceResult<OrderCreateResult> CreateOrder(OrderCreateRequest request)
        {
            try
            {
                if (request.ClientId == 0)
                    return new() { Success = false, Message = "ClientId is not specified", Data = OrderCreateResult.ValidationFailed };

                if (request.ServiceId == 0)
                    return new() { Success = false, Message = "ServiceId is not specified", Data = OrderCreateResult.ValidationFailed };

                if (request.CarId == 0)
                    return new() { Success = false, Message = "CarId is not specified", Data = OrderCreateResult.ValidationFailed };

                if (string.IsNullOrWhiteSpace(request.Description))
                    return new() { Success = false, Message = "Description is not specified", Data = OrderCreateResult.ValidationFailed };

                var client = _clientService.GetBasicById(request.ClientId);
                if (client == null)
                    return new() { Success = false, Message = $"Cannot find client by provided Id: {request.ClientId}", Data = OrderCreateResult.ClientNotFound };

                if (!client.IsEnabled)
                    return new() { Success = false, Message = $"Client is disabled", Data = OrderCreateResult.ClientDisabled };

                var carsGetResult = _carService.GetClientCars(client.Id);
                if (!carsGetResult.Success || carsGetResult.Data?.FirstOrDefault(c => c.Id == request.CarId) == null)
                    return new() { Success = false, Message = $"Cannot find client card by provided Id: {request.CarId}", Data = OrderCreateResult.CarNotFound };

                _ordersRepository.Insert(request);

                return new() { Success = true, Data = OrderCreateResult.Success};
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred", Data = OrderCreateResult.UnknownError };
            }
        }

        public ServiceResult<List<OrderItem>> GetClientOrders(int clientId)
        {
            try
            {
                if (clientId == 0)
                    return new() { Success = false, Message = "ClientId is not specified" };

                var orders = _ordersRepository.GetClientOrders(clientId);

                return new() { Success = true, Data = orders }; 
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred" };
            }
        }

        public ServiceResult<OrderCancelResult> CancelOrder(int orderId)
        {
            try
            {
                if (orderId == 0)
                    return new() { Success = false, Message = "OrderId is not specified", Data = OrderCancelResult.ValidationFailed };

                var order = _ordersRepository.GetOrderById(orderId);
                if (order == null)
                    return new() { Success = false, Message = "Order not found", Data = OrderCancelResult.OrderNotFound };

                if (order.OrderStatus == OrderStatus.Canceled)
                    return new() { Success = false, Message = "Order already cancelled", Data = OrderCancelResult.AlreadyCancelled };

                _ordersRepository.SetOrderStatus(orderId, OrderStatus.Canceled);

                return new() { Success = true, Data = OrderCancelResult.Success };
            }
            catch (Exception ex)
            {
                return new() { Success = false, Message = "Technical Error Occurred", Data = OrderCancelResult.UnknownError };
            }
        }

    }
}
