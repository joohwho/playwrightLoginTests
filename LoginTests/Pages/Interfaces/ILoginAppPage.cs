using Microsoft.Playwright;

namespace LoginTests.Pages.Interfaces;

public interface ILoginAppPage : IBaseAppPage
{
    Task FillInUsernameAsync(string username);
    Task FillInPasswordAsync(string password);
    Task PressOnSubmitAsync();
    Task<bool> CheckTheLoginPageMessageAsync(string message);
}
