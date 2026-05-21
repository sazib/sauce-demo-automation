using OpenQA.Selenium;

namespace SauceDemo.Framework.Utilities;

public static class ScreenshotHelper
{
    public static byte[] TakeScreenshot(IWebDriver driver)
    {
        return ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
    }

    public static void SaveScreenshot(IWebDriver driver, string filePath)
    {
        var screenshot = TakeScreenshot(driver);
        var directory = Path.GetDirectoryName(filePath);
        
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllBytes(filePath, screenshot);
    }

    public static string SaveScreenshotWithTimestamp(IWebDriver driver, string directory, string prefix = "screenshot")
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var fileName = $"{prefix}_{timestamp}.png";
        var filePath = Path.Combine(directory, fileName);
        
        SaveScreenshot(driver, filePath);
        
        return filePath;
    }
}
