using FluentAssertions;
using Reqnroll;
using SauceDemo.Framework.Configuration;
using SauceDemo.Framework.Driver;
using SauceDemo.Framework.Pages;

namespace SauceDemo.Tests.StepDefinitions;

[Binding]
public class LoginSteps
{
    private readonly ScenarioContext _scenarioContext;
    private LoginPage _loginPage = null!;

    public LoginSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I am on the SauceDemo login page")]
    public void GivenIAmOnTheSauceDemoLoginPage()
    {
        DriverManager.NavigateToBaseUrl();
        _loginPage = new LoginPage(DriverManager.Driver);
        _loginPage.IsOnLoginPage().Should().BeTrue("the login page should be displayed");
    }

    [Given(@"I login with valid credentials")]
    [When(@"I login with valid credentials")]
    public void WhenILoginWithValidCredentials()
    {
        var user = ConfigurationManager.Instance.GetTestUser("StandardUser");
        _loginPage = new LoginPage(DriverManager.Driver);
        _loginPage.Login(user.Username, user.Password);

        var inventoryPage = new InventoryPage(DriverManager.Driver);
        inventoryPage.IsOnInventoryPage().Should().BeTrue("user should be logged in successfully");
    }

    [When(@"I attempt to login with username ""([^""]*)"" and password ""([^""]*)""")]
    public void WhenIAttemptToLoginWithUsernameAndPassword(string username, string password)
    {
        _loginPage = new LoginPage(DriverManager.Driver);
        _loginPage.Login(username, password);
    }

    [Then(@"I should see an error message")]
    public void ThenIShouldSeeAnErrorMessage()
    {
        _loginPage = new LoginPage(DriverManager.Driver);
        _loginPage.IsErrorMessageDisplayed().Should().BeTrue("an error message should be displayed");
    }

    [Then(@"the error message should contain ""([^""]*)""")]
    public void ThenTheErrorMessageShouldContain(string expectedMessage)
    {
        _loginPage = new LoginPage(DriverManager.Driver);
        var actualMessage = _loginPage.GetErrorMessage();
        actualMessage.Should().Contain(expectedMessage, $"error message should contain '{expectedMessage}'");
    }
}
