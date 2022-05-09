Feature: Create Hearing confirmation notifications
  In order to manage hearing confirmation notifications
  As an API service
  I want to create hearing confirmation notifications data

Scenario: Create a hearing confirmation for a judge
	Given I have a hearing confirmation for a judge email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for a staffmember
	Given I have a hearing confirmation for a staffmember email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for an ejud judge
	Given I have a hearing confirmation for an ejud judge email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for a judicial office holder
	Given I have a hearing confirmation for a joh email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for a LIP
	Given I have a hearing confirmation for a LIP email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for a telephone 
  Given I have a hearing confirmation for a telephone email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing confirmation for a representative
	Given I have a hearing confirmation for a representative email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a new hearing confirmation template for a LIP
  Given I have a hearing confirmation for a LIP email notification request with new template
  When I send the create notification request
  Then the api client should return true
  And Notify should have my request

Scenario: Create a new hearing confirmation template for a judicial office holder
  Given I have a hearing confirmation for a joh email notification request with new template
  When I send the create notification request
  Then the api client should return true
  And Notify should have my request

Scenario: Create a new hearing confirmation template for a representative
  Given I have a hearing confirmation for a representative email notification request with new template
  When I send the create notification request
  Then the api client should return true
  And Notify should have my request

  Scenario: Create a new hearing confirmation template for a ejudge judicial office holder
  Given I have a hearing confirmation for a ejudge joh email notification request with new template
  When I send the create notification request
  Then the api client should return true
  And Notify should have my request
