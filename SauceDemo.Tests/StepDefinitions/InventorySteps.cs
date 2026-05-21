using FluentAssertions;
using Reqnroll;
using SauceDemo.Framework.Driver;
using SauceDemo.Framework.Pages;

namespace SauceDemo.Tests.StepDefinitions;

[Binding]
public class InventorySteps
{
    private readonly ScenarioContext _scenarioContext;
    private InventoryPage _inventoryPage = null!;

    public InventorySteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [When(@"I add ""([^""]*)"" to the cart")]
    public void WhenIAddToTheCart(string productName)
    {
        _inventoryPage = new InventoryPage(DriverManager.Driver);
        _inventoryPage.AddProductToCart(productName);
        
        // Store product name for later verification
        if (!_scenarioContext.ContainsKey("AddedProducts"))
        {
            _scenarioContext["AddedProducts"] = new List<string>();
        }
        ((List<string>)_scenarioContext["AddedProducts"]).Add(productName);
    }

    [When(@"I add the following products to the cart:")]
    public void WhenIAddTheFollowingProductsToTheCart(Table table)
    {
        _inventoryPage = new InventoryPage(DriverManager.Driver);
        var products = new List<string>();

        foreach (var row in table.Rows)
        {
            var productName = row["Product"];
            _inventoryPage.AddProductToCart(productName);
            products.Add(productName);
        }

        _scenarioContext["AddedProducts"] = products;
    }

    [When(@"I navigate to the shopping cart")]
    public void WhenINavigateToTheShoppingCart()
    {
        _inventoryPage = new InventoryPage(DriverManager.Driver);
        _inventoryPage.ClickShoppingCart();
    }

    [Then(@"I should be on the products page")]
    public void ThenIShouldBeOnTheProductsPage()
    {
        _inventoryPage = new InventoryPage(DriverManager.Driver);
        _inventoryPage.IsOnInventoryPage().Should().BeTrue("user should be on the products page");
    }

    [Then(@"the cart badge should show (\d+) items?")]
    public void ThenTheCartBadgeShouldShowItems(int expectedCount)
    {
        _inventoryPage = new InventoryPage(DriverManager.Driver);
        var actualCount = _inventoryPage.GetCartItemCount();
        actualCount.Should().Be(expectedCount, $"cart badge should show {expectedCount} items");
    }
}
