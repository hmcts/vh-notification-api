Feature: Create Hearing amendment notifications
  In order to manage hearing amendment notifications
  As an API service
  I want to create hearing amendment notifications data

Scenario: Create a hearing amendment for a judge
	Given I have a hearing amendment for a judge email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a judge demo or test
	Given I have a hearing amendment for a judge demo or test email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a staffmember
	Given I have a hearing amendment for a staffmember email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a staffmember demo or test
	Given I have a hearing amendment for a staffmember demo or test email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for an ejud judge
	Given I have a hearing amendment for an ejud judge email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for an ejud judge demo or test
	Given I have a hearing amendment for an ejud judge demo or test email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for an ejud joh
	Given I have a hearing amendment for an ejud joh email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for an ejud joh demo or test
	Given I have a hearing amendment for an ejud joh demo or test email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a judicial office holder
	Given I have a hearing amendment for a joh email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a representative
	Given I have a hearing amendment for a representative email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a participant demo or test
	Given I have a hearing amendment for a participant demo or test email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request

Scenario: Create a hearing amendment for a LIP
	Given I have a hearing amendment for a LIP email notification request
	When I send the create notification request
	Then the api client should return true
	And Notify should have my request
