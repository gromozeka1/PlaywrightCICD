using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;

namespace ApplicationTest.Fixture
{
    public interface ITestFixtureBase
    {
        Task NavigateToUrl();
        Task TakeScreenshotAsync(string fileName);
    }

    public class TestFixtureBase : ITestFixtureBase
    {
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;

        private Task<IPage> _page;

        public TestFixtureBase(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
        {
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _page = playwrightDriver.Page;
        }

        public async Task NavigateToUrl()
        {
            await (await _page).GotoAsync(_testSettings.ApplicationUrl);
        }

        public async Task TakeScreenshotAsync(string fileName)
        {
            await (await _page).ScreenshotAsync(new PageScreenshotOptions { Path = fileName, FullPage = true });
        }
    }
}
