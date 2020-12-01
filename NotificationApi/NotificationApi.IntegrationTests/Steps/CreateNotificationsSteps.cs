using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using AcceptanceTests.Common.Api.Helpers;
using FluentAssertions;
using NotificationApi.Contract.Requests;
using NotificationApi.Domain.Enums;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Helper;

namespace NotificationApi.IntegrationTests.Steps
{
    [Binding]
    public class CreateNotificationsSteps
    {
        private readonly IntTestContext _context;
        
        public CreateNotificationsSteps(IntTestContext context)
        {
            _context = context;
        }
        
        [Given("I have a valid create new email notification request")]
        public void I_have_a_valid_create_new_email_notification_request()
        {
            var request = BuildRequest();
            _context.Uri = ApiUriFactory.NotificationEndpoints.CreateNewEmailNotificationResponse;
            _context.HttpMethod = HttpMethod.Post;
            var body = RequestHelper.Serialise(request);
            _context.HttpContent = new StringContent(body, Encoding.UTF8, "application/json");
        }
        
        [Then("the response should have the status (.*)")]
        public void the_response_should_have_the_status_created(HttpStatusCode statusCode)
        {
            _context.Response.StatusCode.Should().Be(statusCode);
        }
        
        [Then("the success status should be (.*)")]
        public void the_success_status_should_be_true(bool isSuccess)
        {
            _context.Response.IsSuccessStatusCode.Should().Be(isSuccess);
        }

        private AddNotificationRequest BuildRequest()
        {
            var parameters = new Dictionary<string, string>
            {
                {"Case number", "134"},
                {"Name", "test"},
                {"Day Month Year", "12/12/11"},
                {"time", "DateTime"},
                {"Username", "testUser"}
            };

            return new AddNotificationRequest
            {
                ContactEmail = "email@email.com",
                HearingId = Guid.NewGuid(),
                MessageType = (int)MessageType.Email,
                NotificationType = (int)NotificationType.CreateIndividual,
                Parameters = parameters,
                ParticipantId = Guid.NewGuid(),
                PhoneNumber = "1234567890"
            };
        }
    }
}
