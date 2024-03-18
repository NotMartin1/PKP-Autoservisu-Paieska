using Model.Entities.Client;

namespace Model.Repositories.Interfaces
{
    public interface IClientRepository
    {
        void Insert(ClientExtendedData client);
        ClientBasicData GetBasicByUsername(string username);
        bool CheckIfExsitsByUsername(string username);
        bool ValidateCredentials(string username, string password);
        bool CheckIfExsitsById(int id);
    }
}