using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class CartPage : BasePage
{
    // Locators
    private readonly By _pageTitle = By.CssSelector(".title");
    private readonly By _cartItems = By.CssSelector(".cart_item");
    private readonly By _checkoutButton = By.Id("checkout");
    private readonly By _continueShoppingButton = By.Id("continue-shopping");

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    // Dynamic locators
    private By GetCartItemByProductName(string productName)
    {
        return By.XPath($"//div[@class='cart_item'][.//div[text()='{productName}']]");
    }

    private By GetRemoveButtonByProductName(string productName)
    {
        return By.XPath($"//div[@class='cart_item'][.//div[text()='{productName}']]//button[contains(@id, 'remove')]");
    }

    // Actions
    public void ClickCheckout()
    {
        WaitForElementToBeClickable(_checkoutButton).Click();
    }

    public void ClickContinueShopping()
    {
        WaitForElementToBeClickable(_continueShoppingButton).Click();
    }

    public void RemoveProductFromCart(string productName)
    {
        WaitForElementToBeClickable(GetRemoveButtonByProductName(productName)).Click();
    }

    // Validations
    public bool IsOnCartPage()
    {
        return IsElementDisplayed(_pageTitle) && GetPageTitle() == "Your Cart";
    }

    public string GetPageTitle()
    {
        return WaitForElement(_pageTitle).Text;
    }

    public bool IsProductInCart(string productName)
    {
        return IsElementDisplayed(GetCartItemByProductName(productName));
    }

    public int GetCartItemCount()
    {
        return Driver.FindElements(_cartItems).Count;
    }
}
