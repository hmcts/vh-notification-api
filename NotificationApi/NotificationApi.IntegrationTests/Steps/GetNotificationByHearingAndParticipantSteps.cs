using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NotificationApi.Contract.Responses;
using NotificationApi.Domain.Enums;
using NotificationApi.IntegrationTests.Contexts;
using NotificationApi.IntegrationTests.Helper;
using TechTalk.SpecFlow;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class GetNotificationByHearingAndParticipantSteps
    {
        private readonly IntTestContext _context;

        public GetNotificationByHearingAndParticipantSteps(IntTestContext context)
        {
            _context = context;
        }

        [Given(@"I have a request to get notification by participant and hearing")]
        public async Task GivenIHaveARequestToGetPasswordNotificationTypeByEmail()
        {
            // Act 
            var notification = await _context.TestDataManager.SeedCreatedNotification();
            _context.TestRun.NotificationsCreated.Add(notification);


            _context.Uri = ApiUriFactory.NotificationEndpoints.GetNotificationByHearingAndParticipant((int)notification.NotificationType, notification.ParticipantRefId.ToString(), notification.HearingRefId.ToString());
            _context.HttpMethod = HttpMethod.Get;
        }

        [Then(@"the response should contain a  notification response")]
        public async Task ThenTheResponseShouldContainANotificationResponse()
        {
            var notificationResponse = await ApiClientResponse.GetResponses<NotificationResponse>(_context.Response.Content);
            notificationResponse.Id.Should().Be(_context.TestRun.NotificationsCreated.FirstOrDefault().Id);
        }
    }
}
