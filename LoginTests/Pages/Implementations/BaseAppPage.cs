using Microsoft.Playwright;
using LoginTests.Pages.Interfaces;
using LoginTests.Support.Drivers.Interfaces;

namespace LoginTests.Pages.Implementations;

public class BaseAppPage : IBaseAppPage
{
    public IPage Page { get; }

    public BaseAppPage(IDriver driver)
    {
        Page = driver.Page;
    }
}
