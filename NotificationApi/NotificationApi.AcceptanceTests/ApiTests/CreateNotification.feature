Feature: Create Notification
  In order to create a notification
  As an api service
  I want to be able to create a notification
  
  Scenario: Create a new user email notification
    Given I have a request to create an email notification for new individual
    When I send the create notification request
    Then the api client should return true
    And Notify should have my request

  Scenario: Create a password reset email notification
    Given I have a request to create an email notification for password reset
    When I send the create notification request
    Then the api client should return true
    And Notify should have my password reset request
