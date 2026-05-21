namespace SauceDemo.Framework.Configuration;

public class TestSettings
{
    public string BaseUrl { get; set; } = "https://www.saucedemo.com";
    public string Browser { get; set; } = "Chrome";
    public bool Headless { get; set; } = false;
    public int ImplicitWaitSeconds { get; set; } = 10;
    public int PageLoadTimeoutSeconds { get; set; } = 30;
    public bool ScreenshotOnFailure { get; set; } = true;
    public string ScreenshotPath { get; set; } = "Screenshots";
    public string LogLevel { get; set; } = "Information";
}
