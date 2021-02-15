Feature: Create Hearing reminder notifications
  In order to manage hearing reminder notifications
  As an API service
  I want to create hearing reminder notifications data

  Scenario: Create a hearing reminder for a judicial office holder
    Given I have a hearing reminder for a joh email notification request
    When I send the create notification request
    Then the api client should return true
    And Notify should have my request

  Scenario: Create a hearing reminder for a LIP
    Given I have a hearing reminder for a LIP email notification request
    When I send the create notification request
    Then the api client should return true
    And Notify should have my request

  Scenario: Create a hearing reminder for a representative
    Given I have a hearing reminder for a representative email notification request
    When I send the create notification request
    Then the api client should return true
    And Notify should have my request
