using Model.Entities.Car.Request;

namespace Model.Repositories.Interfaces
{
    public interface ICarRepository
    {
        void Insert(CarAddRequest args);
    }
}