using Model.Entities.Car;
using Model.Exstensions;
using Model.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class CarMakeRepository : ICarMakeRepository
    {
        private readonly IGenericRepository _genericRepository;

        private const string TABLE_NAME = "make";

        public CarMakeRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Insert(string name)
        {
            var sql = new MySqlCommand(@$"
            INSERT INTO {TABLE_NAME} (Name) VALUES (?name)");

            sql.AddParameter("?name", name);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public List<CarMake> GetMakes()
        {
            var sql = new MySqlCommand(@$"
            SELECT Id, Name
            FROM {TABLE_NAME}
            ORDER BY Id ASC");

            return _genericRepository.FetchList<CarMake>(sql);
        }

        public bool CheckIfExsitsByName(string name)
        {
            var sql = new MySqlCommand($@"
            SELECT COUNT(*)
            FROM {TABLE_NAME}
            WHERE Name = ?name");

            sql.AddParameter("?name", name);

            return _genericRepository.FetchSingleInt(sql) > 0;
        }
    }
}
