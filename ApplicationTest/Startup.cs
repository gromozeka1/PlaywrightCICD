using ApplicationTest.Pages;
using Microsoft.Extensions.DependencyInjection;
using TestFramework.Config;
using TestFramework.Driver;

namespace ApplicationTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IPlaywrightDriver, PlaywrightDriver>()
                .AddScoped<IPlaywrightDriverInitializer, PlaywrightDriverInitializer>()
                .AddScoped<IProductListPage, ProductListPage>()
                .AddScoped<IProductPage, ProductPage>();
        }
    }
}
