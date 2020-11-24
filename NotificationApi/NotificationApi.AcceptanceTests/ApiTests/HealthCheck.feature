Feature: Health Check
  In order to assess the status of the service
  As an api service
  I want to be able to request the health of the video api

  Scenario: Get the health of the api
    Given I send a get health request
    Then the application version should be retrieved
    And the database health should be okay
