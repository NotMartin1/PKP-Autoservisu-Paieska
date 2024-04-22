using Model.Entities.Order;

namespace Model.Repositories
{
    public interface IOrdersRepository
    {
        List<OrderItem> GetClientOrders(int clientId);
        OrderItem GetOrderById(int id);
        void Insert(OrderCreateRequest args);
        void SetOrderStatus(int orderId, OrderStatus status);
    }
}