
using Microsoft.Playwright;
using NUnit.Framework;
using snipeIT_autoTests.Pages;

namespace snipeIT_autoTests;

public partial class VerifyAssetHistoryTests
{
    [Order(3)]
    [Test]
    public async Task VerifyAssetCreationHistory()
    {
        using var playwright = await Playwright.CreateAsync();

        var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                ExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
                Headless = false
            });

        try
        {
            var page = await browser.NewPageAsync();

            // Log in
            var loginPage = new LoginPage();
            await loginPage.Login(page);

            // Get the asset tag from the creation test
            string assetTag = AssetTests.CreatedAssetTag
                ?? throw new InvalidOperationException(
                    "No asset tag was captured. Run the creation test first.");
            await page.GotoAsync("https://demo.snipeitapp.com/hardware");
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            // Search for the asset
            var searchField = page.GetByRole(
                AriaRole.Searchbox,
                new() { Name = "Search" }).First;

            await searchField.FillAsync(assetTag);
            await searchField.PressAsync("Enter");

            // Click the asset tag
            await page.WaitForTimeoutAsync(3000);
            // Wait for the asset tag to appear in the search results
             var assetResult = page.GetByText(
             assetTag, new() { Exact = true }).First;

            await assetResult.WaitForAsync();
            await assetResult.ClickAsync();

            Console.WriteLine("Current URL after search: " + page.Url);
            Console.WriteLine("Search results: " +
                await page.Locator("body").InnerTextAsync());
            
            await page.WaitForTimeoutAsync(3000);
            // Open the History tab
            await page.Locator("a[href='#history']").ClickAsync();
            await page.Locator("#history").WaitForAsync();

            // Read and display the history
            var historyText = await page.Locator("#history").InnerTextAsync();
            Console.WriteLine("Asset History:");
            Console.WriteLine(historyText);
            
            await page.WaitForTimeoutAsync(3000);
            // Verify Created At is displayed
            Assert.That(historyText, Does.Contain("Created At"));
            await page.WaitForTimeoutAsync(3000);
        }
        finally
        {
            await browser.CloseAsync();
        }
    }
}
