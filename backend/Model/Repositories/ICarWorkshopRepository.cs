using Model.Entities.CarService;

namespace Model.Repositories
{
    public interface ICarWorkshopRepository
    {
        bool CheckIfCompanyNameExsits(string companyName);
        void Insert(CarWorkshopExtendedData args);
        bool ValidateCredentials(string username, string password);
    }
}