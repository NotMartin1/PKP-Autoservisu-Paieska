using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.Order;
using Model.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _orderService;

        public OrderController(IOrdersService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Create")]
        public ServiceResult<OrderCreateResult> Create([FromBody] OrderCreateRequest request)
        {
            var result = _orderService.CreateOrder(request);
            return result;
        }

        [HttpGet("GetClientOrders")]
        public ServiceResult<List<OrderItem>> GetOrders([FromQuery] int clientId)
        {
            var result = _orderService.GetClientOrders(clientId);
            return result;
        }

        [HttpDelete("OrderCancel")]
        public ServiceResult<OrderCancelResult> OrderCancel([FromQuery] int orderId)
        {
            var result = _orderService.CancelOrder(orderId);
            return result;
        }
    }
}
