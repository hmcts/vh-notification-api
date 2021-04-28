Feature: Get Templates
  In order to get templates
  As an api service
  I want to be able to get the templates available

Scenario Outline: Get a template for a notification type
	Given I notification type <notificationType>
	When I send a get template by notification type request
	Then a template should return
	And should exist in GovUK notify

	Examples:
		| notificationType                          |
		| CreateIndividual                          |
		| CreateRepresentative                      |
		| HearingConfirmationLip                    |
		| HearingConfirmationRepresentative         |
		| HearingConfirmationJudge                  |
		| HearingConfirmationJoh                    |
		| HearingConfirmationLipMultiDay            |
		| HearingConfirmationRepresentativeMultiDay |
		| HearingConfirmationJudgeMultiDay          |
		| HearingConfirmationJohMultiDay            |
		| HearingAmendmentLip                       |
		| HearingAmendmentRepresentative            |
		| HearingAmendmentJudge                     |
		| HearingAmendmentJoh                       |
		| HearingReminderLip                        |
		| HearingReminderRepresentative             |
		| HearingReminderJoh                        |
		| HearingConfirmationEJudJudge              |
		| HearingConfirmationEJudJudgeMultiDay      |
		| HearingAmendmentEJudJudge                 |
		| HearingAmendmentEJudJoh                   |
