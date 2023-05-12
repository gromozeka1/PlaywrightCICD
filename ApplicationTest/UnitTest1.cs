using ApplicationTest.Pages;
using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;

namespace ApplicationTest
{
    public class UnitTest1 : IClassFixture<PlaywrightDriverInitializer>
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly PlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly TestSettings _testSettings;

        public UnitTest1(PlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = ConfigReader.ReadConfig();
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _playwrightDriver = new PlaywrightDriver(_testSettings, _playwrightDriverInitializer);
        }

        [Fact]
        public async Task Test1()
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync(_testSettings.ApplicationUrl);

            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await page.GetByLabel("UserName").FillAsync("admin");

            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();
        }

        [Fact]
        public async Task Test2()
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://localhost:8000/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);
            
            await productListPage.CreateProductAsync();

            await productPage.CreateProduct("Speaker", "Speaker description", 2000, "2");
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList("Speaker");

            var element = productListPage.IsProductCreated("Speaker");
            await Assertions.Expect(element).ToBeVisibleAsync();
        }
    }
}