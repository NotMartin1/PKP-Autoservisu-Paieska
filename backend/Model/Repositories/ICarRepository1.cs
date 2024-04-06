using Model.Entities.Car.Request;

namespace Model.Repositories
{
    public interface ICarRepository1
    {
        void Insert(CarAddRequest args);
    }
}