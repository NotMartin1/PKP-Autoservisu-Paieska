using Model.Entities.Client;
using Model.Exstensions;
using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IGenericRepository _genericRepository;

        private const string TABLE_NAME = "client";

        public ClientRepository(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Insert(ClientExtendedData client)
        {
            var sql = new MySqlCommand($@"
            INSERT INTO {TABLE_NAME}
            (Username, Password, Fullname, CreatedAt, IsEnabled)
            VALUES
            (?username, ?password, ?fullname, NOW(), ?isEnabled)");

            sql.AddParameter("?username", client.Username);
            sql.AddParameter("?password", client.Password);
            sql.AddParameter("?fullname", client.Fullname);
            sql.AddParameter("?isEnabled", client.IsEnabled);

            _genericRepository.ExecuteNonQuery(sql);
        }

        public bool CheckIfExsitsByUsername(string username)
        {
            var sql = new MySqlCommand($@"
            SELECT COUNT(*) FROM {TABLE_NAME}
            WHERE Username = ?username");

            sql.AddParameter("?username", username);

            return _genericRepository.FetchSingleInt(sql) > 0;
        }
    }
}
