using FluentAssertions;
using Reqnroll;
using SauceDemo.Framework.Driver;
using SauceDemo.Framework.Pages;

namespace SauceDemo.Tests.StepDefinitions;

[Binding]
public class CartSteps
{
    private readonly ScenarioContext _scenarioContext;
    private CartPage _cartPage = null!;

    public CartSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [When(@"I proceed to checkout")]
    public void WhenIProceedToCheckout()
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.ClickCheckout();
    }

    [When(@"I continue shopping from the cart")]
    public void WhenIContinueShoppingFromTheCart()
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.ClickContinueShopping();
    }

    [When(@"I remove ""([^""]*)"" from the cart")]
    public void WhenIRemoveFromTheCart(string productName)
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.RemoveProductFromCart(productName);
    }

    [Then(@"I should be on the cart page")]
    public void ThenIShouldBeOnTheCartPage()
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.IsOnCartPage().Should().BeTrue("user should be on the cart page");
    }

    [Then(@"the cart should contain (\d+) items?")]
    public void ThenTheCartShouldContainItems(int expectedCount)
    {
        _cartPage = new CartPage(DriverManager.Driver);
        var actualCount = _cartPage.GetCartItemCount();
        actualCount.Should().Be(expectedCount, $"cart should contain {expectedCount} items");
    }

    [Then(@"""([^""]*)"" should be in the cart")]
    public void ThenShouldBeInTheCart(string productName)
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.IsProductInCart(productName).Should().BeTrue($"'{productName}' should be in the cart");
    }

    [Then(@"""([^""]*)"" should not be in the cart")]
    public void ThenShouldNotBeInTheCart(string productName)
    {
        _cartPage = new CartPage(DriverManager.Driver);
        _cartPage.IsProductInCart(productName).Should().BeFalse($"'{productName}' should not be in the cart");
    }

    [When(@"I cancel the checkout")]
    public void WhenICancelTheCheckout()
    {
        var checkoutPage = new CheckoutStepOnePage(DriverManager.Driver);
        checkoutPage.ClickCancel();
    }
}
