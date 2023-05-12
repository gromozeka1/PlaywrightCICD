using ApplicationTest.Models;
using ApplicationTest.Pages;
using AutoFixture.Xunit2;
using Microsoft.Playwright;
using TestFramework.Config;
using TestFramework.Driver;

namespace ApplicationTest
{
    public class UnitTest1
    {
        private readonly IPlaywrightDriver _playwrightDriver;
        private readonly TestSettings _testSettings;
        private readonly IProductListPage _productListPage;
        private readonly IProductPage _productPage;

        public UnitTest1(IPlaywrightDriver playwrightDriver, TestSettings testSettings, IProductListPage productListPage, IProductPage productPage)
        {
            _playwrightDriver = playwrightDriver;
            _testSettings = testSettings;
            _productListPage = productListPage;
            _productPage = productPage;
        }

        [Theory, AutoData]
        public async Task TestWithAutoFixtureData(Product product)
        {
            var page = await _playwrightDriver.Page;

            await page.GotoAsync("http://localhost:8000/");

            await _productListPage.CreateProductAsync();

            await _productPage.CreateProduct(product);
            await _productPage.ClickCreate();

            await _productListPage.ClickProductFromList(product.Name);

            var element = _productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }
    }
}