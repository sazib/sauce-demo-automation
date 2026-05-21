using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class CheckoutCompletePage : BasePage
{
    // Locators
    private readonly By _pageTitle = By.CssSelector(".title");
    private readonly By _completeHeader = By.CssSelector(".complete-header");
    private readonly By _completeText = By.CssSelector(".complete-text");
    private readonly By _backHomeButton = By.Id("back-to-products");

    public CheckoutCompletePage(IWebDriver driver) : base(driver)
    {
    }

    // Actions
    public void ClickBackHome()
    {
        WaitForElementToBeClickable(_backHomeButton).Click();
    }

    // Validations
    public bool IsOnCheckoutCompletePage()
    {
        return IsElementDisplayed(_pageTitle) && GetPageTitle() == "Checkout: Complete!";
    }

    public string GetPageTitle()
    {
        return WaitForElement(_pageTitle).Text;
    }

    public string GetCompleteHeader()
    {
        return WaitForElement(_completeHeader).Text;
    }

    public string GetCompleteText()
    {
        return WaitForElement(_completeText).Text;
    }

    public bool IsOrderComplete()
    {
        return IsElementDisplayed(_completeHeader) && 
               GetCompleteHeader().Contains("Thank you for your order");
    }
}
