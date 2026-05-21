using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class LoginPage : BasePage
{
    // Locators
    private readonly By _usernameInput = By.Id("user-name");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");
    private readonly By _errorButton = By.CssSelector(".error-button");

    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    // Actions
    public void EnterUsername(string username)
    {
        WaitForElement(_usernameInput).Clear();
        Driver.FindElement(_usernameInput).SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        WaitForElement(_passwordInput).Clear();
        Driver.FindElement(_passwordInput).SendKeys(password);
    }

    public void ClickLoginButton()
    {
        WaitForElementToBeClickable(_loginButton).Click();
    }

    public void Login(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLoginButton();
    }

    // Validations
    public bool IsErrorMessageDisplayed()
    {
        return IsElementDisplayed(_errorMessage);
    }

    public string GetErrorMessage()
    {
        return WaitForElement(_errorMessage).Text;
    }

    public bool IsOnLoginPage()
    {
        return IsElementDisplayed(_loginButton);
    }
}
