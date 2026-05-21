using Reqnroll;
using SauceDemo.Framework.Driver;
using SauceDemo.Framework.Configuration;
using Serilog;

namespace SauceDemo.Tests.Hooks;

[Binding]
public class TestHooks
{
    private readonly ScenarioContext _scenarioContext;

    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        // Initialize logger
        var settings = ConfigurationManager.Instance.TestSettings;
        var logLevel = settings.LogLevel.ToLower() switch
        {
            "debug" => Serilog.Events.LogEventLevel.Debug,
            "information" => Serilog.Events.LogEventLevel.Information,
            "warning" => Serilog.Events.LogEventLevel.Warning,
            "error" => Serilog.Events.LogEventLevel.Error,
            _ => Serilog.Events.LogEventLevel.Information
        };

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(logLevel)
            .WriteTo.Console()
            .WriteTo.File("logs/test-execution-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Test execution started");
        Log.Information($"Base URL: {settings.BaseUrl}");
        Log.Information($"Browser: {settings.Browser}");
        Log.Information($"Headless: {settings.Headless}");
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        Log.Information($"Starting scenario: {_scenarioContext.ScenarioInfo.Title}");
        DriverManager.InitializeDriver();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TestError != null)
        {
            Log.Error($"Scenario failed: {_scenarioContext.ScenarioInfo.Title}");
            Log.Error($"Error: {_scenarioContext.TestError.Message}");

            // Take screenshot on failure
            var settings = ConfigurationManager.Instance.TestSettings;
            if (settings.ScreenshotOnFailure)
            {
                try
                {
                    var screenshot = DriverManager.TakeScreenshot();
                    var screenshotPath = Path.Combine(settings.ScreenshotPath, 
                        $"{_scenarioContext.ScenarioInfo.Title.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    
                    Directory.CreateDirectory(settings.ScreenshotPath);
                    File.WriteAllBytes(screenshotPath, screenshot);
                    
                    Log.Information($"Screenshot saved: {screenshotPath}");
                }
                catch (Exception ex)
                {
                    Log.Warning($"Failed to capture screenshot: {ex.Message}");
                }
            }
        }
        else
        {
            Log.Information($"Scenario passed: {_scenarioContext.ScenarioInfo.Title}");
        }

        DriverManager.QuitDriver();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Log.Information("Test execution completed");
        Log.CloseAndFlush();
    }
}
