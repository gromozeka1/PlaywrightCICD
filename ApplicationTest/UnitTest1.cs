using ApplicationTest.Models;
using ApplicationTest.Pages;
using AutoFixture.Xunit2;
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

            //await productPage.CreateProduct("Speaker", "Speaker description", 2000, "2");
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList("Speaker");

            var element = productListPage.IsProductCreated("Speaker");
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Theory]
        [InlineData("Speaker1", "Gaming speaker", 2000, "2")]
        [InlineData("USB1", "USB 3.0", 300, "3")]
        [InlineData("Webcam1", "Camera", 4000, "2")]
        [InlineData("Wires1", "Wires For Life", 1000, "2")]
        public async Task TestWithInlineData(string name, string description, int price, string productType)
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://localhost:8000/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);

            await productListPage.CreateProductAsync();

            //await productPage.CreateProduct(name, description, price, productType);
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList(name);

            var element = productListPage.IsProductCreated(name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Fact]
        public async Task TestWithConcreteTypes()
        {
            var page = await _playwrightDriver.Page;

            var product = new Product()
            {
                Name = "Test product 1",
                Description = "Test product description",
                Price = 1000,
                ProductType = ProductType.CPU
            };

            await page.GotoAsync("http://localhost:8000/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);

            await productListPage.CreateProductAsync();

            await productPage.CreateProduct(product);
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList(product.Name);

            var element = productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Theory, AutoData]
        public async Task TestWithAutoFixtureData(Product product)
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://localhost:8000/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);

            await productListPage.CreateProductAsync();

            await productPage.CreateProduct(product);
            await productPage.ClickCreate();

            await productListPage.ClickProductFromList(product.Name);

            var element = productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }
    }
}