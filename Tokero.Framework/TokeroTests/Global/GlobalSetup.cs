using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightFramework.TokeroTests.Global
{
    public static class GlobalSetup
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private static IPage _currentPage;  

        public static async Task InitializeAsync()
        {
            if (_browser != null) return; 

            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--disable-cache", "--incognito" }
            });
        }

        public static IBrowser GetBrowser()
        {
            return _browser;
        }

        
        public static void SetCurrentPage(IPage page)
        {
            _currentPage = page;
        }

        
        public static async Task<IPage> GetCurrentPageAsync()
        {
            return _currentPage;
        }

        public static async Task<IBrowser> InitializeBrowserAsync(string browserName)
        {
            return browserName.ToLowerInvariant() switch
            {
                "chromium" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }),
                "firefox"  => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }),
                "webkit"   => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }),
                _ => throw new ArgumentException($"Unsupported browser: {browserName}")
            };
        }

        public static async Task CleanupAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }
    }
}
