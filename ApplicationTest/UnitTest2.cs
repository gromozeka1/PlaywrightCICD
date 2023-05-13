using ApplicationTest.Fixture;
using ApplicationTest.Models;
using ApplicationTest.Pages;
using AutoFixture.Xunit2;
using Microsoft.Playwright;

namespace ApplicationTest
{
    public class UnitTest2
    {
        private readonly ITestFixtureBase _testFixtureBase;
        private readonly IProductListPage _productListPage;
        private readonly IProductPage _productPage;

        public UnitTest2(ITestFixtureBase testFixtureBase, IProductListPage productListPage, IProductPage productPage)
        {
            _testFixtureBase = testFixtureBase;
            _productListPage = productListPage;
            _productPage = productPage;
        }

        [Theory(Skip = "Skipping local test"), AutoData]
        public async Task TestWithAutoFixtureData(Product product)
        {
            await _testFixtureBase.NavigateToUrl();

            await _productListPage.CreateProductAsync();

            await _productPage.CreateProduct(product);
            await _productPage.ClickCreate();

            await _productListPage.ClickProductFromList(product.Name);

            var element = _productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }
    }
}