using Model.Entities.CarService;
using Model.Exstensions;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class CarWorkshopRepository : ICarWorkshopRepository
    {
        private readonly IGenericRepository _genericRepository;

        private const string TABLE_NAME = "serviceShop";

        public CarWorkshopRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Insert(CarWorkshopExtendedData args)
        {
            var sql = new MySqlCommand($@"
            INSERT INTO {TABLE_NAME}
            (Username, Password, CompanyName, Address, PhoneNumber, Email, Website, Description)
            VALUES
            (?username, ?password, ?companyName, ?address, ?phoneNumber, ?email, ?website, ?description)");

            sql.AddParametersFromObject(args);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public bool CheckIfCompanyNameExsits(string companyName)
        {
            var sql = new MySqlCommand($@"
            SELECT COUNT(*)
            FROM {TABLE_NAME}
            WHERE CompanyName = ?companyName");

            sql.AddParameter("?companyName", companyName);

            return _genericRepository.FetchSingleInt(sql) > 0;
        }
    }
}
