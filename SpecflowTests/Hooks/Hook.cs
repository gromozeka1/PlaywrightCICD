using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;

namespace SpecflowTests.Hooks
{
    [Binding]
    public class Hook
    {
        private readonly TestSettings _testSettings;
        private readonly Task<IPage> _page;

        public Hook(IPlaywrightDriver playwrightDriver, TestSettings testSettings)
        {
            _testSettings = testSettings;
            _page = playwrightDriver.Page;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await (await _page).GotoAsync(_testSettings.ApplicationUrl);
        }
    }
}
