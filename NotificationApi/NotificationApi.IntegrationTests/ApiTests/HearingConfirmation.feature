Feature: Create Hearing confirmation notifications
  In order to manage hearing confirmation notifications
  As an API service
  I want to create hearing confirmation notifications data

Scenario: Create a hearing confirmation for a judge
	Given I have a hearing confirmation for a judge email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing confirmation for a staffmember
	Given I have a hearing confirmation for a staffmember email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing confirmation for an ejud judge
	Given I have a hearing confirmation for an ejud judge email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing confirmation for an ejud judicial office holder
	Given I have a hearing confirmation for an ejud joh email notification request
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

Scenario: Create a telephone hearing confirmation
	Given I have a telephone hearing confirmation for an email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing confirmation for a representative
	Given I have a hearing confirmation for a representative email notification request
	When I send the request
	Then the response should have the status OK and success status True

  Scenario: Create a new hearing confirmation template for a LIP
    Given I have a hearing confirmation for a LIP email notification request with new template
    When I send the request
    Then the response should have the status OK and success status True
    
  Scenario: Create a new hearing confirmation template for a judicial office holder
    Given I have a hearing confirmation for a joh email notification request with new template
    When I send the request
    Then the response should have the status OK and success status True
    
  Scenario: Create a new hearing confirmation template for a representative
    Given I have a hearing confirmation for a representative email notification request with new template
    When I send the request
    Then the response should have the status OK and success status True
    
