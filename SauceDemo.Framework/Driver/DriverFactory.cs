using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SauceDemo.Framework.Configuration;

namespace SauceDemo.Framework.Driver;

/// <summary>
/// Creates browser-specific WebDriver instances.
/// Driver binaries are resolved automatically by Selenium Manager (bundled with
/// Selenium.WebDriver 4.6+), so no separate driver download step is needed.
/// In CI the workflow pins Chrome via the setup-chrome action, and Selenium Manager
/// picks up the matching chromedriver from PATH automatically.
/// </summary>
public class DriverFactory
{
    public static IWebDriver CreateDriver(TestSettings settings)
    {
        IWebDriver driver = settings.Browser.ToLower() switch
        {
            "chrome"  => CreateChromeDriver(settings.Headless),
            "firefox" => CreateFirefoxDriver(settings.Headless),
            "edge"    => CreateEdgeDriver(settings.Headless),
            _         => throw new ArgumentException($"Browser '{settings.Browser}' is not supported. Valid values: Chrome, Firefox, Edge")
        };

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        driver.Manage().Timeouts().PageLoad     = TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);
        driver.Manage().Window.Maximize();

        return driver;
    }

    private static IWebDriver CreateChromeDriver(bool headless)
    {
        // ChromeDriver is resolved by Selenium Manager — no manual SetUpDriver() call needed.
        // If CHROMEDRIVER_PATH or chromedriver is already on PATH (e.g. in CI after
        // setup-chrome), Selenium Manager will use it directly without downloading anything.
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--disable-blink-features=AutomationControlled");
        options.AddExcludedArgument("enable-automation");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless)
    {
        var options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("--headless");
        }

        options.AddArgument("--width=1920");
        options.AddArgument("--height=1080");

        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver(bool headless)
    {
        var options = new EdgeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--window-size=1920,1080");

        return new EdgeDriver(options);
    }
}
