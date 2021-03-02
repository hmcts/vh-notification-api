Feature: Get Password Notification
	In order to get password reset notification
  As an api service
  I want to be able to get a password reset notification by user email


Scenario: Get Password Notification by user email
	Given I have a request to create an email notification for password reset
  And I send the create notification request
 	When I send a request to get password notification by email
  Then the result should have a valid response
