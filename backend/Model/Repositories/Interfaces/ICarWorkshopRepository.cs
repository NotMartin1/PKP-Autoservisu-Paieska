using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;

namespace Model.Repositories.Interfaces
{
    public interface ICarWorkshopRepository
    {
        bool CheckIfCompanyNameExsits(string companyName);
        bool CheckIfExsistsById(int id);
        CarWorkshopBasicData GetBasicByUsername(string username);
        string GetPasswordByUsername(string username);
        bool CheckIfExsitsByUsername(string username);
        void Insert(CarWorkshopExtendedData args);
        List<CarWorkshopDisplayBasicData> List(ListArgs args);
        bool ValidateCredentials(string username, string password);
    }
}