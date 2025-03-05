using Microsoft.Playwright;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;
using LoginTests.Support.Reports.ExtentReport;
using LoginTests.Support.Drivers.Interfaces;

namespace LoginTests.Support.Hooks;

[Binding]
public class Hooks
{
    private IPage _page;
    private static ExtentTest _feature = default!;
    private static ExtentTest _scenario = default!;
    private readonly FeatureContext _featureContext;
    private readonly ScenarioContext _scenarioContext;
    private static readonly ExtentReports _extent = ExtentReportManager.GetExtentReport();

    public Hooks(
        IDriver driver,
        ScenarioContext scenarioContext,
        FeatureContext featureContext
    )
    {
        _page = driver.Page;
        _featureContext = featureContext;
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Console.WriteLine("Starting Tests...");
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void BeforeScenario(IPage page)
    {
        _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        _page = page;
    }

    [AfterStep]
    public async Task AfterStep()
    {
        var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
        var stepName = _scenarioContext.StepContext.StepInfo.Text;
        var stepNode = _scenario.CreateNode(new GherkinKeyword(stepType), stepName);
        string screenshotBase64 = await CaptureScreenshotBase64();

        stepNode.Info($"<img src='data:image/png;base64,{screenshotBase64}'/>");

        switch (_scenarioContext.StepContext.Status)
        {
            case ScenarioExecutionStatus.OK:
                stepNode.Pass("Step executed successfully");
                break;

            case ScenarioExecutionStatus.StepDefinitionPending:
                stepNode.Info("Step definition pending");
                break;

            case ScenarioExecutionStatus.UndefinedStep:
                stepNode.Fail("Step is undefined (no implementation)");
                break;

            case ScenarioExecutionStatus.BindingError:
                stepNode.Fail("Step binding error");
                break;

            case ScenarioExecutionStatus.TestError:
                stepNode.Fail(_scenarioContext.TestError?.Message ?? "Unknown error");
                break;

            case ScenarioExecutionStatus.Skipped:
                stepNode.Skip("Step was skipped");
                break;

            default:
                stepNode.Info("Unknown step status");
                break;
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        Console.WriteLine($"Finishing scenario: {_scenarioContext.ScenarioInfo.Title}");
    }

    [AfterFeature]
    public static void AfterFeature()
    {
        Console.WriteLine("Finishing Feature...");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _extent.Flush();
    }

    private async Task<string> CaptureScreenshotBase64()
    {
        var screenshotBytes = await _page.ScreenshotAsync();
        string base64Image = Convert.ToBase64String(screenshotBytes);

        return base64Image;
    }
}
