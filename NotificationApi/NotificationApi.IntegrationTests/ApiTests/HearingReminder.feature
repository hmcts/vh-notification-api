Feature: Create Hearing reminder notifications
  In order to manage hearing reminder notifications
  As an API service
  I want to create hearing reminder notifications data

  Scenario: Create a hearing reminder for a judicial office holder
    Given I have a hearing reminder for a joh email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a hearing reminder for a LIP
    Given I have a hearing reminder for a LIP email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a hearing reminder for a representative
    Given I have a hearing reminder for a representative email notification request
    When I send the request
    Then the response should have the status OK and success status True
