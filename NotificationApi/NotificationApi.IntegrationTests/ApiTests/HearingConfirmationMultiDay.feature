Feature: Create Multi-Day Hearing confirmation notifications
  In order to manage multi-day hearing confirmation notifications
  As an API service
  I want to create hearing multi-day confirmation notifications data
  
  Scenario: Create a multi-day hearing confirmation for a judge
    Given I have a multi-day hearing confirmation for a judge email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a multi-day hearing confirmation for an ejud judge
    Given I have a multi-day hearing confirmation for an ejud judge email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a multi-day hearing confirmation for a judicial office holder
    Given I have a multi-day hearing confirmation for a joh email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a multi-day hearing confirmation for a LIP
    Given I have a multi-day hearing confirmation for a LIP email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a multi-day hearing confirmation for a representative
    Given I have a multi-day hearing confirmation for a representative email notification request
    When I send the request
    Then the response should have the status OK and success status True
