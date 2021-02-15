using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using AcceptanceTests.Common.Api.Helpers;
using FluentAssertions;
using NotificationApi.Contract;
using NotificationApi.Contract.Requests;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;
using Testing.Common.Extensions;
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

        [Given("I have a valid new user email notification request")]
        public void I_have_a_valid_create_new_email_notification_request()
        {
            var request = BuildNewUserNotificationRequest(MessageType.Email);
            InitCreateNotificationRequest(request);
        }

        [Given("I have a valid password reset email notification request")]
        public void I_have_a_valid_password_reset_email_notification_request()
        {
            var messageType = MessageType.Email;
            var templateType = NotificationType.PasswordReset;
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"password", "inttestpassword!"}
            };
            var request = AddNotificationRequestBuilder.BuildNonHearingRequest(messageType, templateType, parameters);
            InitCreateNotificationRequest(request);
        }

        private void InitCreateNotificationRequest(AddNotificationRequest request)
        {
            _context.Uri = ApiUriFactory.NotificationEndpoints.CreateNewEmailNotification;
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

        private AddNotificationRequest BuildNewUserNotificationRequest(MessageType messageType)
        {
            var parameters = new Dictionary<string, string>
            {
                {"name", $"{Faker.Name.FullName()}"},
                {"username", $"{Guid.NewGuid().ToString()}@intautomation.com"},
                {"random password", "inttestpassword!"}
            };

            return AddNotificationRequestBuilder.BuildRequest(messageType, NotificationType.CreateIndividual,
                parameters);
        }
    }
}
