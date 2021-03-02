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
    public class GetPasswordNotificationByEmailSteps: BaseSteps
    {
        private readonly IntTestContext _context;

        public GetPasswordNotificationByEmailSteps(IntTestContext context)
        {
            _context = context;
        }


        [Given(@"I have a request to get password notification by email")]
        public async Task GivenIHaveARequestToGetPasswordNotificationByEmail()
        {
            // Act 
            const string email = "test@hmcts.net";
            var notification = await _context.TestDataManager.SeedPasswordNotification(email);
            _context.TestRun.NotificationsCreated.Add(notification);


            _context.Uri = ApiUriFactory.NotificationEndpoints.GetPasswordNotificationByEmail(email);
            _context.HttpMethod = HttpMethod.Get;
        }

        [Then(@"the response should contain notification responses")]
        public async Task ThenTheResponseShouldContainNotificationResponses()
        {
            var notificationResponse = await ApiClientResponse.GetResponses<List<NotificationResponse>>(_context.Response.Content);
            notificationResponse.Count.Should().BeGreaterThan(0);
            notificationResponse.Any(n => n.Id == _context.TestRun.NotificationsCreated.FirstOrDefault().Id).Should().BeTrue();
        }
    }
}
