using Model.Entities.CarService;
using Model.Entities.CarWorkshop;
using Model.Entities.Filter;

namespace Model.Repositories
{
    public interface ICarWorkshopRepository
    {
        bool CheckIfCompanyNameExsits(string companyName);
        CarWorkshopBasicData GetBasicByUsername(string username);
        CarWorkshopDetails GetCarWorkshopDetails(int id);
        void Insert(CarWorkshopExtendedData args);
        List<CarWorkshopDisplayBasicData> List(ListArgs args);
        void SetWorkingHours(int id, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday);
        bool ValidateCredentials(string username, string password);
    }
}