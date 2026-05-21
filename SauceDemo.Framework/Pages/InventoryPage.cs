using OpenQA.Selenium;

namespace SauceDemo.Framework.Pages;

public class InventoryPage : BasePage
{
    // Locators
    private readonly By _pageTitle = By.CssSelector(".title");
    private readonly By _inventoryItems = By.CssSelector(".inventory_item");
    private readonly By _shoppingCartBadge = By.CssSelector(".shopping_cart_badge");
    private readonly By _shoppingCartLink = By.CssSelector(".shopping_cart_link");
    private readonly By _hamburgerMenu = By.Id("react-burger-menu-btn");
    private readonly By _logoutLink = By.Id("logout_sidebar_link");

    public InventoryPage(IWebDriver driver) : base(driver)
    {
    }

    // Dynamic locators
    private By GetAddToCartButtonByProductName(string productName)
    {
        return By.XPath($"//div[@class='inventory_item'][.//div[text()='{productName}']]//button[contains(@id, 'add-to-cart')]");
    }

    private By GetRemoveButtonByProductName(string productName)
    {
        return By.XPath($"//div[@class='inventory_item'][.//div[text()='{productName}']]//button[contains(@id, 'remove')]");
    }

    // Actions
    public void AddProductToCart(string productName)
    {
        var addButton = WaitForElementToBeClickable(GetAddToCartButtonByProductName(productName));
        ScrollToElement(addButton);
        addButton.Click();
    }

    public void RemoveProductFromCart(string productName)
    {
        WaitForElementToBeClickable(GetRemoveButtonByProductName(productName)).Click();
    }

    public void ClickShoppingCart()
    {
        WaitForElementToBeClickable(_shoppingCartLink).Click();
    }

    public void Logout()
    {
        WaitForElementToBeClickable(_hamburgerMenu).Click();
        WaitForElementToBeClickable(_logoutLink).Click();
    }

    // Validations
    public bool IsOnInventoryPage()
    {
        return IsElementDisplayed(_pageTitle) && GetPageTitle() == "Products";
    }

    public string GetPageTitle()
    {
        return WaitForElement(_pageTitle).Text;
    }

    public int GetCartItemCount()
    {
        if (!IsElementDisplayed(_shoppingCartBadge))
            return 0;

        var badgeText = Driver.FindElement(_shoppingCartBadge).Text;
        return int.TryParse(badgeText, out var count) ? count : 0;
    }

    public bool IsProductInCart(string productName)
    {
        return IsElementDisplayed(GetRemoveButtonByProductName(productName));
    }

    public int GetInventoryItemCount()
    {
        return Driver.FindElements(_inventoryItems).Count;
    }
}
