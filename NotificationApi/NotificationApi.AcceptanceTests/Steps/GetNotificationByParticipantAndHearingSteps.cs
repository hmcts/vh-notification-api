using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract;
using NotificationApi.Contract.Responses;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class GetNotificationByParticipantAndHearingSteps
    {
        private readonly AcTestContext _context;
        private NotificationType _notificationType = NotificationType.CreateIndividual;

        public GetNotificationByParticipantAndHearingSteps(AcTestContext context)
        {
            _context = context;
        }

        [When(@"I send a request to get notification participant and hearing")]
        public async Task WhenISendARequestToGetNotificationParticipantAndHearing()
        {
            _notificationType = _context.CreateNotificationRequest.NotificationType;
            var participantId = _context.CreateNotificationRequest.ParticipantId.ToString();
            var hearingId = _context.CreateNotificationRequest.HearingId.ToString();
            await _context.ExecuteApiRequest(() =>
                _context.ApiClient.GetNotificationByHearingAndParticipantAsync(_notificationType, hearingId, participantId));
        }

        [Then(@"the result should have a valid notification response")]
        public void ThenTheResultShouldHaveAValidNotificationResponse()
        {
            _context.ApiClientResponse.Should().BeOfType<NotificationResponse>();
            var model = (NotificationResponse)_context.ApiClientResponse;
            model.Id.Should().NotBeEmpty();
        }
    }
}
