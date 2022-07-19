Feature: Create Notifications
  In order to manage notifications
  As an API service
  I want to create notifications data
  
Scenario: Creating a new user email notification
  Given I have a valid new user email notification request 
  When I send the request
  Then the response should have the status OK and success status True

Scenario: Creating a new staffmember email notification
  Given I have a valid new staffmember email notification request 
  When I send the request
  Then the response should have the status OK and success status True

Scenario: Creating a password reset email notification
  Given I have a valid password reset email notification request
  When I send the request
  Then the response should have the status OK and success status True
