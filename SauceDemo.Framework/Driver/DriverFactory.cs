using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SauceDemo.Framework.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemo.Framework.Driver;

public class DriverFactory
{
    public static IWebDriver CreateDriver(TestSettings settings)
    {
        IWebDriver driver = settings.Browser.ToLower() switch
        {
            "chrome" => CreateChromeDriver(settings.Headless),
            "firefox" => CreateFirefoxDriver(settings.Headless),
            "edge" => CreateEdgeDriver(settings.Headless),
            _ => throw new ArgumentException($"Browser '{settings.Browser}' is not supported")
        };

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);
        driver.Manage().Window.Maximize();

        return driver;
    }

    private static IWebDriver CreateChromeDriver(bool headless)
    {
        // Use fully qualified name to avoid conflict with our DriverManager class
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        
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
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
        options.AddExcludedArgument("enable-automation");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless)
    {
        // Use fully qualified name to avoid conflict with our DriverManager class
        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
        
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
        // Use fully qualified name to avoid conflict with our DriverManager class
        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
        
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
