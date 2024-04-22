using Model.Entities.Order;

namespace Model.Repositories
{
    public interface IOrdersRepository
    {
        List<OrderItem> GetClientOrders(int clientId);
        void Insert(OrderCreateRequest args);
    }
}