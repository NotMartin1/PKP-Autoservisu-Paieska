using Model.Entities;
using Model.Entities.Order;

namespace Model.Services
{
    public interface IOrdersService
    {
        ServiceResult<OrderCancelResult> CancelOrder(int orderId);
        ServiceResult<OrderCreateResult> CreateOrder(OrderCreateRequest request);
        ServiceResult<List<OrderItem>> GetClientOrders(int clientId);
    }
}