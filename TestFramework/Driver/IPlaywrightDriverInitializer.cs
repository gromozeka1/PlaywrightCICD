using Microsoft.Playwright;
using TestFramework.Config;

namespace TestFramework.Driver
{
    public interface IPlaywrightDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings);
    }
}