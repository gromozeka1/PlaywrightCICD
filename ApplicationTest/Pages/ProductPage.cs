using Microsoft.Playwright;

namespace ApplicationTest.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }

        private ILocator TxtName => _page.GetByLabel("Name");
        private ILocator TxtDescription => _page.GetByLabel("Description");
        private ILocator TxtPrice => _page.Locator("#Price");
        private ILocator SelectProductType => _page.GetByRole(AriaRole.Combobox, new () { Name = "ProductType" });

        private ILocator LnkCreate => _page.GetByRole(AriaRole.Button, new () { Name = "Create" });

        public async Task CreateProduct(string name, string description, decimal price, string productType)
        {
            await TxtName.FillAsync(name);
            await TxtDescription.FillAsync(description);
            await TxtPrice.FillAsync(price.ToString());
            await SelectProductType.SelectOptionAsync(productType);
        }

        public async Task ClickCreate()
        {
            await LnkCreate.ClickAsync();
        }
    }
}
