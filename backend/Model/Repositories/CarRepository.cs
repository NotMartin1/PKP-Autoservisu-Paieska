using Model.Entities.Car.Request;
using Model.Exstensions;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IGenericRepository _genericRepository;

        private const string TABLE_NAME = "car";

        public CarRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Insert(CarAddRequest args)
        {
            var sql = new MySqlCommand(@$"
            INSERT INTO {TABLE_NAME}
            (MakeId, Model, Engine, Mileage, Year, ClientId)
            VALUES
            (?makeId, ?model, ?engine, ?mileage, ?productionYear, ?clientId)");

            sql.AddParametersFromObject(args);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public void Delete(CarDeleteRequest request)
        {
            var sql = new MySqlCommand($@"
            DELETE FROM {TABLE_NAME}
            WHERE Id = ?id");

            sql.AddParameter("?id", request.CarId);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public List<CarAddRequest> List(int clientId)
        {
            var sql = new MySqlCommand($@"
            SELECT Id, MakeId, Model, Engine, Mileage, Year, ClientId
            FROM {TABLE_NAME}
            WHERE ClientId = ?clientId");

            sql.AddParameter("?clientId", clientId);

            return _genericRepository.FetchList<CarAddRequest>(sql);
        }
    }
}
