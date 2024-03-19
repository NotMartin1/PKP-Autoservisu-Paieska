using MySql.Data.MySqlClient;

namespace Model.Repositories
{
    public interface IGenericRepository
    {
        void ExecuteNonQuery(MySqlCommand command);
        List<T> FetchList<T>(MySqlCommand command) where T : class, new();
        T FetchSingle<T>(MySqlCommand command) where T : class, new();
        int FetchSingleInt(MySqlCommand command);
        string FetchSingleString(MySqlCommand command);
    }
}