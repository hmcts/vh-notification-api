Feature: Create Notification
  In order to create a notification
  As an api service
  I want to be able to create a notification
  
  Scenario: Create a new email notification
    Given I have a request to create an email notification
    When I send the create notification request
    Then the api client should return true
    And Notify should have my request
