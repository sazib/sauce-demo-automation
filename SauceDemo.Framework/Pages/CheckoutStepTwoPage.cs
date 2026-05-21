using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class CheckoutStepTwoPage : BasePage
{
    // Locators
    private readonly By _pageTitle = By.CssSelector(".title");
    private readonly By _cartItems = By.CssSelector(".cart_item");
    private readonly By _subtotalLabel = By.CssSelector(".summary_subtotal_label");
    private readonly By _taxLabel = By.CssSelector(".summary_tax_label");
    private readonly By _totalLabel = By.CssSelector(".summary_total_label");
    private readonly By _finishButton = By.Id("finish");
    private readonly By _cancelButton = By.Id("cancel");

    public CheckoutStepTwoPage(IWebDriver driver) : base(driver)
    {
    }

    // Dynamic locators
    private By GetCartItemByProductName(string productName)
    {
        return By.XPath($"//div[@class='cart_item'][.//div[text()='{productName}']]");
    }

    // Actions
    public void ClickFinish()
    {
        WaitForElementToBeClickable(_finishButton).Click();
    }

    public void ClickCancel()
    {
        WaitForElementToBeClickable(_cancelButton).Click();
    }

    // Validations
    public bool IsOnCheckoutStepTwoPage()
    {
        return IsElementDisplayed(_pageTitle) && GetPageTitle() == "Checkout: Overview";
    }

    public string GetPageTitle()
    {
        return WaitForElement(_pageTitle).Text;
    }

    public bool IsProductInSummary(string productName)
    {
        return IsElementDisplayed(GetCartItemByProductName(productName));
    }

    public int GetCartItemCount()
    {
        return Driver.FindElements(_cartItems).Count;
    }

    public string GetSubtotal()
    {
        return WaitForElement(_subtotalLabel).Text;
    }

    public string GetTax()
    {
        return WaitForElement(_taxLabel).Text;
    }

    public string GetTotal()
    {
        return WaitForElement(_totalLabel).Text;
    }
}
