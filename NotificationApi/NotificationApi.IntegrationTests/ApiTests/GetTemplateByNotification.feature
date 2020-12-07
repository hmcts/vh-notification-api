Feature: Get Template By Notification Type
  In order to get a list of templates for a notification
  As a developer
  I want to verify a template exists for a notification type

  Scenario Outline: Successfully get a template for a notification type
    Given I have a request to get a template by notification type <notificationType>
    When I send the request
    Then the response should have the status OK and success status True
    And the response should contain a template for notification type <notificationType>
    Examples:
      |notificationType     |
      |CreateIndividual     |
      |CreateRepresentative |
