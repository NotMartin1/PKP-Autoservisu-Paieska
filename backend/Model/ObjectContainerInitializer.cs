using Model.Repositories;
using Model.Services;
using SimpleInjector;

namespace Model
{
    public class ObjectContainerInitializer
    {
        public static void Init(Container container)
        {
            container.Register<ICarWorkshopService, CarWorkshopService>();
            container.Register<ICarWorkshopRepository, CarWorkshopRepository>();
            container.Register<IGenericRepository, GenericRepository>();
            container.Register<IValidationService, ValidationService>();
            container.Register<IClientService, ClientService>();
            container.Register<IClientRepository, ClientRepository>();
            container.Register<ICarService, CarService>();
            container.Register<ICarMakeRepository, CarMakeRepository>();
            container.Register<ICarRepository, CarRepository>();
            container.Register<IOrdersService, OrdersService>();
            container.Register<IOrdersRepository, OrdersRepository>();
        }
    }
}
