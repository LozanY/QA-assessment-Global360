using Microsoft.Playwright;
using NUnit.Framework;
using snipeIT_autoTests.Pages;

namespace snipeIT_autoTests;

public partial class AssetTests
{
[Order(2)]
[Test]
public async Task ValidateCreatedAsset()
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

        // Search using the asset tag captured in the creation test
        string assetTag = AssetTests.CreatedAssetTag
            ?? throw new InvalidOperationException("No asset tag was captured.");

        // Open the asset list first
            await page.GotoAsync("https://demo.snipeitapp.com/hardware");

        // Search for the asset
            var searchField = page.GetByRole(
                AriaRole.Searchbox,
                new() { Name = "Search" }).First;

            await searchField.WaitForAsync();
            await searchField.FillAsync(assetTag);
            await searchField.PressAsync("Enter");

        // Wait for search results to finish loading
            await page.WaitForURLAsync("**/hardware?**");

            var assetResult = page.GetByRole(AriaRole.Link)
                .Filter(new() { HasText = assetTag })
                .First;

            await assetResult.WaitForAsync();
            await assetResult.ClickAsync();

        // Confirm the asset details page has loaded
            await page.WaitForTimeoutAsync(2000);


    }
    finally
    {
        await browser.CloseAsync();
    }
}
}