using OpenQA.Selenium;
using SauceDemo.Framework.Configuration;

namespace SauceDemo.Framework.Driver;

public class DriverManager
{
    private static readonly ThreadLocal<IWebDriver> _driver = new();

    public static IWebDriver Driver
    {
        get
        {
            if (_driver.Value == null)
            {
                throw new InvalidOperationException("WebDriver has not been initialized. Call InitializeDriver first.");
            }
            return _driver.Value;
        }
    }

    public static void InitializeDriver()
    {
        var settings = ConfigurationManager.Instance.TestSettings;
        _driver.Value = DriverFactory.CreateDriver(settings);
    }

    public static void QuitDriver()
    {
        if (_driver.Value != null)
        {
            _driver.Value.Quit();
            _driver.Value.Dispose();
            _driver.Value = null!;
        }
    }

    public static void NavigateToBaseUrl()
    {
        var baseUrl = ConfigurationManager.Instance.TestSettings.BaseUrl;
        Driver.Navigate().GoToUrl(baseUrl);
    }

    public static byte[] TakeScreenshot()
    {
        return ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray;
    }
}
