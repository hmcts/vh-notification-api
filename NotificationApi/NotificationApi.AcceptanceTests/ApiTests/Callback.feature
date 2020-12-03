Feature: Delivery Receipts
  In order to handle delivery receipts from Notify
  As an api service
  I want to verify notification statuses are updated correctly
  
Scenario: Successfully update the delivery status of a notification
  Given I have a notification that has been sent
  And I have a delivery receipt callback with a status delivered
  When I send the callback request
  Then the api client should return true
