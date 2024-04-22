using Model.Entities.Client;

namespace Model.Repositories
{
    public interface IClientRepository
    {
        void Insert(ClientExtendedData client);
        ClientBasicData GetBasicByUsername(string username);
        bool CheckIfExsitsByUsername(string username);
        bool ValidateCredentials(string username, string password);
        bool CheckIfExsitsById(int id);
        ClientBasicData GetBasicById(int id);
    }
}