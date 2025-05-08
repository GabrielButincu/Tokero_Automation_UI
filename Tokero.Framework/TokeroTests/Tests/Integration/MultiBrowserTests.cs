using PlaywrightFramework.TokeroTests.Global;


namespace TokeroTests.Tests.Integration
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class MultiBrowserTests : BaseTest
    {
        private List<string> _browserNames;

        [Test, Category("CrossBrowser")]
        public async Task TestFooterAcrossMultipleBrowsers()
        {
            _browserNames = new List<string>
            {
                "chromium",
                "firefox",
                "webkit"
            };

            var tasks = _browserNames.Select(async browserName =>
            {
                var browser = await GlobalSetup.InitializeBrowserAsync(browserName);
                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();

                GlobalSetup.SetCurrentPage(page);

                await page.GotoAsync("https://tokero.dev/en/");

                bool isFooterVisible = await page.IsVisibleAsync("footer");
                Assert.That(isFooterVisible, Is.True, $"Footer not visible in {browserName}");

                TestContext.WriteLine($"✅ Footer visible in {browserName}");

                await context.CloseAsync();
            }).ToList();

            await Task.WhenAll(tasks);
        }
    }
}