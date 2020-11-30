Feature: Addition
  In order to manage notifcations
  As an API service
  I want to create, update, retrieve and delete notifications data
  
Scenario: Creating a email new notification
  Given I have a valid create new email notification request 
  When I send the request to the endpoint
  Then the response should have the status Created
  And the success status should be True
  And the notification details should be retrieved
