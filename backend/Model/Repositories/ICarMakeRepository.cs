using Model.Entities.Car;

namespace Model.Repositories
{
    public interface ICarMakeRepository
    {
        bool CheckIfExsitsById(int id);
        bool CheckIfExsitsByName(string name);
        List<CarMake> GetMakes();
        void Insert(string name);
    }
}