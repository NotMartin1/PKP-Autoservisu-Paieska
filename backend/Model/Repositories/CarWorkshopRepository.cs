﻿using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;
using Model.Exstensions;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System.Data.SqlClient;
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


        public void SetWorkingHours(int carWorkshopId, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
        {
            var sql = new MySqlCommand($@"
            INSERT INTO workinghours
            (ShopId, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday)
            VALUES
            (?carWorkshopId, ?monday, ?tuesday, ?wednesday, ?thursday, ?friday, ?saturday, ?sunday)");

            sql.AddParameter("?carWorkshopId", carWorkshopId);
            sql.AddParameter("?monday", monday);
            sql.AddParameter("?tuesday", tuesday);
            sql.AddParameter("?wednesday", wednesday);
            sql.AddParameter("?thursday", thursday);
            sql.AddParameter("?friday", friday);
            sql.AddParameter("?saturday", saturday);
            sql.AddParameter("?sunday", sunday);

            _genericRepository.ExecuteNonQuery(sql);


            var lastInsertedId = _genericRepository.GetLastInsertedId();

            // Update the ServiceShop table with the WorkingHours ID
            var updateSql = new MySqlCommand(@$"
    UPDATE serviceshop
    SET WorkingHoursId = ?workingHoursId
    WHERE Id = ?carWorkshopId");

            updateSql.AddParameter("?workingHoursId", lastInsertedId);
            updateSql.AddParameter("?carWorkshopId", carWorkshopId);

            _genericRepository.ExecuteNonQuery(updateSql);

        }

        public CarWorkshopDetails GetCarWorkshopDetails(int id)
        {
            var sql = new MySqlCommand($@"
            SELECT 
                sshp.CompanyName,
                sshp.Address,
                sshp.PhoneNumber,
                sshp.Email,
                sshp.Website,
                sshp.Description,
                sc.Name AS Specialization,
                wh.Monday,
                wh.Tuesday,
                wh.Wednesday,
                wh.Thursday,
                wh.Friday,
                wh.Saturday,
                wh.Sunday
            FROM serviceShop sshp
            LEFT JOIN serviceshopspecialization sshpsc ON sshp.Id = sshpsc.ShopId
            LEFT JOIN specializations sc ON sc.Id = sshpsc.SpecializationId
            LEFT JOIN workinghours wh ON sshp.WorkingHoursId = wh.ShopId
            WHERE sshp.Id = ?id");

            sql.AddParameter("?id", id);

            var workingHours = GetCarWorkshopWorkingHours(id);
            var carWorkshopDetails = _genericRepository.FetchSingle<CarWorkshopDetails>(sql);
            carWorkshopDetails.WorkingHours = workingHours;
            return carWorkshopDetails;

        }

        public WorkingHours GetCarWorkshopWorkingHours(int id)
        {
            var sql = new MySqlCommand($@"
            SELECT 
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday,
                Saturday,
                Sunday
            FROM workinghours
            WHERE ShopId = ?id");

            sql.AddParameter("?id", id);

            return _genericRepository.FetchSingle<WorkingHours>(sql);
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
            LEFT JOIN serviceshopspecialization sshpsc ON sshp.Id = sshpsc.ShopId
            LEFT JOIN specializations sc ON sc.Id = sshpsc.SpecializationId
            LEFT JOIN feedback f ON sshp.Id = f.ShopId");

            var where = new List<string>();

            var companyNameFilter = args.Filters.FirstOrDefault(x => string.Equals("CompanyName", x.ColumnName));
            if (companyNameFilter != null)
            {
                where.Add($"sshp.CompanyName LIKE '%{companyNameFilter.Value}%'");
            }

            var specializationIdFilter = args.Filters.FirstOrDefault(x => string.Equals("SpecializationId", x.ColumnName));
            if (specializationIdFilter != null)
            {
                where.Add("sc.Id = ?specializationId");
                sql.AddParameter("?specializationId", specializationIdFilter.Value);
            }


            if (where.Count > 0)
                sql.CommandText += $" WHERE {string.Join(" AND ", where)}";

            sql.CommandText += " GROUP BY sshp.CompanyName, sshp.Description, sc.Name";

            var ratingFilter = args.Filters.FirstOrDefault(x => string.Equals("Raiting", x.ColumnName));
            if (ratingFilter != null)
            {
                sql.CommandText += " HAVING AVG(f.Rating) >= ?raiting";
                sql.AddParameter("?raiting", ratingFilter.Value);
            }

            var columns = new List<string>
            {
                "CompanyName",
                "Description",
                "Specialization",
                "AverageRating"
            };

            if (columns.Contains(args.OrderColumnName) && args.OrderDirection.HasValue)
            {
                var direction = args.OrderDirection.Value == OrderDirection.DESC ? "DESC" : "ASC";
                sql.CommandText += $" ORDER BY {args.OrderColumnName} {direction}";
            }

            if (args.Take != 0)
            {
                sql.CommandText += $" LIMIT {args.Skip.Value}, {args.Take.Value}";
            }

            return _genericRepository.FetchList<CarWorkshopDisplayBasicData>(sql);
        }

        public bool CheckIfCompanyNameExsits(string companyName)
        {
            var sql = new MySqlCommand(@$"
            SELECT COUNT(*) 
            FROM {TABLE_NAME}
            WHERE CompanyName = ?companyName");

            sql.AddParameter("?companyName", companyName);

            return _genericRepository.FetchSingleInt(sql) > 0;
        }
    }
}
