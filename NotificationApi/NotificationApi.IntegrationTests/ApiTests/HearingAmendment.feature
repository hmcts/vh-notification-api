Feature: Create Hearing amendment notifications
  In order to manage hearing amendment notifications
  As an API service
  I want to create hearing amendment notifications data

Scenario: Create a hearing amendment for a judge
	Given I have a hearing amendment for a judge email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for an ejud judge
	Given I have a hearing amendment for an ejud judge email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for an ejud judicial office holder
	Given I have a hearing amendment for an ejud joh email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for an ejud judicial office holder demo or test
	Given I have a hearing amendment for an ejud joh demo or test email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for a judicial office holder
	Given I have a hearing amendment for a joh email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for a LIP
	Given I have a hearing amendment for a LIP email notification request
	When I send the request
	Then the response should have the status OK and success status True

Scenario: Create a hearing amendment for a representative
	Given I have a hearing amendment for a representative email notification request
	When I send the request
	Then the response should have the status OK and success status True
