using FluentAssertions;
using Reqnroll;
using SauceDemo.Framework.Driver;
using SauceDemo.Framework.Pages;

namespace SauceDemo.Tests.StepDefinitions;

[Binding]
public class CheckoutSteps
{
    private readonly ScenarioContext _scenarioContext;
    private CheckoutStepOnePage _checkoutStepOnePage = null!;
    private CheckoutStepTwoPage _checkoutStepTwoPage = null!;
    private CheckoutCompletePage _checkoutCompletePage = null!;

    public CheckoutSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [When(@"I fill in checkout information with:")]
    public void WhenIFillInCheckoutInformationWith(Table table)
    {
        _checkoutStepOnePage = new CheckoutStepOnePage(DriverManager.Driver);
        
        string firstName = string.Empty;
        string lastName = string.Empty;
        string postalCode = string.Empty;

        foreach (var row in table.Rows)
        {
            var field = row["Field"];
            var value = row["Value"];

            switch (field)
            {
                case "FirstName":
                    firstName = value;
                    break;
                case "LastName":
                    lastName = value;
                    break;
                case "PostalCode":
                    postalCode = value;
                    break;
            }
        }

        _checkoutStepOnePage.FillCheckoutInformation(firstName, lastName, postalCode);
    }

    [When(@"I continue to checkout overview")]
    [When(@"I attempt to continue to checkout overview")]
    public void WhenIContinueToCheckoutOverview()
    {
        _checkoutStepOnePage = new CheckoutStepOnePage(DriverManager.Driver);
        _checkoutStepOnePage.ClickContinue();
    }

    [When(@"I finish the checkout")]
    public void WhenIFinishTheCheckout()
    {
        _checkoutStepTwoPage = new CheckoutStepTwoPage(DriverManager.Driver);
        _checkoutStepTwoPage.ClickFinish();
    }

    [When(@"I cancel the checkout overview")]
    public void WhenICancelTheCheckoutOverview()
    {
        _checkoutStepTwoPage = new CheckoutStepTwoPage(DriverManager.Driver);
        _checkoutStepTwoPage.ClickCancel();
    }

    [Then(@"I should be on the checkout information page")]
    public void ThenIShouldBeOnTheCheckoutInformationPage()
    {
        _checkoutStepOnePage = new CheckoutStepOnePage(DriverManager.Driver);
        _checkoutStepOnePage.IsOnCheckoutStepOnePage().Should().BeTrue("user should be on checkout information page");
    }

    [Then(@"I should be on the checkout overview page")]
    public void ThenIShouldBeOnTheCheckoutOverviewPage()
    {
        _checkoutStepTwoPage = new CheckoutStepTwoPage(DriverManager.Driver);
        _checkoutStepTwoPage.IsOnCheckoutStepTwoPage().Should().BeTrue("user should be on checkout overview page");
    }

    [Then(@"I should see ""([^""]*)"" in the order summary")]
    public void ThenIShouldSeeInTheOrderSummary(string productName)
    {
        _checkoutStepTwoPage = new CheckoutStepTwoPage(DriverManager.Driver);
        _checkoutStepTwoPage.IsProductInSummary(productName).Should().BeTrue($"'{productName}' should be in order summary");
    }

    [Then(@"I should see all selected products in the order summary")]
    public void ThenIShouldSeeAllSelectedProductsInTheOrderSummary()
    {
        _checkoutStepTwoPage = new CheckoutStepTwoPage(DriverManager.Driver);
        
        if (_scenarioContext.ContainsKey("AddedProducts"))
        {
            var addedProducts = (List<string>)_scenarioContext["AddedProducts"];
            foreach (var product in addedProducts)
            {
                _checkoutStepTwoPage.IsProductInSummary(product).Should().BeTrue($"'{product}' should be in order summary");
            }
        }
    }

    [Then(@"I should see the order confirmation")]
    public void ThenIShouldSeeTheOrderConfirmation()
    {
        _checkoutCompletePage = new CheckoutCompletePage(DriverManager.Driver);
        _checkoutCompletePage.IsOnCheckoutCompletePage().Should().BeTrue("order confirmation page should be displayed");
        _checkoutCompletePage.IsOrderComplete().Should().BeTrue("order should be completed successfully");
    }

    [Then(@"the confirmation message should contain ""([^""]*)""")]
    public void ThenTheConfirmationMessageShouldContain(string expectedMessage)
    {
        _checkoutCompletePage = new CheckoutCompletePage(DriverManager.Driver);
        var actualMessage = _checkoutCompletePage.GetCompleteHeader();
        actualMessage.Should().Contain(expectedMessage, $"confirmation message should contain '{expectedMessage}'");
    }

    [Then(@"I should see an error message on checkout page")]
    public void ThenIShouldSeeAnErrorMessageOnCheckoutPage()
    {
        _checkoutStepOnePage = new CheckoutStepOnePage(DriverManager.Driver);
        _checkoutStepOnePage.IsErrorMessageDisplayed().Should().BeTrue("an error message should be displayed");
    }

    [Then(@"the checkout error message should contain ""([^""]*)""")]
    public void ThenTheCheckoutErrorMessageShouldContain(string expectedMessage)
    {
        _checkoutStepOnePage = new CheckoutStepOnePage(DriverManager.Driver);
        var actualMessage = _checkoutStepOnePage.GetErrorMessage();
        actualMessage.Should().Contain(expectedMessage, $"error message should contain '{expectedMessage}'");
    }
}
