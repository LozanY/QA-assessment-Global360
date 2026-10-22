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

        var searchField = page.GetByRole(
             AriaRole.Searchbox,
            new() { Name = "Search" }).First;

        await searchField.FillAsync(assetTag);
        await searchField.PressAsync("Enter");

        // Wait for the asset tag to appear in the search results
        var assetResult = page.GetByText(
         assetTag, new() { Exact = true }).First;

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