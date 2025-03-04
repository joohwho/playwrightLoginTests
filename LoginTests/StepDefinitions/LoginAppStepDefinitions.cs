using LoginTests.Pages.Interfaces;
using LoginTests.Support.Drivers.Interfaces;

namespace LoginTests;

[Binding]
public class LoginAppStepDefinitions
{
    private readonly IDriver Driver;
    private readonly ILoginAppPage Page;
    public LoginAppStepDefinitions(
        IDriver driver,
        ILoginAppPage page
    )
    {
        Driver = driver;
        Page = page;
    }

    [Given("I am on login page")]
    public void GivenIAmOnLoginPage() => Driver.Page.GotoAsync("https://practicetestautomation.com/practice-test-login/");

    [Given("I fill in {string} in the Username")]
    public async Task GivenIFillInInTheUsername(string username) => await Page.FillInUsernameAsync(username);

    [Given("I fill in {string} in the Password")]
    public async Task GivenIFillInInThePassword(string password) => await Page.FillInPasswordAsync(password);

    [When("I press the Submit button")]
    public async Task WhenIPressTheSubmitButton() => await Page.PressOnSubmitAsync();

    [Then("I should see the {string} page")]
    public async Task ThenIShouldSeeThePage(string welcome) => Assert.IsTrue(await Page.CheckTheLoginPageMessageAsync(welcome));
}
