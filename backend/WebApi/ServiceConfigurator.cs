using Model.Services;
using Model.Services.Interfaces;
using Server.Core.Middleware;
using SimpleInjector;

namespace WebApi
{
    public static class ServiceConfigurator
    {
        private static Container container = new Container();

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddControllersWithViews().
                            AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                            });

            services.AddControllersWithViews();
services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                       .AddControllerActivation();
            });

            Model.ObjectContainerInitializer.Init(container);

            services.AddScoped<TokenValidationFilter>();
        }
    }
}
