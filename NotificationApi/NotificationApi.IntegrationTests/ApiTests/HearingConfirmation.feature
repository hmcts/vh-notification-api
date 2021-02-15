Feature: Create Hearing confirmation notifications
  In order to manage hearing confirmation notifications
  As an API service
  I want to create hearing confirmation notifications data

  @Ignore until template is updated
  Scenario: Create a hearing confirmation for a judge
    Given I have a hearing confirmation for a judge email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a hearing confirmation for a judicial office holder
    Given I have a hearing confirmation for a joh email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a hearing confirmation for a LIP
    Given I have a hearing confirmation for a LIP email notification request
    When I send the request
    Then the response should have the status OK and success status True

  Scenario: Create a hearing confirmation for a representative
    Given I have a hearing confirmation for a representative email notification request
    When I send the request
    Then the response should have the status OK and success status True
