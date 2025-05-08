using PlaywrightFramework.TokeroTests.Global;
using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace TokeroTests.Tests
{
    public class BaseTest
    {
        protected IBrowserContext _context;
        protected IPage _page;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            await GlobalSetup.InitializeAsync();
        }

        [SetUp]
        public async Task SetUp()
        {
            var browser = GlobalSetup.GetBrowser();
            _context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null // 
            });
            _page = await _context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await GlobalSetup.CleanupAsync();
        }
    }
}