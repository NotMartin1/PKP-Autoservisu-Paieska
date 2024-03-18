using Model.Repositories;
using Model.Repositories.Interfaces;
using Model.Services;
using Model.Services.Interfaces;
using SimpleInjector;

namespace Model
{
    public class ObjectContainerInitializer
    {
        public static void Init(Container container)
        {
            container.Register<IClientService, ClientService>();
            container.Register<ICarWorkshopService, CarWorkshopService>();
            container.Register<ICarService, CarService>();
            container.Register<IClientRepository, ClientRepository>();
            container.Register<ICarWorkshopRepository, CarWorkshopRepository>();
            container.Register<IGenericRepository, GenericRepository>();
            container.Register<IValidationService, ValidationService>();
            container.Register<ICarMakeRepository, CarMakeRepository>();
            container.Register<ICarRepository, CarRepository>();
        }
    }
}
