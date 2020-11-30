Feature: Get Templates
  In order to get templates
  As an api service
  I want to be able to get the templates available

  Scenario Outline: Get a template for a notification type
    Given I notification type <notificationType>
    When I send a get template by notification type request
    Then a template should return
    Examples:
      |notificationType |
      |1                |
      |2                |
