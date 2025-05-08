using Microsoft.Playwright;

namespace PlaywrightFramework.TokeroTests.Tests.Integration
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    public class PerformanceTests
    {
        [Test, Category("Performance")]
        public async Task MeasurePageLoadTime()
        {
            var loadTimes = new List<double>();

            for (int i = 0; i < 5; i++)
            {
                using var playwright = await Playwright.CreateAsync();
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    Args = new[] { "--disable-cache", "--incognito" }
                });

                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();

                var startTime = DateTime.Now;
                await page.GotoAsync("https://tokero.dev/en/");
                var endTime = DateTime.Now;

                var loadTime = (endTime - startTime).TotalMilliseconds;
                loadTimes.Add(loadTime);

                Console.WriteLine($"⏳ Run {i + 1}: Page loaded in {loadTime} ms.");

                await browser.CloseAsync();
            }

            double averageLoadTime = loadTimes.Average();
            Console.WriteLine($"📊 Average page load time: {averageLoadTime} ms.");

            var dir = @"C:\Users\butin\RiderProjects\TokeroAutomation\Tokero.Framework\TokeroTests\Reports";
            Directory.CreateDirectory(dir);

            var fileName = $"PageLoadTime_Avg_TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var filePath = Path.Combine(dir, fileName);

            try
            {
                await File.AppendAllLinesAsync(filePath, new[]
                {
                    $"Test Run: {DateTime.Now}",
                    string.Join(Environment.NewLine, loadTimes.Select((t, index) => $"Run {index + 1}: {t} ms")),
                    $"Average Load Time: {averageLoadTime} ms",
                    new string('-', 40)
                });

                Console.WriteLine($"Results saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        [Test, Category("Performance")]
        public async Task MeasureButtonClickResponseTime_Average()
        {
            var responseTimes = new List<double>();

            for (int i = 0; i < 5; i++)
            {
                using var playwright = await Playwright.CreateAsync();
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    Args = new[] { "--disable-cache", "--incognito" }
                });

                var context = await browser.NewContextAsync();
                var page = await context.NewPageAsync();

                await page.GotoAsync("https://tokero.dev/en/");
                var buttonSelector = "//button[text()='Accept all cookies']";

                var startTime = DateTime.Now;
                await page.ClickAsync(buttonSelector);
                var endTime = DateTime.Now;

                var responseTime = (endTime - startTime).TotalMilliseconds;
                responseTimes.Add(responseTime);

                Console.WriteLine($"⏳ Run {i + 1}: Button click responded in {responseTime} ms.");

                await browser.CloseAsync();
            }

            double average = responseTimes.Average();
            Console.WriteLine($"📊 Average response time: {average} ms.");

            var dir = @"C:\Users\butin\RiderProjects\TokeroAutomation\Tokero.Framework\TokeroTests\Reports";
            Directory.CreateDirectory(dir);

            var filePath = Path.Combine(dir, "ClickResponseTimes.txt");

            try
            {
                await File.AppendAllLinesAsync(filePath, new[]
                {
                    $"Test Run: {DateTime.Now}",
                    string.Join(Environment.NewLine, responseTimes.Select((t, index) => $"Run {index + 1}: {t} ms")),
                    $"Average: {average} ms",
                    new string('-', 40)
                });

                Console.WriteLine($"Results saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }

            Assert.That(average, Is.LessThan(500), "Average response time exceeded 500ms.");
        }
    }
}
