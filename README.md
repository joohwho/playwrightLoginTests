# Playwright Login Automation with C# .NET and Extent Reports

This project implements automated tests for a login flow using Playwright, Reqnroll (formerly SpecFlow), and Extent Reports for report generation.

## Features
- ✅ Login with valid credentials
- ✅ Invalid login error message validation
- ✅ Instance configuration with IObjectContainer
- ✅ Dependency Injection
- ✅ Extent Reports with logs, screenshots, and videos
- ✅ Trace generation and evidence capture

## Technologies
- C# .NET 8
- Microsoft Playwright
- Reqnroll (SpecFlow)
- Extent Reports
- NUnit

## Project Structure
```plaintext
LoginTests
├── Features
│   └── LoginApp.feature
├── Pages
│   ├── ILoginAppPage.cs
│   └── LoginAppPage.cs
├── Reports
│   └── ExtentReport.html
├── Support
│   ├── Drivers
│   │   ├── IDriver.cs
│   │   └── Driver.cs
│   ├── StartUp.cs
│   └── ExtentReportManager.cs
├── Tests
│   └── LoginTests.cs
├── .gitignore
└── README.md
```

## How to Run the Automation

### 1️⃣ Install Dependencies
```bash
dotnet restore
```

### 2️⃣ Run Tests
```bash
dotnet test
```

### 3️⃣ Generate Extent Reports
Reports are saved under `/Reports`. To view:
```bash
start Reports/ExtentReport.html
```

## Extent Reports Setup
In `ExtentReportManager.cs`, initialize the report:
```csharp
var htmlReporter = new ExtentHtmlReporter("Reports/ExtentReport.html");
var extent = new ExtentReports();
extent.AttachReporter(htmlReporter);
```
End the report in test teardown:
```csharp
extent.Flush();
```

## Capturing Evidence in Reports

### Add Screenshots
```csharp
var screenshotPath = "Reports/screenshot.png";
await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
test.Log(Status.Pass, "Test passed successfully!", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
```

## Playwright Trace Viewer Integration
For trace visualization:
```bash
npx playwright show-trace trace.zip
```

## Example Code
```csharp
using Microsoft.Playwright;
using ExtentReports;
using System.Threading.Tasks;

public class LoginTests
{
    private IPage Page;
    private ExtentReports extent;
    private ExtentTest test;

    [SetUp]
    public async Task SetUp()
    {
        var playwright = await Playwright.CreateAsync();
        // Continue setup...
    }
}
```
## Final Notes

- Feel free to open issues or submit pull requests for improvements or fixes.
- Contributions are welcome, but please ensure that tests are written for any new features or bug fixes.
- This project is continually evolving, and any feedback or suggestions are appreciated!

## Contact
If you have any questions, feel free to reach out through the GitHub Issues section or email me directly.

# Happy testing!
