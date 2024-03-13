using Model.Entities.Car;

namespace Model.Repositories.Interfaces
{
    public interface ICarMakeRepository
    {
        bool CheckIfExsitsByName(string name);
        List<CarMake> GetMakes();
        void Insert(string name);
    }
}