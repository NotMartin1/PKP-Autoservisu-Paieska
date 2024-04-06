using Model.Entities.Client;

namespace Model.Repositories
{
    public interface IClientRepository
    {
        bool CheckIfExsitsByUsername(string username);
        void Insert(ClientExtendedData client);
    }
}