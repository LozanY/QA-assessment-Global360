
using Microsoft.Playwright;
using NUnit.Framework;

namespace snipeIT_autoTests;

public class baseTest
{
    protected IPlaywright playwright;
    protected IBrowser browser;
    protected IPage page;

    [SetUp]
    public async Task SetUp()
    {
        playwright = await Playwright.CreateAsync();

        browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                ExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
                Headless = false
            });

        page = await browser.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await browser.CloseAsync();
        playwright.Dispose();
    }
}
