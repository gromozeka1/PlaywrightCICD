﻿using Microsoft.Playwright;
using SpecflowTests.Models;
using TestFramework.Driver;

namespace SpecflowTests.Pages
{
    public interface IProductPage
    {
        Task ClickCreate();
        Task CreateProduct(Product product);
    }

    public class ProductPage : IProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPlaywrightDriver playwrightDriver)
            => _page = playwrightDriver.Page.Result;

        private ILocator TxtName => _page.GetByLabel("Name");
        private ILocator TxtDescription => _page.GetByLabel("Description");
        private ILocator TxtPrice => _page.Locator("#Price");
        private ILocator SelectProductType => _page.GetByRole(AriaRole.Combobox, new() { Name = "ProductType" });

        private ILocator LnkCreate => _page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task CreateProduct(Product product)
        {
            await TxtName.FillAsync(product.Name);
            await TxtDescription.FillAsync(product.Description);
            await TxtPrice.FillAsync(product.Price.ToString());
            await SelectProductType.SelectOptionAsync(product.ProductType.ToString());
        }

        public async Task ClickCreate()
        {
            await LnkCreate.ClickAsync();
        }
    }
}
