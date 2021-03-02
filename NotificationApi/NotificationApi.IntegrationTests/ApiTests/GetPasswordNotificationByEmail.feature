Feature: Get Password Notification By Email
  In order to get a password reset notification for an email
  As a developer
  I want to verify a password reset notification exists for given email

  Scenario Outline: Successfully get a password notification by email
    Given I have a request to get password notification by email
    When I send the request
    Then the response should have the status OK and success status True
    And the response should contain notification responses
