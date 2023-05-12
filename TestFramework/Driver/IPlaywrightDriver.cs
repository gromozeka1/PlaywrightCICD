using Microsoft.Playwright;

namespace TestFramework.Driver
{
    public interface IPlaywrightDriver
    {
        Task<IBrowser> Browser { get; }
        Task BrowserContext { get; }
        Task<IPage> Page { get; }
    }
}