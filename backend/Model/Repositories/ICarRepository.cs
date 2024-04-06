using Model.Entities.Car.Request;

namespace Model.Repositories
{
    public interface ICarRepository
    {
        void Delete(CarDeleteRequest request);
        void Insert(CarAddRequest args);
    }
}