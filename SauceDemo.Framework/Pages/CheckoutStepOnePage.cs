using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class CheckoutStepOnePage : BasePage
{
    // Locators
    private readonly By _pageTitle = By.CssSelector(".title");
    private readonly By _firstNameInput = By.Id("first-name");
    private readonly By _lastNameInput = By.Id("last-name");
    private readonly By _postalCodeInput = By.Id("postal-code");
    private readonly By _continueButton = By.Id("continue");
    private readonly By _cancelButton = By.Id("cancel");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");

    public CheckoutStepOnePage(IWebDriver driver) : base(driver)
    {
    }

    // Actions
    public void EnterFirstName(string firstName)
    {
        WaitForElement(_firstNameInput).Clear();
        Driver.FindElement(_firstNameInput).SendKeys(firstName);
    }

    public void EnterLastName(string lastName)
    {
        WaitForElement(_lastNameInput).Clear();
        Driver.FindElement(_lastNameInput).SendKeys(lastName);
    }

    public void EnterPostalCode(string postalCode)
    {
        WaitForElement(_postalCodeInput).Clear();
        Driver.FindElement(_postalCodeInput).SendKeys(postalCode);
    }

    public void FillCheckoutInformation(string firstName, string lastName, string postalCode)
    {
        EnterFirstName(firstName);
        EnterLastName(lastName);
        EnterPostalCode(postalCode);
    }

    public void ClickContinue()
    {
        WaitForElementToBeClickable(_continueButton).Click();
    }

    public void ClickCancel()
    {
        WaitForElementToBeClickable(_cancelButton).Click();
    }

    // Validations
    public bool IsOnCheckoutStepOnePage()
    {
        return IsElementDisplayed(_pageTitle) && GetPageTitle() == "Checkout: Your Information";
    }

    public string GetPageTitle()
    {
        return WaitForElement(_pageTitle).Text;
    }

    public bool IsErrorMessageDisplayed()
    {
        return IsElementDisplayed(_errorMessage);
    }

    public string GetErrorMessage()
    {
        return WaitForElement(_errorMessage).Text;
    }
}
