Feature: Create Notifications
  In order to manage notifcations
  As an API service
  I want to create notifications data
  
Scenario: Creating a email notification
  Given I have a valid create new email notification request 
  When I send the request
  Then the response should have the status OK
  And the success status should be True

    
Scenario: Creating a sms notification
  Given I have a valid create new sms notification request 
  When I send the request
  Then the response should have the status OK
  And the success status should be True
