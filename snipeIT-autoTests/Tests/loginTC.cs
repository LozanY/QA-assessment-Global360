
using Microsoft.Playwright;
using NUnit.Framework;
using snipeIT_autoTests.Pages;

namespace snipeIT_autoTests;

public class LoginTests : baseTest
{
    [Test]
    public async Task TC01_VerifyLogin()
    {
        var loginPage = new LoginPage();
        await loginPage.Login(page);

        Assert.That(page.Url, Does.Not.Contain("/login"));
    }
}
