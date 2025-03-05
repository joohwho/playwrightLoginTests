using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace LoginTests.Support.Reports.ExtentReport;

public static class ExtentReportManager
{
    private static readonly object _lock = new();
    private static ExtentSparkReporter _sparkReporter = default!;

    public static ExtentReports Extent = default!;

    public static ExtentReports GetExtentReport()
    {
        if (Extent == null)
        {
            lock (_lock)
            {
                if (Extent == null)
                {
                    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..\\..\\..\\Support\\Reports\\ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                    _sparkReporter = new ExtentSparkReporter(reportPath);
                    _sparkReporter.Config.DocumentTitle = "Relatório de Testes";
                    _sparkReporter.Config.ReportName = "Automação de Testes";
                    _sparkReporter.Config.Theme = Theme.Standard;

                    Extent = new ExtentReports();
                    Extent.AttachReporter(_sparkReporter);
                }
            }
        }

        return Extent;
    }

    public static void FlushReport() => Extent?.Flush();
}
