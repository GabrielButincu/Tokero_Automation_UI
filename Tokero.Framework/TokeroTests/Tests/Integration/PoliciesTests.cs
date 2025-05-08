using Microsoft.Playwright;
using TokeroTests.Tests;

[TestFixture, Parallelizable(ParallelScope.All)]
public class PolicyTests : BaseTest
{
    [Test, Category("Policies")]
    public async Task VerifyAllPolicyPagesLoadProperly()
    {
        await _page.GotoAsync("https://tokero.dev/en/policies/");

        await _page.ClickAsync("//button[contains(@class, 'cookieConsentPopup_acceptCookiesBtn__w2Y0c')]");

        await _page.WaitForSelectorAsync(
            "div.mud-container.mud-container-maxwidth-lg.mud-container--gutters",
            new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        var elements = await _page.Locator("div.text-center a[href^='/en/policies/']").ElementHandlesAsync();
        List<string> links = new();

        foreach (var element in elements)
        {
            var href = await element.GetAttributeAsync("href");
            if (!string.IsNullOrEmpty(href))
            {
                links.Add(href);
            }
        }

        foreach (var policyUrl in links)
        {
            var fullUrl = $"https://tokero.dev{policyUrl}";
            Console.WriteLine($"🔍 Checking: {fullUrl}");

            await _page.GotoAsync(fullUrl);
            try
            {
                await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

                var loadingTextLocator = _page
                    .Locator("p.text-center.text-2xl.font-bold", new() { HasTextString = "Please wait, page is loading..." });

                await loadingTextLocator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

                await loadingTextLocator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden,
                    Timeout = 10000
                });

                Console.WriteLine("🔄 Loading text appeared and disappeared.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine("⚠️ Loading text did NOT appear or disappear in time. Continuing.");
            }

            var status = await _page.EvaluateAsync<string>("() => document.readyState");
            Assert.That(status, Is.EqualTo("complete"), $" {fullUrl} did not load properly.");

            var contentExists = await _page.Locator("body").IsVisibleAsync();
            Assert.That(contentExists, Is.True, $" {fullUrl} does not have visible content.");

            Console.WriteLine($"✅ {fullUrl} passed all checks.");
        }
    }
}