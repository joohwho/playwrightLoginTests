using Microsoft.Playwright;
using LoginTests.Support.Drivers.Interfaces;

namespace LoginTests.Support.Drivers.Implementations;

public class Driver : IDriver
{
    private readonly Task<IPage> _page;
    private IBrowser? _browser;
    public IPage Page => _page.Result;

    public Driver()
    {
        _page = InitializePlaywright();
    }

    public async Task<IPage> InitializePlaywright()
    {
        var playwright = await Playwright.CreateAsync();

        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var context = await _browser.NewContextAsync(new()
        {
            BaseURL = "https://practicetestautomation.com/practice-test-login/"
        });

        return await context.NewPageAsync();
    }

    public void Dispose()
    {
        _browser?.CloseAsync();
    }
}
