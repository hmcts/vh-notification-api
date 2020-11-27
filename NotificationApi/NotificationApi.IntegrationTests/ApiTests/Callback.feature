Feature: Delivery Callbacks
  In order to handle delivery receipts from Notify
  As a developer
  I want to verify notification statuses are updated correctly

  Scenario Outline: Successfully update the delivery status of a notification
    Given I have a notification that has been sent
    And I have a update notification request with a status <status>
    When I send the request
    Then the response should have the status OK and success status True
    Examples:
      |status             |
      |delivered          |
      |permanent-failure  |
      |temporary-failure  |
      |technical-failure  |


  Scenario: Send update the delivery status with mismatched ids
    Given I have a notification that has been sent
    And I have a update notification request with mismatched ids
    When I send the request
    Then the response should have the status BadRequest and success status False

  Scenario: Send an invalid payload when updating a notification
    Given I have a notification that has been sent
    And I have a update notification request with an invalid status
    When I send the request
    Then the response should have the status BadRequest and success status False
