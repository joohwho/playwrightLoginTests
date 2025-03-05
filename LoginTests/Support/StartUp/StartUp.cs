using Reqnroll.BoDi;
using LoginTests.Pages.Interfaces;
using LoginTests.Pages.Implementations;
using LoginTests.Support.Drivers.Interfaces;
using LoginTests.Support.Drivers.Implementations;
using Microsoft.Playwright;

namespace LoginTests.Support.StartUp;

[Binding]
public class StartUp(IObjectContainer objectContainer)
{
    private readonly IObjectContainer _objectContainer = objectContainer;

    [BeforeTestRun]
    public static void GlobalSetup(IObjectContainer container)
    {
        var driver = new Driver();

        container.RegisterInstanceAs<IDriver>(driver);
        container.RegisterInstanceAs<IPage>(driver.Page);

        container.RegisterTypeAs<LoginAppPage, ILoginAppPage>();
    }

    [AfterTestRun]
    public static void GlobalTeardown()
    {
        // Se precisar liberar recursos globalmente
    }
}
