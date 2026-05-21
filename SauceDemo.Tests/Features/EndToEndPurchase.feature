Feature: End-to-End Purchase Flow
  As a customer
  I want to complete a purchase from start to finish
  So that I can buy products from the store

  Background:
    Given I am on the SauceDemo login page
    And I login with valid credentials

  @smoke @happy-path
  Scenario: Complete end-to-end purchase with single product
    When I add "Sauce Labs Backpack" to the cart
    And I navigate to the shopping cart
    And I proceed to checkout
    And I fill in checkout information with:
      | Field      | Value      |
      | FirstName  | John       |
      | LastName   | Doe        |
      | PostalCode | 12345      |
    And I continue to checkout overview
    And I finish the checkout
    Then I should see the order confirmation
    And the confirmation message should contain "Thank you for your order"

  @smoke @happy-path
  Scenario: Complete end-to-end purchase with multiple products
    When I add the following products to the cart:
      | Product                           |
      | Sauce Labs Backpack               |
      | Sauce Labs Bike Light             |
      | Sauce Labs Bolt T-Shirt           |
    And I navigate to the shopping cart
    Then the cart should contain 3 items
    When I proceed to checkout
    And I fill in checkout information with:
      | Field      | Value      |
      | FirstName  | Jane       |
      | LastName   | Smith      |
      | PostalCode | 67890      |
    And I continue to checkout overview
    Then I should see all selected products in the order summary
    When I finish the checkout
    Then I should see the order confirmation
