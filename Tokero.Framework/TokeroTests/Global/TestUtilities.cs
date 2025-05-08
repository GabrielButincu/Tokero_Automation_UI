namespace TokeroTests.Global;

using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;

public static class TestUtilities
{
    public static async Task CaptureScreenshot(IPage page, string testName)
    {
        string path = $"Reports/{testName}.png";
        await page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
        Console.WriteLine($"🖼 Screenshot saved: {path}");
    }
}