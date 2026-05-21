Feature: Negative Scenarios and Validation
  As a system
  I want to validate user inputs and prevent invalid operations
  So that data integrity is maintained

  Background:
    Given I am on the SauceDemo login page

  @negative @validation
  Scenario: Login with invalid username
    When I attempt to login with username "invalid_user" and password "secret_sauce"
    Then I should see an error message
    And the error message should contain "Username and password do not match"

  @negative @validation
  Scenario: Login with invalid password
    When I attempt to login with username "standard_user" and password "wrong_password"
    Then I should see an error message
    And the error message should contain "Username and password do not match"

  @negative @validation
  Scenario: Login with locked out user
    When I attempt to login with username "locked_out_user" and password "secret_sauce"
    Then I should see an error message
    And the error message should contain "Sorry, this user has been locked out"

  @negative @validation
  Scenario: Login with empty credentials
    When I attempt to login with username "" and password ""
    Then I should see an error message
    And the error message should contain "Username is required"

  @negative @validation
  Scenario Outline: Checkout with missing required fields
    Given I login with valid credentials
    And I add "Sauce Labs Backpack" to the cart
    And I navigate to the shopping cart
    And I proceed to checkout
    When I fill in checkout information with:
      | Field      | Value        |
      | FirstName  | <FirstName>  |
      | LastName   | <LastName>   |
      | PostalCode | <PostalCode> |
    And I attempt to continue to checkout overview
    Then I should see an error message on checkout page
    And the checkout error message should contain "<ErrorMessage>"

    Examples:
      | FirstName | LastName | PostalCode | ErrorMessage              |
      |           | Doe      | 12345      | First Name is required    |
      | John      |          | 12345      | Last Name is required     |
      | John      | Doe      |            | Postal Code is required   |
