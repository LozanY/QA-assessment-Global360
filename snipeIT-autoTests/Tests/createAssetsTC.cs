
using Microsoft.Playwright;
using NUnit.Framework;
using snipeIT_autoTests.Pages;

namespace snipeIT_autoTests;

public class AssetTests
{
    public static string? CreatedAssetTag;
    [Test]
    public async Task CreateNewAsset()
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

            // Click Create New dropdown
            await page.GetByText("Create New", new() { Exact = true }).ClickAsync();

            // Choose Asset
            await page.GetByText("Asset", new() { Exact = true }).ClickAsync();

            // Wait for the asset form
            await page.WaitForURLAsync("**/hardware/create**");
           
            // Open Company dropdown
                await page.Locator(
                    "#select2-company_id_select-container"
                ).ClickAsync();

            // Get available companies
                var companies = page.Locator(".select2-results__option");

            // Wait for options to appear
                await companies.First.WaitForAsync();

            // Select a random company
                int count = await companies.CountAsync();
                var random = new Random();
                int index = random.Next(count);

                await companies.Nth(index).ClickAsync();

            // Select Status: Ready to Deploy
                await page.Locator("#select2-status_select_id-container").ClickAsync();
                await page.WaitForTimeoutAsync(2000);

            // Open the Model dropdown
                await page.Locator("#select2-model_select_id-container").ClickAsync();

            // Select MacBook Pro 13"
                await page.GetByText(
                    "Laptops - Macbook Pro 13\" (#5525598838148367)",
                    new() { Exact = true }
                ).ClickAsync();
                 await page.WaitForTimeoutAsync(2000);
                
            // Select Status: Ready to Deploy

                var status = page.Locator(
                    "#select2-status_select_id-container"
                );

                await status.WaitForAsync();

                Console.WriteLine(await status.CountAsync());
                Console.WriteLine(await status.IsVisibleAsync());
                Console.WriteLine(await status.InnerTextAsync());
                await page.WaitForTimeoutAsync(2000);
            
            // Click the visible checkout button
            // Select User for checkout
                await page.Locator(
                    "input[name='checkout_to_type'][value='user']"
                ).CheckAsync();
              
            // Capture the asset tag
                CreatedAssetTag = await page.Locator("#asset_tag").InputValueAsync();

                Console.WriteLine($"Asset tag: {CreatedAssetTag}");
              
            // Click Save
                await page.Locator("#submit_button").ClickAsync();
                await page.WaitForTimeoutAsync(2000);

        }
        finally
        {
            await browser.CloseAsync();
        }
    }
}
