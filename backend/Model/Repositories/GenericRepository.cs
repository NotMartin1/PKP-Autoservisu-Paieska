using Microsoft.Extensions.Configuration;
using Model.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Model.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IConfiguration _configuration;

        private const string CONNECTION_STRING_IDENTIFIER = "MySql";

        public GenericRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<T> MapToList<T>(MySqlDataReader reader) where T : class, new()
        {
            var resultList = new List<T>();

            while (reader.Read())
            {
                var item = MapToObject<T>(reader);
                resultList.Add(item);
            }

            return resultList;
        }

        private T MapToObject<T>(IDataRecord reader) where T : class, new()
        {
            var item = new T();
            var properties = typeof(T).GetProperties();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var property = properties.FirstOrDefault(p => p.Name == columnName);

                 if (property != null && reader[columnName] != DBNull.Value)
                {
                    var value = reader[columnName];
                    var targetType = property.PropertyType;

                    if (targetType == typeof(bool) && value.GetType() == typeof(short))
                    {
                        value = Convert.ToBoolean(value);
                    }

                    property.SetValue(item, value);
                }
            }


            return item;
        }


        public T FetchSingle<T>(MySqlCommand command) where T : class, new()
        {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return MapToObject<T>(reader);
                    else
                        return default;
                }
            }
        }

        public int FetchSingleInt(MySqlCommand command)
        {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                command.Connection = connection;

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public List<T> FetchList<T>(MySqlCommand command) where T : class, new()
        {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    return MapToList<T>(reader);
                }
            }
        }

        public void ExecuteNonQuery(MySqlCommand command)
        {
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                command.Connection = connection;

                command.ExecuteNonQuery();
            }
        }
        private MySqlConnection GetSqlConnection()
        {
            var connectionString = _configuration.GetConnectionString(CONNECTION_STRING_IDENTIFIER);
            return new MySqlConnection(connectionString);
        }
    }
}
