using Microsoft.Playwright;
using LoginTests.Pages.Interfaces;
using LoginTests.Support.Drivers.Interfaces;

namespace LoginTests.Pages.Implementations;

class LoginAppPage : BaseAppPage, ILoginAppPage
{
    public LoginAppPage(IDriver driver) : base(driver)
    {
    }

    private ILocator UsernameField => Page.GetByRole(AriaRole.Textbox, new() { NameString = "Username" });
    private ILocator PasswordField => Page.GetByRole(AriaRole.Textbox, new() { NameString = "Password" });
    private ILocator SubmitButton => Page.GetByRole(AriaRole.Button, new() { NameString = "Submit" });

    public async Task FillInUsernameAsync(string username) => await UsernameField.FillAsync(username);
    public async Task FillInPasswordAsync(string password) => await PasswordField.FillAsync(password);
    public async Task PressOnSubmitAsync() => await SubmitButton.ClickAsync();
    public async Task<bool> CheckTheLoginPageMessageAsync(string message) => await Page.Locator($"text={message}").IsVisibleAsync();
}
