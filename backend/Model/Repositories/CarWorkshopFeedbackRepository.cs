using Model.Entities.Feedback;
using Model.Entities.Feedback.Request;
using Model.Exstensions;
using Model.Repositories.Interfaces;
using Model.Services;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class CarWorkshopFeedbackRepository : ICarWokshopFeedackRepository
    {
        private readonly IGenericRepository _genericRepository;

        private const string TABLE_NAME = "feedback";

        public CarWorkshopFeedbackRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Create(FeedbackCreateRequest request)
        {
            var sql = new MySqlCommand($@"
            INSERT INTO {TABLE_NAME}
            (ShopId, ClientId, Rating, Message, CreatedAt)
            VALUES
            (?shopId, ?clientId, ?rating, ?message, NOW())");

            sql.AddParametersFromObject(request);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public List<FeedbackDisplayData> GetTotal(int? shopId)
        {
            var sql = new MySqlCommand($@"
            SELECT 
			    c.Username As ClientName,
                shp.CompanyName As WorkshopCompanyName,
                f.Message,
                f.Rating,
                f.CreatedAt
		        FROM feedback f
            INNER JOIN client c ON f.ClientId = c.Id
            INNER JOIN serviceshop shp ON f.ShopId = shp.Id");

            var where = new List<string>();

            if (shopId.HasValue)
            {
                where.Add("f.ShopId = ?shopId");
                sql.AddParameter("?shopId", shopId);
            }

            if (where.Count > 0)
                sql.CommandText += $" WHERE {string.Join(" AND ", where)}";

            return _genericRepository.FetchList<FeedbackDisplayData>(sql);
        }
    }
}
