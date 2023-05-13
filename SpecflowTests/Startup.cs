using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using SpecflowTests.Pages;
using TestFramework.Config;
using TestFramework.Driver;

namespace SpecflowTests
{
    public class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
                .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
                .AddScoped<IProductListPage, ProductListPage>()
                .AddScoped<IProductPage, ProductPage>();

            return services;
        }
    }
}
