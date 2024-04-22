using Model.Entities.Order;
using Model.Exstensions;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private const string TABLE_NAME = "order";

        private readonly IGenericRepository _genericRepository;

        public OrdersRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Insert(OrderCreateRequest args)
        {
            var sql = new MySqlCommand($@"
                INSERT INTO `{TABLE_NAME}`
                (ClientId, ServiceId, CarId, Description, Status, ArrivalTime)
                VALUES
                (?clientId, ?serviceId, ?carId, ?description, 0, ?arrivalTime)");

            sql.AddParametersFromObject(args);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public List<OrderItem> GetClientOrders(int clientId)
        {
            var sql = new MySqlCommand($@"
                SELECT Id, ClientId, CarId, Status, Description, Id, ArrivalTime
                FROM `{TABLE_NAME}`
                WHERE ClientId = ?clientId");

            sql.AddParameter("?clientId", clientId);

            return _genericRepository.FetchList<OrderItem>(sql);
        }
    }
}
