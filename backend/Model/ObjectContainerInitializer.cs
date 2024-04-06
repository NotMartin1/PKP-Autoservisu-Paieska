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
        }
    }
}
