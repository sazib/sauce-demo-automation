using Microsoft.Extensions.Configuration;

namespace SauceDemo.Framework.Configuration;

public class ConfigurationManager
{
    private static ConfigurationManager? _instance;
    private readonly IConfiguration _configuration;

    private ConfigurationManager()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";
        
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static ConfigurationManager Instance => _instance ??= new ConfigurationManager();

    public TestSettings TestSettings
    {
        get
        {
            var settings = new TestSettings();
            _configuration.GetSection("TestSettings").Bind(settings);
            
            // Allow environment variable overrides
            settings.BaseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? settings.BaseUrl;
            settings.Browser = Environment.GetEnvironmentVariable("BROWSER") ?? settings.Browser;
            
            if (bool.TryParse(Environment.GetEnvironmentVariable("HEADLESS"), out var headless))
                settings.Headless = headless;
            
            return settings;
        }
    }

    public TestUser GetTestUser(string userType)
    {
        var user = new TestUser();
        _configuration.GetSection($"TestUsers:{userType}").Bind(user);
        return user;
    }

    public string GetValue(string key)
    {
        return _configuration[key] ?? string.Empty;
    }
}
