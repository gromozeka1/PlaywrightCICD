using Microsoft.Playwright;
using TestFramework.Driver;

namespace ApplicationTest.Pages
{
    public interface IProductListPage
    {
        Task ClickProductFromList(string name);
        Task CreateProductAsync();
        ILocator IsProductCreated(string product);
    }

    public class ProductListPage : IProductListPage
    {
        private readonly IPage _page;

        public ProductListPage(IPlaywrightDriver playwrightDriver) 
            => _page = playwrightDriver.Page.Result;

        private ILocator LnkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });

        private ILocator LnkCreate => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });

        public async Task CreateProductAsync()
        {
            await LnkProductList.ClickAsync();
            await LnkCreate.ClickAsync();
        }

        public async Task ClickProductFromList(string name)
        {
            await _page.GetByRole(AriaRole.Row, new() { Name = name })
                .GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
        }

        public ILocator IsProductCreated(string product)
        {
            return _page.GetByText(product, new PageGetByTextOptions { Exact = true });
        }
    }
}
