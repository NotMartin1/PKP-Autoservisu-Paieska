using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public interface IGenericRepository
    {
        void ExecuteNonQuery(MySqlCommand command);
        object ExecuteReader(MySqlCommand sql);
        List<T> FetchList<T>(MySqlCommand command) where T : class, new();
        T FetchSingle<T>(MySqlCommand command) where T : class, new();
        int FetchSingleInt(MySqlCommand command);
        object GetLastInsertedId();
        IDisposable GetSqlConnection();
    }
}