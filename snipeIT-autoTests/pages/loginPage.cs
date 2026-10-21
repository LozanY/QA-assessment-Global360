
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace snipeIT_autoTests.Pages
{
    public class LoginPage
    {
        public async Task Login(IPage page)
        {
            await page.GotoAsync("https://demo.snipeitapp.com/login");

            await page.Locator("#username").FillAsync("admin");
            await page.Locator("#password-field").FillAsync("password");
            await page.Locator("#submit").ClickAsync();

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}
