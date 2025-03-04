using Microsoft.Playwright;

namespace LoginTests.Pages.Interfaces;

public interface IBaseAppPage
{
    IPage Page { get; }
}
