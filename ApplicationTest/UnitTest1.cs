using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;

namespace ApplicationTest
{
    public class UnitTest1 : IClassFixture<PlaywrightDriverInitializer>
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly PlaywrightDriverInitializer _playwrightDriverInitializer;

        public UnitTest1(PlaywrightDriverInitializer playwrightDriverInitializer)
        {
            var testSettings = new TestSettings
            {
                DriverType = DriverType.Chromium,
                Headless = false,
                Timeout = 150000,
                SlowMo = 2000,
                ApplicationUrl = "http://eaapp.somee.com/",
            };

            _playwrightDriverInitializer = playwrightDriverInitializer;
            _playwrightDriver = new PlaywrightDriver(testSettings, _playwrightDriverInitializer);
        }

        [Fact]
        public async Task Test1()
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://eaapp.somee.com/");

            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await page.GetByLabel("UserName").FillAsync("admin");

            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();
        }
    }
}