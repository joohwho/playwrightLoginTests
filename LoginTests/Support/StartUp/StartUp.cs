using Reqnroll.BoDi;
using LoginTests.Pages.Interfaces;
using LoginTests.Pages.Implementations;
using LoginTests.Support.Drivers.Interfaces;
using LoginTests.Support.Drivers.Implementations;

namespace LoginTests.Support.StartUp;

[Binding]
public class StartUp
{
    private readonly IObjectContainer _objectContainer;

    public StartUp(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeTestRun]
    public static void GlobalSetup(IObjectContainer container)
    {
        container.RegisterInstanceAs<IDriver>(new Driver());
        container.RegisterTypeAs<LoginAppPage, ILoginAppPage>();
    }

    [AfterTestRun]
    public static void GlobalTeardown()
    {
        // Se precisar liberar recursos globalmente
    }
}
