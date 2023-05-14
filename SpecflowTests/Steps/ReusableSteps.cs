using Microsoft.Playwright;
using TestFramework.Driver;

namespace SpecflowTests.Steps
{
    [Binding]
    public class ReusableSteps
    {
        private readonly IPlaywrightDriver _playwrightDriver;

        public ReusableSteps(IPlaywrightDriver playwrightDriver)
        {
            _playwrightDriver = playwrightDriver;
        }

        [Given(@"I ensure ""([^""]*)"" data is cleaned up if already exists")]
        public async Task GivenIEnsureDataIsCleanedUpIfAlreadyExists(string productName)
        {
            await (await _playwrightDriver.ApiRequestContext).DeleteAsync("Product/DeleteByName",
                new APIRequestContextOptions()
                {
                    Params = new Dictionary<string, object>
                    {
                        {"productName", productName}
                    }
                });
        }
    }
}
