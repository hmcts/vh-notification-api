Feature: Get Notification By Participant And Hearing
  In order to get notifications by participant and hearing
  As a developer
  I want to verify a notification has been created for a hearing and participant

  Scenario Outline: Successfully get a notification by participant and hearing
    Given I have a request to get notification by participant and hearing
    When I send the request
    Then the response should have the status OK and success status True
    And the response should contain a  notification response
