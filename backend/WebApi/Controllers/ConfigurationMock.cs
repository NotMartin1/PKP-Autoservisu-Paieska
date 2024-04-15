using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Test
{
    public static class ConfigurationMock
    {
        public static Container GetContainer()
        {
            Container container = new Container();
            container.RegisterInstance(GetConfiguration());
            Model.ObjectContainerInitializer.Init(container);
            return container;
        }

        private static IConfiguration GetConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration;
        }
    }
}
