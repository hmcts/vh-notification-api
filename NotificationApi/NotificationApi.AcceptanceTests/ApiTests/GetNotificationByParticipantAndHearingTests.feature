Feature: Get Notification By Participant And Hearing
	In order to get a notification
  As an api service
  I want to be able to get a notification by participant and hearings


Scenario: Get Notification by Participant and Hearing
	Given I have a request to create an email notification for new individual
  And I send the create notification request
 	When I send a request to get notification participant and hearing
  Then the result should have a valid notification response
