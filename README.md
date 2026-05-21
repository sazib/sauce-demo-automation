# SauceDemo Automation Framework

A comprehensive C# test automation framework for [SauceDemo](https://www.saucedemo.com/) using Reqnroll (SpecFlow successor), NUnit, and Selenium WebDriver with the Page Object Model pattern.

## 🏗️ Project Structure

```
SauceDemo.AutomationFramework/
├── SauceDemo.Framework/              # Core framework library
│   ├── Configuration/                # Configuration management
│   │   ├── ConfigurationManager.cs   # Centralized config handler
│   │   ├── TestSettings.cs           # Test settings model
│   │   └── TestUser.cs               # Test user model
│   ├── Driver/                       # WebDriver management
│   │   ├── DriverFactory.cs          # Browser driver factory
│   │   └── DriverManager.cs          # Driver lifecycle management
│   └── Pages/                        # Page Object Model
│       ├── BasePage.cs               # Base page with common methods
│       ├── LoginPage.cs              # Login page object
│       ├── InventoryPage.cs          # Products page object
│       ├── CartPage.cs               # Shopping cart page object
│       ├── CheckoutStepOnePage.cs    # Checkout info page object
│       ├── CheckoutStepTwoPage.cs    # Checkout overview page object
│       └── CheckoutCompletePage.cs   # Order confirmation page object
│
├── SauceDemo.Tests/                  # Test project
│   ├── Features/                     # Gherkin feature files
│   │   ├── EndToEndPurchase.feature  # Happy path scenarios
│   │   ├── NegativeScenarios.feature # Validation & error scenarios
│   │   └── Navigation.feature        # Navigation & state persistence
│   ├── StepDefinitions/              # Reqnroll step definitions
│   │   ├── LoginSteps.cs
│   │   ├── InventorySteps.cs
│   │   ├── CartSteps.cs
│   │   └── CheckoutSteps.cs
│   ├── Hooks/                        # Test lifecycle hooks
│   │   └── TestHooks.cs              # Setup/teardown logic
│   ├── appsettings.json              # Local test configuration
│   └── appsettings.CI.json           # CI/CD configuration
│
└── .github/workflows/                # CI/CD pipeline
    └── test-execution.yml            # GitHub Actions workflow
```

## 🚀 Quick Start

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Chrome, Firefox, or Edge browser
- Git

### Clone and Run

```bash
# Clone the repository
git clone <repository-url>
cd SauceDemo.AutomationFramework

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests with specific tags
dotnet test --filter "Category=smoke"
dotnet test --filter "Category=happy-path"
dotnet test --filter "Category=negative"
```

### Run from IDE

1. Open `SauceDemo.AutomationFramework.sln` in Visual Studio, Rider, or VS Code
2. Build the solution
3. Open Test Explorer
4. Run individual tests or entire test suites

## ⚙️ Configuration

### Configuration Files

The framework uses a hierarchical configuration system:

1. **appsettings.json** - Default local settings
2. **appsettings.{Environment}.json** - Environment-specific overrides
3. **Environment Variables** - Runtime overrides

### Test Settings

Edit `SauceDemo.Tests/appsettings.json`:

```json
{
  "TestSettings": {
    "BaseUrl": "https://www.saucedemo.com",
    "Browser": "Chrome",                    // Chrome, Firefox, or Edge
    "Headless": false,                      // true for headless mode
    "ImplicitWaitSeconds": 10,
    "PageLoadTimeoutSeconds": 30,
    "ScreenshotOnFailure": true,
    "ScreenshotPath": "Screenshots",
    "LogLevel": "Information"               // Debug, Information, Warning, Error
  }
}
```

### Environment Variables

Override settings at runtime:

```bash
# Linux/Mac
export TEST_ENVIRONMENT=CI
export BROWSER=Firefox
export HEADLESS=true
export BASE_URL=https://www.saucedemo.com
dotnet test
```

### Test Users

Configured in `appsettings.json`:

```json
{
  "TestUsers": {
    "StandardUser": {
      "Username": "standard_user",
      "Password": "secret_sauce"
    },
    "LockedOutUser": {
      "Username": "locked_out_user",
      "Password": "secret_sauce"
    }
  }
}
```

## 🧪 Test Scenarios

### 1. End-to-End Purchase (Happy Path)
- ✅ Complete purchase with single product
- ✅ Complete purchase with multiple products
- ✅ Verify order confirmation

### 2. Negative Scenarios & Validation
- ✅ Login with invalid username
- ✅ Login with invalid password
- ✅ Login with locked out user
- ✅ Login with empty credentials
- ✅ Checkout with missing required fields (First Name, Last Name, Postal Code)

### 3. Navigation & State Persistence
- ✅ Navigate back from cart to products (cart persists)
- ✅ Navigate back from checkout information (cart persists)
- ✅ Navigate back from checkout overview (selections persist)
- ✅ Remove item from cart and verify updates

## 🏛️ Architecture

### Page Object Model

All UI interactions are encapsulated in Page Objects:

```csharp
// Example: LoginPage.cs
public class LoginPage : BasePage
{
    private readonly By _usernameInput = By.Id("user-name");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");

    public void Login(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLoginButton();
    }
}
```

### Step Definitions

Step definitions map Gherkin steps to page object methods:

```csharp
[When(@"I login with valid credentials")]
public void WhenILoginWithValidCredentials()
{
    var user = ConfigurationManager.Instance.GetTestUser("StandardUser");
    _loginPage.Login(user.Username, user.Password);
}
```

### Driver Management

Thread-safe WebDriver management with automatic setup:

```csharp
// Automatically manages driver lifecycle
DriverManager.InitializeDriver();  // Before each scenario
DriverManager.QuitDriver();        // After each scenario
```

## 📊 Test Reports

### Console Output
Tests produce detailed console output with Serilog logging.

### TRX Reports
Test results are saved in TRX format:
```
SauceDemo.Tests/TestResults/test-results.trx
```

### Screenshots
Failed tests automatically capture screenshots:
```
SauceDemo.Tests/Screenshots/
```

### Logs
Execution logs are saved daily:
```
SauceDemo.Tests/logs/test-execution-YYYYMMDD.log
```

## 🔄 CI/CD Pipeline

### GitHub Actions

The project includes a GitHub Actions workflow that:
- ✅ Runs on push to main/develop branches
- ✅ Runs on pull requests
- ✅ Supports manual trigger
- ✅ Executes tests in headless mode
- ✅ Uploads test results and artifacts
- ✅ Publishes test reports

### Pipeline Configuration

See `.github/workflows/test-execution.yml` for the complete pipeline configuration.

### Running in CI

The pipeline automatically uses `appsettings.CI.json` with headless mode enabled.

## 🛠️ Advanced Usage

### Run Specific Test Categories

```bash
# Smoke tests only
dotnet test --filter "Category=smoke"

# Happy path scenarios
dotnet test --filter "Category=happy-path"

# Negative scenarios
dotnet test --filter "Category=negative"

# Navigation tests
dotnet test --filter "Category=navigation"

# Validation tests
dotnet test --filter "Category=validation"
```

### Run Specific Feature

```bash
dotnet test --filter "FullyQualifiedName~EndToEndPurchase"
dotnet test --filter "FullyQualifiedName~NegativeScenarios"
dotnet test --filter "FullyQualifiedName~Navigation"
```

### Parallel Execution

```bash
# Run tests in parallel (default)
dotnet test --parallel

# Specify max parallel threads
dotnet test -- NUnit.NumberOfTestWorkers=4
```

### Different Browsers

```bash
# Chrome (default)
dotnet test

# Firefox
export BROWSER=Firefox
dotnet test

# Edge
export BROWSER=Edge
dotnet test
```

## 🎯 Design Decisions

### Why Reqnroll?
- Modern successor to SpecFlow with better .NET support
- Active development and community
- Better performance and compatibility with .NET 8+

### Why Page Object Model?
- Separation of concerns (test logic vs. UI interactions)
- Reusability and maintainability
- Easier to update when UI changes
- No WebDriver calls in step definitions

### Why WebDriverManager?
- Automatic driver download and management
- No manual driver updates needed
- Cross-platform compatibility

### Configuration Strategy
- Hierarchical configuration (JSON → Environment → Variables)
- Environment-specific settings (Local, CI, etc.)
- Easy to extend and customize

## 🧩 Extending the Framework

### Add New Page Object

```csharp
public class NewPage : BasePage
{
    private readonly By _element = By.Id("element-id");

    public NewPage(IWebDriver driver) : base(driver) { }

    public void PerformAction()
    {
        WaitForElementToBeClickable(_element).Click();
    }
}
```

### Add New Feature

1. Create feature file in `Features/` folder
2. Write Gherkin scenarios
3. Generate step definitions
4. Implement step definitions using page objects

### Add New Browser Support

Edit `DriverFactory.cs` and add new browser case:

```csharp
case "safari":
    return CreateSafariDriver(settings.Headless);
```

## 📝 Best Practices

1. **Keep step definitions thin** - Business logic in page objects
2. **Use meaningful locators** - Prefer IDs, then CSS, then XPath
3. **Wait explicitly** - Use WebDriverWait, avoid Thread.Sleep
4. **One assertion per step** - Clear failure messages
5. **Clean test data** - Each test starts in known state
6. **Tag scenarios** - Enable selective test execution

## 🐛 Troubleshooting

### Driver Issues
```bash
# Clear driver cache
rm -rf ~/.webdriver

# Manually specify driver path
export CHROMEDRIVER_PATH=/path/to/chromedriver
```

### Build Issues
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Test Failures
- Check screenshots in `Screenshots/` folder
- Review logs in `logs/` folder
- Verify configuration in `appsettings.json`

## 📚 Resources

- [Reqnroll Documentation](https://docs.reqnroll.net/)
- [Selenium Documentation](https://www.selenium.dev/documentation/)
- [NUnit Documentation](https://docs.nunit.org/)
- [SauceDemo Test Site](https://www.saucedemo.com/)

## 📄 License

N/A

## 👤 Author

Created as a take-home exercise for Senior QA Automation Engineer position.

---

**Note**: This framework demonstrates professional test automation practices including:
- Clean architecture with separation of concerns
- Configurable and maintainable design
- Comprehensive test coverage
- CI/CD integration
- Proper logging and reporting
- Industry-standard patterns (Page Object Model, BDD)
