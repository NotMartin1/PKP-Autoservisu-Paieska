using Model.Entities.Car.Request;
using Model.Exstensions;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class CarRepository : ICarRepository1
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
    }
}
