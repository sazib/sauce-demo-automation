Feature: Navigation and State Persistence
  As a customer
  I want to navigate between pages without losing my selections
  So that I can review and modify my order before completing it

  Background:
    Given I am on the SauceDemo login page
    And I login with valid credentials

  @navigation @state-persistence
  Scenario: Navigate back from cart to products and verify cart persists
    When I add "Sauce Labs Backpack" to the cart
    And I add "Sauce Labs Bike Light" to the cart
    And I navigate to the shopping cart
    Then the cart should contain 2 items
    When I continue shopping from the cart
    Then I should be on the products page
    And the cart badge should show 2 items

  @navigation @state-persistence
  Scenario: Navigate back from checkout information and verify cart persists
    When I add "Sauce Labs Backpack" to the cart
    And I navigate to the shopping cart
    And I proceed to checkout
    Then I should be on the checkout information page
    When I cancel the checkout
    Then I should be on the cart page
    And the cart should contain 1 item
    And "Sauce Labs Backpack" should be in the cart

  @navigation @state-persistence
  Scenario: Navigate back from checkout overview and verify selections persist
    When I add "Sauce Labs Backpack" to the cart
    And I add "Sauce Labs Bolt T-Shirt" to the cart
    And I navigate to the shopping cart
    And I proceed to checkout
    And I fill in checkout information with:
      | Field      | Value      |
      | FirstName  | John       |
      | LastName   | Doe        |
      | PostalCode | 12345      |
    And I continue to checkout overview
    Then I should be on the checkout overview page
    And I should see "Sauce Labs Backpack" in the order summary
    And I should see "Sauce Labs Bolt T-Shirt" in the order summary
    When I cancel the checkout overview
    Then I should be on the products page
    And the cart badge should show 2 items

  @navigation @state-persistence
  Scenario: Remove item from cart and verify cart updates
    When I add "Sauce Labs Backpack" to the cart
    And I add "Sauce Labs Bike Light" to the cart
    And I add "Sauce Labs Bolt T-Shirt" to the cart
    And I navigate to the shopping cart
    Then the cart should contain 3 items
    When I remove "Sauce Labs Bike Light" from the cart
    Then the cart should contain 2 items
    And "Sauce Labs Backpack" should be in the cart
    And "Sauce Labs Bolt T-Shirt" should be in the cart
    And "Sauce Labs Bike Light" should not be in the cart
