using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;
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

        public CarWorkshopBasicData GetBasicByUsername(string username)
        {
            var sql = new MySqlCommand($@"
            SELECT Id, Username, CompanyName, Email
            FROM {TABLE_NAME}
            WHERE Username = ?username");

            sql.AddParameter("?username", username);

            return _genericRepository.FetchSingle<CarWorkshopBasicData>(sql);
        }

        public bool ValidateCredentials(string username, string password)
        {
            var sql = new MySqlCommand(@$"
            SELECT COUNT(*)
            FROM {TABLE_NAME}
            WHERE Username = ?username AND Password = ?password");

            sql.AddParameter("?username", username);
            sql.AddParameter("?password", password);

            return _genericRepository.FetchSingleInt(sql) > 0;
        }

        public List<CarWorkshopDisplayBasicData> List(ListArgs args)
        {
            var sql = new MySqlCommand($@"
            SELECT 
                sshp.CompanyName,
                sshp.Description,
                sc.Name AS Specialization,
                AVG(f.Rating) AS AverageRating
            FROM serviceShop sshp
            INNER JOIN serviceshopspecialization sshpsc ON sshp.Id = sshpsc.ShopId
            INNER JOIN specializations sc ON sc.Id = sshpsc.SpecializationId
            LEFT JOIN feedback f ON sshp.Id = f.ShopId");


            return _genericRepository.FetchList<CarWorkshopDisplayBasicData>(sql);
        }
    }
}
