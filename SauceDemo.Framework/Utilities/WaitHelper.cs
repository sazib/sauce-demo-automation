using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Framework.Utilities;

public static class WaitHelper
{
    public static void WaitForCondition(IWebDriver driver, Func<IWebDriver, bool> condition, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(condition);
    }

    public static T WaitForCondition<T>(IWebDriver driver, Func<IWebDriver, T> condition, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(condition);
    }

    public static void WaitForPageLoad(IWebDriver driver, int timeoutSeconds = 30)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
    }

    public static void WaitForAjax(IWebDriver driver, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(d => (bool)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active == 0"));
    }
}
