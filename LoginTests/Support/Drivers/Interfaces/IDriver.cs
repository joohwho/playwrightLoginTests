using Microsoft.Playwright;

namespace LoginTests.Support.Drivers.Interfaces;

public interface IDriver : IDisposable
{
    IPage Page { get; }
    Task<IPage> InitializePlaywright();
}
