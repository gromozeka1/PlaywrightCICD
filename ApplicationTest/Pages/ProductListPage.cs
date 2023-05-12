using Microsoft.Playwright;

namespace ApplicationTest.Pages
{
    public class ProductListPage
    {
        private readonly IPage _page;

        public ProductListPage(IPage page)
        {
            _page = page;
        }

        private ILocator LnkProductList => _page.GetByRole(AriaRole.Link, new () { Name = "Product" });

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
