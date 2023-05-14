using ApplicationTest.Models;
using TestFramework.Driver;
using System.Text.Json;
using FluentAssertions.Execution;
using FluentAssertions;
using Microsoft.Playwright;

namespace ApplicationTest.ApiTests
{
    public class ProductsApiTests
    {
        private readonly IPlaywrightDriver _playwrightDriver;

        public ProductsApiTests(IPlaywrightDriver playwrightDriver)
        {
            _playwrightDriver = playwrightDriver;
        }

        [Fact]
        public async Task GetProductsAsync()
        {
            var productResponse = await (await _playwrightDriver.ApiRequestContext).GetAsync("Product/GetProductById/1");

            var data = await productResponse.JsonAsync();

            var product = data?.Deserialize<Product>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            using (new AssertionScope())
            {
                productResponse.Status.Should().Be(200);
                product?.Name.Should().Be("Keyboard");
                product?.Price.Should().Be(150);
                product?.Id.Should().Be(1);
            }
        }

        [Fact]
        public async Task DeleteProductAsync()
        {
            var deleteResponse =  await (await _playwrightDriver.ApiRequestContext).DeleteAsync("Product/Delete", 
                new APIRequestContextOptions()
                {
                    Params = new Dictionary<string, object>
                    {
                        {"id", "1" }
                    }
                });

            var getResponse = await (await _playwrightDriver.ApiRequestContext).GetAsync("Product/GetProductById/1");

            using (new AssertionScope())
            {
                deleteResponse.Status.Should().Be(200);
                getResponse.Status.Should().Be(204);
            }
        }
    }
}
